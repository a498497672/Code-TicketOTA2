using System;
using System.Web;
using Ticket.Core.Service;
using Ticket.Model.Model.OtaBusinessUser;
using Ticket.Model.Result;
using Ticket.Utility.Helpers;
using Ticket.Utility.Key;

namespace Ticket.DistributorPlatform.Application
{
    public class LoginFacadeService
    {
        private readonly OtaBusinessUserService _otaBusinessUserService;
        private readonly OtaBusinessService _otaBusinessService;
        public LoginFacadeService(
            OtaBusinessUserService otaBusinessUserService,
            OtaBusinessService otaBusinessService)
        {
            _otaBusinessUserService = otaBusinessUserService;
            _otaBusinessService = otaBusinessService;
        }

        public TResult<LoginUser> Login(UserLoginModel userLoginModel)
        {
            var result = new TResult<LoginUser>();
            var code = HttpContext.Current.Request.Cookies[SessionKey.ManagerUserLoginCode];
            if (code == null || (string.IsNullOrEmpty(code.Value)))
            {
                return result.FailureResult(null, "验证码已过期，请重新输入验证码！");
            }
            if (!String.Equals(code.Value, userLoginModel.Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                return result.FailureResult(null, "请输入正确的验证码！");
            }
            var passWord = Md5HashHelper.HashPassword(userLoginModel.PassWord + SessionKey.CloudUserInfo);
            var adminUser = _otaBusinessUserService.Get(userLoginModel.UserName, passWord);
            if (adminUser == null)
            {
                return result.FailureResult(null, "用户名或密码不正确！");
            }
            //adminUser.LastLoginTime = DateTime.Now;
            //_otaBusinessUserService.Update(adminUser);
            var businesss = _otaBusinessService.Get(adminUser.OtaBusinessId);
            var userInfo = new LoginUser
            {
                Id = adminUser.Id,
                OtaBusinessId = adminUser.OtaBusinessId,
                UserName = adminUser.UserName,
                RealName = businesss.FullName
            };

            HttpContext.Current.Session[SessionKey.ManagerUserInfo] = userInfo;
            return result.SuccessResult(userInfo, "登陆成功");
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

        public TResult Update(UserUpdateModel model)
        {
            var result = new TResult();
            var passWord = Md5HashHelper.HashPassword(model.PassWord + SessionKey.CloudUserInfo);
            var adminUser = _otaBusinessUserService.Get(model.UserName, passWord);
            if (adminUser == null)
            {
                return result.FailureResult("旧密码不正确");
            }
            adminUser.PassWord = Md5HashHelper.HashPassword(model.NowPassWord + SessionKey.CloudUserInfo);
            _otaBusinessUserService.Update(adminUser);
            return result.SuccessResult();
        }
    }
}
