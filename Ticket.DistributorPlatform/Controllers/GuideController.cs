using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.DistributorPlatform.Application;
using Ticket.Model.Model.TravelAgency;

namespace Ticket.DistributorPlatform.Controllers
{
    public class GuideController : BaseController
    {
        private readonly GuideFacadeService _guideFacadeService;
        public GuideController(GuideFacadeService guideFacadeService)
        {
            _guideFacadeService = guideFacadeService;
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult ListData(GuideQueryModel model)
        {
            model.OTABusinessId = UserInfo.OtaBusinessId;
            var result = _guideFacadeService.GetList(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddData(GuideAddModel model)
        {
            model.OTABusinessId = UserInfo.OtaBusinessId;
            var result = _guideFacadeService.Add(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(int id)
        {
            var result = _guideFacadeService.Get(id);
            return View(result);
        }

        public ActionResult UpdateData(GuideUpdateModel model)
        {
            var result = _guideFacadeService.Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var result = _guideFacadeService.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MinList()
        {
            return View();
        }
    }
}