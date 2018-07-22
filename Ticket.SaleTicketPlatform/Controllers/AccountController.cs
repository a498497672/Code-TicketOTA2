using System.Web.Mvc;
using Ticket.Model.Model.EnterpriseUser;
using Ticket.Model.Result;
using Ticket.SaleTicketPlatform.App_Start;
using Ticket.SaleTicketPlatform.Application;
using Ticket.Utility.Exceptions;

namespace Ticket.SaleTicketPlatform.Controllers
{
    public class AccountController : BaseController
    {
        private readonly EnterpriseUserFacadeService _enterpriseUserFacadeService;

        public AccountController(EnterpriseUserFacadeService enterpriseUserFacadeService) : base(enterpriseUserFacadeService)
        {
            _enterpriseUserFacadeService = enterpriseUserFacadeService;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCheckCode()
        {
            _enterpriseUserFacadeService.CreateCheckCodeImage();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCheckCode2()
        {
            var data = _enterpriseUserFacadeService.CreateCheckCodeImage2();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">数据</param>
        public JsonResult Login(EnterpriseUserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            var result = _enterpriseUserFacadeService.Login(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        public JsonResult LoginOut()
        {
            var result = _enterpriseUserFacadeService.LoginOut();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        /// <returns></returns>
        public JsonResult IsLogin()
        {
            var result = new TResult().SuccessResult();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}