using System.Text;
using System.Web.Mvc;
using Ticket.Model.Model.EnterpriseUser;
using Ticket.Model.Result;
using Ticket.SaleTicketPlatform.App_Start;
using Ticket.SaleTicketPlatform.Application;
using Ticket.Utility.Exceptions;
using Ticket.Utility.Key;

namespace Ticket.SaleTicketPlatform.Controllers
{
    public class BaseController : Controller
    {
        private readonly EnterpriseUserFacadeService _enterpriseUserFacadeService;
        public BaseController(EnterpriseUserFacadeService enterpriseUserFacadeService)
        {
            _enterpriseUserFacadeService = enterpriseUserFacadeService;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomsJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            var exceptionType = exception.GetType();
            //对于Ajax请求，直接返回一个用于封装异常的JsonResult
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                TResult result = new TResult();
                if (exceptionType == typeof(SimpleBadRequestException))
                {
                    filterContext.Result = Json(result.FailureResult(exception.Message), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    filterContext.Result = Json(result.ErrorResult("服务器错误，请联系管理员！"), JsonRequestBehavior.AllowGet);
                }
                //当结果为json时，设置异常已处理
                filterContext.ExceptionHandled = true;
            }
            else
            {
                //否则调用原始设置
                base.OnException(filterContext);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (controllerName == "account" && (actionName == "login" || actionName == "getcheckcode"))
            {
                return;
            }
            var isAjaxRequst = filterContext.HttpContext.Request.IsAjaxRequest();
            #region 验证登录
            TResult result = new TResult();
            if (UserInfo == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = Json(result.RequestAuthorizeResult("登录失效,请刷新页面"), JsonRequestBehavior.AllowGet);
                    return;
                }
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            #endregion
        }


        /// <summary>
        /// 用户信息
        /// </summary>
        public EnterpriseUser UserInfo
        {
            set
            {
                System.Web.HttpContext.Current.Session[SessionKey.UserInfo] = value;
            }
            get
            {
                return _enterpriseUserFacadeService.LoginForSession();
            }
        }
    }
}