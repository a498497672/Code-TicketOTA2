using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Service;
using Ticket.Model.Enum;
using Ticket.Model.Model.Order;
using Ticket.Model.Result;

namespace Ticket.SaleTicketPlatform.Application
{
    public class OrderFacadeService
    {
        private readonly OrderService _orderService;
        private readonly TicketService _ticketService;
        private readonly OrderDetailService _orderDetailService;
        private readonly SaleLogService _saleLogService;
        private readonly TicketTestingService _ticketTestingService;
        private readonly EnterpriseUserService _enterpriseUserService;
        private readonly PrintService _printService;
        private readonly RefundDetailService _refundDetailService;

        public OrderFacadeService(
            OrderService orderService,
            TicketService ticketService,
            OrderDetailService orderDetailService,
            SaleLogService saleLogService,
            TicketTestingService ticketTestingService,
            EnterpriseUserService enterpriseUserService,
            PrintService printService,
            RefundDetailService refundDetailService)
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _orderDetailService = orderDetailService;
            _saleLogService = saleLogService;
            _ticketTestingService = ticketTestingService;
            _enterpriseUserService = enterpriseUserService;
            _printService = printService;
            _refundDetailService = refundDetailService;
        }


        public TResult PayOrder(OrderCreateModel order)
        {
            var result = new TResult();
            if (order.TicketItem.Count <= 0)
            {
                return result.ErrorResult("请选择您要购买的产品");
            }

            var userInfo = _enterpriseUserService.LoginForSession();
            var orderInfo = new OrderInfoCreateModel
            {
                PayType = order.PayType,
                UserId = userInfo.UserId,
                ValidityDate = order.ValidityDate,
                Mobile = "",
                Linkman = "",
                TicketCategory = (int)TicketCategoryStatus.QrCodePrintTicket,
                TicketSource = (int)TicketSourceStatus.ScenicSpot,
                TicketItem = order.TicketItem
            };

            List<int> productIds = orderInfo.TicketItem.Select(a => a.TicketId).ToList();
            var tbl_Tickets = _ticketService.GetTickets(productIds);
            var tbl_Order = _orderService.AddOrder(orderInfo);
            var tbl_OrderDetails = _orderDetailService.AddOrderDetails(orderInfo, tbl_Order);
            _orderService.UpdateOrder(tbl_Order, tbl_OrderDetails);
            var tbl_Ticket_Testings = _ticketTestingService.addTicketTestings(tbl_Order, tbl_OrderDetails);
            _ticketService.UpdateTicketBySellCount(tbl_Tickets, tbl_OrderDetails);
            try
            {
                _orderService.BeginTran();
                _orderService.Add(tbl_Order);
                _orderDetailService.Add(tbl_OrderDetails);
                _ticketTestingService.Add(tbl_Ticket_Testings);
                _saleLogService.Add(tbl_Order);
                _ticketService.Update(tbl_Tickets);
                //提交事物
                _orderService.CommitTran();
            }
            catch
            {
                _orderService.RollbackTran();
                return result.ErrorResult();
            }

            //打印机打印
            if (order.IsPrint && !string.IsNullOrEmpty(order.PrintKey))
            {
                var isPrint = _printService.Print(tbl_Order.OrderNo, order.PrintKey);
                if (!isPrint.Success)
                {
                    return result.ErrorResult("门票创建成功，" + isPrint.Message);
                }
            }

            return result.SuccessResult();
        }

        public TResult Print(int orderDetailId, string printKey)
        {
            return _printService.Print(orderDetailId, printKey);
        }

        public TPageResult<OrderViewModel> GetOrderList(int page, int pageSize, string keyword)
        {
            var userInfo = _enterpriseUserService.LoginForSession();
            return _orderService.GetOrderList(page, pageSize, userInfo.UserId, keyword);
        }

        public TPageResult<OtaOrderViewModel> GetOtaOrderList(int page, int pageSize, string keyword)
        {
            var userInfo = _enterpriseUserService.LoginForSession();
            return _orderService.GetOtaOrderList(page, pageSize, userInfo.ScenicId, keyword);
        }

        /// <summary>
        /// 退票
        /// </summary>
        /// <returns></returns>
        public TResult RefundOrder(int orderDetailId)
        {
            var userInfo = _enterpriseUserService.LoginForSession();
            return _refundDetailService.RefundOrder(orderDetailId, userInfo.UserId);
        }
    }
}
