using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ticket.DistributorPlatform.App_Start;
using Ticket.Model.Model.OtaBusinessUser;
using Ticket.Model.Result;
using Ticket.Utility.Exceptions;
using Ticket.Utility.Key;

namespace Ticket.DistributorPlatform.Controllers
{
    public class BaseController : Controller
    {
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
                    filterContext.Result = Json(result.FailureResult("服务器错误，请联系管理员！"), JsonRequestBehavior.AllowGet);
                }
                //当结果为json时，设置异常已处理
                filterContext.ExceptionHandled = true;
            }
            else
            {
                //ErrorMessage msg = new ErrorMessage(filterContext.Exception, "页面");
                //msg.ShowException = MvcException.IsExceptionEnabled();

                ////错误记录

                ////设置为true阻止golbal里面的错误执行
                //filterContext.ExceptionHandled = true;
                //filterContext.Result = new ViewResult()
                //{
                //    ViewName = "/Views/Error/ISE.cshtml",
                //    ViewData = new ViewDataDictionary<ErrorMessage>(msg)
                //};

                //否则调用原始设置
                base.OnException(filterContext);
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (controllerName == "login")
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
                    filterContext.Result = Json(result.FailureResult("登录失效,请刷新页面", "304"), JsonRequestBehavior.AllowGet);
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
        public LoginUser UserInfo
        {
            set
            {
                System.Web.HttpContext.Current.Session[SessionKey.ManagerUserInfo] = value;
            }
            get
            {
                LoginUser data = System.Web.HttpContext.Current.Session[SessionKey.ManagerUserInfo] as LoginUser;
                return data;
            }
        }

        /// <summary>
        /// 清楚SESSION and cookie
        /// </summary>
        public void ClearSessionAndCookie()
        {
            System.Web.HttpContext.Current.Session.Clear();

            HttpCookie hc1 = new HttpCookie(SessionKey.ManagerUserLoginCode, null);
            hc1.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(hc1);

            System.Web.HttpContext.Current.Request.Cookies.Clear();
        }
    }
}