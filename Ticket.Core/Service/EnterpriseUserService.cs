using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ticket.Core.Repository;
using Ticket.Model.Enum;
using Ticket.Model.Model.EnterpriseUser;
using Ticket.Model.Result;
using Ticket.SqlSugar.Models;
using Ticket.Utility.Helpers;
using Ticket.Utility.Key;

namespace Ticket.Core.Service
{
    public class EnterpriseUserService
    {
        private readonly EnterpriseUserRepository _enterpriseUserRepository;
        public EnterpriseUserService(EnterpriseUserRepository enterpriseUserRepository)
        {
            _enterpriseUserRepository = enterpriseUserRepository;
        }

        public Tbl_EnterpriseUser Get(string userName, string passWord)
        {
            var model = _enterpriseUserRepository.FirstOrDefault(o => o.UserName.Equals(userName.Trim()) && o.PassWord.Equals(passWord) && o.DataStatus == 0 && o.UserType == (int)UserType.SaleUser);
            return model;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TResult<EnterpriseUser> Login(EnterpriseUserLoginModel UserModel)
        {
            var result = new TResult<EnterpriseUser>();

            if (string.IsNullOrEmpty(UserModel.UserName) || string.IsNullOrEmpty(UserModel.PassWord))
            {
                return result.FailureResult(null, "用户名和密码不能为空！");
            }

            if (string.IsNullOrEmpty(UserModel.Code))
            {
                return result.FailureResult(null, "请输入验证码！");
            }
            var code = HttpContext.Current.Request.Cookies[SessionKey.ManagerUserLoginCode];
            if (code == null || (string.IsNullOrEmpty(code.Value)))
            {
                return result.FailureResult(null, "验证码已过期，请重新输入验证码！");
            }
            if (!String.Equals(code.Value, UserModel.Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                return result.FailureResult(null, "请输入正确的验证码！");
            }
            var password = Md5HashHelper.HashPassword(UserModel.PassWord);
            //当前景区下未停用的售票员
            var model = Get(UserModel.UserName, password);

            if (model != null)
            {
                //var tbl_Scenic = Repository.First<Tbl_Scenic>(a => a.ScenicId == model.ScenicId);
                //UserInfoDTO dto = new UserInfoDTO(model, (int)SystemType.Sale, tbl_Scenic.ScenicName);
                //model.LastLoginTime = DateTime.Now;
                //Repository.Update(model);
                var userInfo = new EnterpriseUser
                {
                    UserId = model.EnterpriseUserId,
                    UserName = model.UserName,
                    RealName = model.RealName,
                    ScenicId = model.ScenicId
                };
                //设置Session
                HttpContext.Current.Session[SessionKey.UserInfo] = userInfo;

                //设置登录信息cookie 1天有效
                HttpCookie hc = new HttpCookie(SessionKey.SaleUserLoginCookie);
                hc.Value = DesHelper.Encrypt(model.UserName + "|" + model.PassWord, SessionKey.ManagerUserLoginCookieKey);
                //设置cookie信息在第二天凌晨过期
                //（用户每天都需要输入密码登录一次，而且不会出现，在某一天中间某一刻时间，突然cookie过期）
                hc.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(hc);

                return result.SuccessResult(userInfo, "登录成功");
            }
            return result.FailureResult(null, "用户名或密码错误");
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        public void CreateCheckCodeImage()
        {
            var checkCode = CheckCodeHelper.GenerateCheckCode();
            HttpCookie hc1 = new HttpCookie(SessionKey.ManagerUserLoginCode, checkCode);
            hc1.Expires = DateTime.Now.AddMinutes(10);
            HttpContext.Current.Response.Cookies.Add(hc1);
            var bytes = CheckCodeHelper.CreateCheckCodeImage(checkCode);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/jpg";
            HttpContext.Current.Response.BinaryWrite(bytes);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        public string CreateCheckCodeImage2()
        {
            var checkCode = CheckCodeHelper.GenerateCheckCode();
            HttpCookie hc1 = new HttpCookie(SessionKey.ManagerUserLoginCode, checkCode);
            hc1.Expires = DateTime.Now.AddMinutes(10);
            HttpContext.Current.Response.Cookies.Add(hc1);
            var bytes = CheckCodeHelper.CreateCheckCodeImage(checkCode);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public TResult LoginOut()
        {
            var result = new TResult();

            //清除session
            System.Web.HttpContext.Current.Session[SessionKey.UserInfo] = null;
            //清除cookie
            int limit = System.Web.HttpContext.Current.Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                string cookieName = System.Web.HttpContext.Current.Request.Cookies[i].Name;
                if (cookieName.Equals(SessionKey.SaleUserLoginCookie))
                {
                    //当前上下文 该cookie失效
                    HttpCookie aCookie = System.Web.HttpContext.Current.Request.Cookies[i];
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    System.Web.HttpContext.Current.Response.Cookies.Add(aCookie);

                    //移除客户端中的cookie
                    System.Web.HttpContext.Current.Request.Cookies.Remove(cookieName);
                    break;
                }
            }

            return result.SuccessResult();
        }

        public EnterpriseUser LoginForSession()
        {
            var userInfo = HttpContext.Current.Session[SessionKey.UserInfo];
            if (userInfo != null)
            {
                return (EnterpriseUser)userInfo;
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies[SessionKey.SaleUserLoginCookie];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                string des = DesHelper.Decrypt(cookie.Value, SessionKey.ManagerUserLoginCookieKey);
                string[] tmpArr = des.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (tmpArr.Length == 2)
                {
                    string userName = tmpArr[0];
                    string passWord = tmpArr[1];
                    //当前景区下未停用的售票员
                    var model = Get(userName, passWord);
                    if (model != null)
                    {
                        //var tbl_Scenic = Repository.First<Tbl_Scenic>(a => a.ScenicId == model.ScenicId);
                        //UserInfoDTO dto = new UserInfoDTO(model, (int)SystemType.Sale, tbl_Scenic.ScenicName);
                        //model.LastLoginTime = DateTime.Now;
                        //Repository.Update(model);
                        var dto = new EnterpriseUser
                        {
                            UserId = model.EnterpriseUserId,
                            UserName = model.UserName,
                            RealName = model.RealName,
                            ScenicId = model.ScenicId
                        };
                        //设置Session
                        HttpContext.Current.Session[SessionKey.UserInfo] = dto;
                        return dto;
                    }
                }
            }
            return null;
        }
    }
}