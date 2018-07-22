using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.DistributorPlatform.App_Start;
using Ticket.DistributorPlatform.Application;
using Ticket.Model.Model.OtaBusinessUser;
using Ticket.Utility.Exceptions;

namespace Ticket.DistributorPlatform.Controllers
{
    public class HomeController : BaseController
    {
        private readonly LoginFacadeService _loginFacadeService;

        public HomeController(LoginFacadeService loginFacadeService)
        {
            _loginFacadeService = loginFacadeService;
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.name = UserInfo.RealName;
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult UpdateUser()
        {
            return View(UserInfo);
        }

        public ActionResult UpdateUserData(UserUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            model.Id = UserInfo.Id;
            model.UserName = UserInfo.UserName;
            var result = _loginFacadeService.Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}