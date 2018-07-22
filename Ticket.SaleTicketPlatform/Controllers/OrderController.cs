using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ticket.Model.Model.Order;
using Ticket.SaleTicketPlatform.App_Start;
using Ticket.SaleTicketPlatform.Application;
using Ticket.Utility.Exceptions;

namespace Ticket.SaleTicketPlatform.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderFacadeService _orderFacadeService;

        public OrderController(OrderFacadeService orderFacadeService, EnterpriseUserFacadeService enterpriseUserFacadeService) : base(enterpriseUserFacadeService)
        {
            _orderFacadeService = orderFacadeService;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public JsonResult PayOrder(OrderCreateModel order)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            var result = _orderFacadeService.PayOrder(order);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="keyword">查询内容</param>
        /// <param name="page">当前页（默认1）</param>
        /// <param name="pageSize">页容量（默认10）</param>
        /// <returns></returns>
        public JsonResult GetOrderList(string keyword = "", int page = 1, int pageSize = 10)
        {
            var data = _orderFacadeService.GetOrderList(page, pageSize, keyword);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取OTA分销订单列表
        /// </summary>
        /// <param name="keyword">查询内容</param>
        /// <param name="page">当前页（默认1）</param>
        /// <param name="pageSize">页容量（默认10）</param>
        /// <returns></returns>
        public JsonResult GetOtaOrderList(string keyword = "", int page = 1, int pageSize = 10)
        {
            var data = _orderFacadeService.GetOtaOrderList(page, pageSize, keyword);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 退票
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult RefundTicket(OrderRefundUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            var data = _orderFacadeService.RefundOrder(model.OrderDetailId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult PrintTicket(PrintModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            var data = _orderFacadeService.Print(model.OrderDetailId, model.PrintKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}