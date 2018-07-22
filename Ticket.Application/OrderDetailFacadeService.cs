using FengjingSDK461.Helpers;
using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
using System;
using Ticket.Core.Service;
using Ticket.Utility.UnitOfWorks;

namespace Ticket.Application
{
    public class OrderDetailFacadeService
    {
        private readonly OrderService _orderService;
        private readonly TicketService _ticketService;
        private readonly OrderDetailService _orderDetailService;
        private readonly SaleLogService _saleLogService;
        private readonly TicketTestingService _ticketTestingService;
        private readonly SmsService _smsService;
        private readonly RefundDetailService _refundDetailService;
        private readonly AuthorizationService _authorizationService;

        public OrderDetailFacadeService(
            OrderService orderService,
            TicketService ticketService,
            OrderDetailService orderDetailService,
            SaleLogService saleLogService,
            TicketTestingService ticketTestingService,
            SmsService smsService,
            RefundDetailService refundDetailService,
            AuthorizationService authorizationService)
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _orderDetailService = orderDetailService;
            _saleLogService = saleLogService;
            _ticketTestingService = ticketTestingService;
            _smsService = smsService;
            _refundDetailService = refundDetailService;
            _authorizationService = authorizationService;
        }

        public OrderDetailRefundResponse RefundOrderDetail(string data, string sign)
        {
            var request = Base64Helper.Base64EncodeToObject<OrderDetailRefundRequest>(data);
            var business = _authorizationService.CheckData(request, data, sign);
            return RefundOrderDetail(request.OrderDetailId);
        }

        /// <summary>
        /// 取消订单详情
        /// </summary>
        /// <param name="orderInfo"></param>
        public OrderDetailRefundResponse RefundOrderDetail(int orderDetailId)
        {
            OrderDetailRefundResponse result = new OrderDetailRefundResponse();
            //判断门票是否可以退票和过了退票有效期
            var checkResult = _orderDetailService.CheckOrderDetailIsCanncel(orderDetailId);
            if (!checkResult.Status)
            {
                //坚持未通过
                result.Message = checkResult.Message;
                result.Code = checkResult.Code;
                return result;
            }
            try
            {
                _orderService.BeginTran();
                //添加退款记录
                var tbl_RefundDetail = _refundDetailService.Add(checkResult.Item);
                //更新订单详情的状态：为已退款
                _orderDetailService.UpdateOrderDetailForRefund(checkResult.Item);
                //更新票的日售票数
                _ticketService.UpdateTicketBySellCount(checkResult.Item);
                //退激活票时，同步删除验票表存在的数据
                _ticketTestingService.Delete(orderDetailId);
                //添加日志
                _saleLogService.Add(tbl_RefundDetail);
                //提交事物
                _orderService.CommitTran();


                //电子票发送退票短信
                //var statusResult = _smsService.Send(checkResult.Item, checkResult.Item.Mobile);
                //if (statusResult.Status)
                //{
                result.Status = true;
                result.Message = "订单详情退款成功";
                return result;
                //}
                //result.Message = "订单详情退款成功，退款短信发送失败";
                //result.Status = true;
                //return result;
            }
            catch (Exception ex)
            {
                _orderService.RollbackTran();
                result.Message = "订单详情退款成功，退款短信发送失败";
                result.Status = true;
                return result;
            }
        }
    }
}
