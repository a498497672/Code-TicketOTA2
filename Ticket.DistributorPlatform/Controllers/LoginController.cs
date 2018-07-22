using System.Web.Mvc;
using Ticket.DistributorPlatform.App_Start;
using Ticket.DistributorPlatform.Application;
using Ticket.Model.Model.OtaBusinessUser;
using Ticket.Model.Result;
using Ticket.Utility.Exceptions;

namespace Ticket.DistributorPlatform.Controllers
{
    /// <summary>
    /// 旅行社后台下单登录
    /// </summary>
    public class LoginController : BaseController
    {
        private readonly LoginFacadeService _loginFacadeService;

        public LoginController(LoginFacadeService loginFacadeService)
        {
            _loginFacadeService = loginFacadeService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        //[ActivityLog("登录", "系统管理员进行帐号登录")]
        public JsonResult PostLogin(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            var result = _loginFacadeService.Login(userLoginModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        //[ActivityLog("注销", "系统管理员进行帐号注销")]
        public JsonResult LoginOut()
        {
            this.ClearSessionAndCookie();
            var result = new TResult();
            return Json(result.SuccessResult(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCheckCode()
        {
            _loginFacadeService.CreateCheckCodeImage();
            return Json("生成随机码成功", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetCheckCode2()
        {
            var data = _loginFacadeService.CreateCheckCodeImage2();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}