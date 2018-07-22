using System;
using System.Web.Mvc;
using Ticket.Model.Model.Ticket;
using Ticket.SaleTicketPlatform.Application;

namespace Ticket.SaleTicketPlatform.Controllers
{
    public class TicketController : BaseController
    {
        private readonly TicketFacadeService _ticketFacadeService;

        public TicketController(TicketFacadeService ticketFacadeService, EnterpriseUserFacadeService enterpriseUserFacadeService) : base(enterpriseUserFacadeService)
        {
            _ticketFacadeService = ticketFacadeService;
        }

        /// <summary>
        /// 获取票种列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTicketType()
        {
            var data = _ticketFacadeService.GetTicketType();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取散客门票列表
        /// </summary>
        /// <param name="playTime">游玩时间（yyyy-MM-dd）</param>
        /// <param name="ticketTypeId">票种Id</param>
        /// <param name="page">当前页（默认1）</param>
        /// <param name="pageSize">页容量（默认100）</param>
        /// <returns></returns>
        public JsonResult GetTicketList(DateTime playTime, int ticketTypeId, int page = 1, int pageSize = 100)
        {
            var data = _ticketFacadeService.GetList(new TblTicketQueryModel
            {
                Page = page,
                PageSize = pageSize,
                PlayTime = playTime,
                TicketTypeId = ticketTypeId
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}