using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.DistributorPlatform.Application;
using Ticket.Model.Model.TravelAgency;

namespace Ticket.DistributorPlatform.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketFacadeService _ticketFacadeService;
        public TicketController(TicketFacadeService ticketFacadeService)
        {
            _ticketFacadeService = ticketFacadeService;
        }

        // GET: Ticket
        public ActionResult List(string playDate)
        {
            ViewBag.PlayDate = playDate;
            return View();
        }

        public ActionResult ListData(TicketQueryModel model)
        {
            model.OTABusinessId = UserInfo.OtaBusinessId;
            var result = _ticketFacadeService.GetPageList(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}