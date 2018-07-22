using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.DistributorPlatform.Application;
using Ticket.Model.Model.TravelAgency;
using Ticket.Model.Result;

namespace Ticket.DistributorPlatform.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderFacadeService _orderFacadeService;
        public OrderController(OrderFacadeService orderFacadeService)
        {
            _orderFacadeService = orderFacadeService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult ListData(OrderQueryModel model)
        {
            model.OTABusinessId = UserInfo.OtaBusinessId;
            var result = _orderFacadeService.GetList(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public JsonResult AddData(OrderAddModel model)
        {
            model.OtaBusinessId = UserInfo.OtaBusinessId;
            var result = _orderFacadeService.Add(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id)
        {
            var data = _orderFacadeService.Get(id);
            return View(data);
        }

        public JsonResult DetailData(int id)
        {
            var result = new TResult<OrderDetailViewModel>();
            var data = _orderFacadeService.Get(id);
            return Json(result.SuccessResult(data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var result = _orderFacadeService.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(int id)
        {
            var data = _orderFacadeService.Get(id);
            return View(data);
        }

        public JsonResult UpdateData(OrderDetailViewModel model)
        {
            var result = _orderFacadeService.Update(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cancel(int id)
        {
            var result = _orderFacadeService.OrderCancel(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}