using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Service;
using Ticket.Model.Model.EnterpriseUser;
using Ticket.Model.Result;

namespace Ticket.SaleTicketPlatform.Application
{
    public class EnterpriseUserFacadeService
    {
        private readonly EnterpriseUserService _enterpriseUserService;
        public EnterpriseUserFacadeService(EnterpriseUserService enterpriseUserService)
        {
            _enterpriseUserService = enterpriseUserService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TResult<EnterpriseUser> Login(EnterpriseUserLoginModel UserModel)
        {
            return _enterpriseUserService.Login(UserModel);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        public void CreateCheckCodeImage()
        {
            _enterpriseUserService.CreateCheckCodeImage();
        }


        /// <summary>
        /// 生成验证码
        /// </summary>
        public string CreateCheckCodeImage2()
        {
           return _enterpriseUserService.CreateCheckCodeImage2();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public TResult LoginOut()
        {
            return _enterpriseUserService.LoginOut();
        }

        public EnterpriseUser LoginForSession()
        {
            return _enterpriseUserService.LoginForSession();
        }
    }
}
