using FengjingSDK461.Enum;
using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
using FengjingSDK461.Model.Result;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Ticket.Core.Service;
using Ticket.SqlSugar.Models;
using Ticket.Utility.Config;
using Ticket.Utility.Extensions;
using Ticket.Utility.Helpers;

namespace Ticket.Application
{
    public class OrderFacadeService
    {
        private readonly OrderService _orderService;
        private readonly TicketService _ticketService;
        private readonly OrderDetailService _orderDetailService;
        private readonly SaleLogService _saleLogService;
        private readonly TicketTestingService _ticketTestingService;
        private readonly SmsService _smsService;
        private readonly AuthorizationService _authorizationService;
        private readonly RefundDetailService _refundDetailService;
        private readonly OtaTicketRelationService _otaTicketRelationService;
        private readonly NoticeOrderConsumedService _noticeOrderConsumedService;
        private readonly OrderTravelNoticeService _orderTravelNoticeService;

        public OrderFacadeService(
            OrderService orderService,
            TicketService ticketService,
            OrderDetailService orderDetailService,
            SaleLogService saleLogService,
            TicketTestingService ticketTestingService,
            SmsService smsService,
            AuthorizationService authorizationService,
            RefundDetailService refundDetailService,
            OtaTicketRelationService otaTicketRelationService,
            NoticeOrderConsumedService noticeOrderConsumedService,
            OrderTravelNoticeService orderTravelNoticeService)
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _orderDetailService = orderDetailService;
            _saleLogService = saleLogService;
            _ticketTestingService = ticketTestingService;
            _smsService = smsService;
            _authorizationService = authorizationService;
            _refundDetailService = refundDetailService;
            _otaTicketRelationService = otaTicketRelationService;
            _noticeOrderConsumedService = noticeOrderConsumedService;
            _orderTravelNoticeService = orderTravelNoticeService;
        }

        /// <summary>
        /// 创建订单并支付
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        public PageResult PayOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderCreateRequest(data);
            if (request == null)
            {
                return PageDataResult.Fault();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.Fault();
            }
            return PayOrder(request, business);
        }

        /// <summary>
        /// 创建订单并支付
        /// </summary>
        /// <param name="orderInfo"></param>
        private PageResult PayOrder(OrderCreateRequest request, Tbl_OTABusiness business)
        {
            OrderInfo orderInfo = request.Body.OrderInfo;
            OrderCreateResponse result = new OrderCreateResponse
            {
                Head = HeadResult.V1
            };
            var validResult = _orderService.ValidDataForOrderCreateRequest(request, result);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            List<int> productIds = orderInfo.TicketList.Select(a => a.ProductId).ToList();
            var ticketIds = _otaTicketRelationService.GetTicketIds(business.Id, productIds);
            var tbl_Tickets = _ticketService.CheckIsTicketIds(ticketIds, business.ScenicId, orderInfo.VisitDate.ToDataTime());

            var validDataResult = _orderService.ValidDataForOrderCreateRequest(request, tbl_Tickets);
            if (!validDataResult.Status)
            {
                result.Head.Code = validDataResult.Code;
                result.Head.Describe = validDataResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.AddOrder(orderInfo, business);
            var tbl_OrderDetails = _orderDetailService.AddOrderDetail(orderInfo, tbl_Order);
            _orderService.UpdateOrder(tbl_Order, tbl_OrderDetails);
            var tbl_Ticket_Testing = _ticketTestingService.addTicketTestings(tbl_Order, tbl_OrderDetails);
            _ticketService.UpdateTicketBySellCount(tbl_Tickets, tbl_OrderDetails);
            var tbl_SaleLog = _saleLogService.addSaleLog(tbl_Order);

            try
            {
                _orderService.BeginTran();
                _orderService.Add(tbl_Order);
                _orderDetailService.Add(tbl_OrderDetails);
                _ticketTestingService.Add(tbl_Ticket_Testing);
                _ticketService.Update(tbl_Tickets);
                _noticeOrderConsumedService.Add(tbl_Order, tbl_OrderDetails, business);
                _orderTravelNoticeService.Add(tbl_Order, business);
                _saleLogService.Add(tbl_Order);
                _orderService.CommitTran();
            }
            catch (Exception ex)
            {
                _orderService.RollbackTran();
                result.Head.Code = "113021";
                result.Head.Describe = "订单创建异常，订单创建失败";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Body = new OrderCreateInfo
            {
                OtaOrderId = tbl_Order.OTAOrderNo,
                OrderId = tbl_Order.OrderNo,
                OrderStatus = "OREDER_SUCCESS",
                Item = new List<OrderCreateItem>()
            };
            tbl_OrderDetails = _orderDetailService.GetList(tbl_Order.OrderNo);
            foreach (var row in tbl_OrderDetails)
            {
                result.Body.Item.Add(new OrderCreateItem
                {
                    OtaOrderDetailId = row.OtaOrderDetailId,
                    ProductId = row.TicketId.ToString(),
                    useDate = row.ValidityDateStart.ToString("yyyy-MM-dd"),
                    CertificateNo = row.CertificateNO,
                    quantity = 500000
                });
            }
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public PageResult CancelOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderCancelRequest(data);
            if (request == null)
            {
                return PageDataResult.Fault();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.Fault();
            }
            return CancelOrder(request, business);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderInfo"></param>
        private PageResult CancelOrder(OrderCancelRequest request, Tbl_OTABusiness business)
        {
            string orderId = request.Body.OrderInfo.OrderId;
            OrderCancelResponse result = new OrderCancelResponse
            {
                Head = HeadResult.V1
            };
            var validResult = _orderService.ValidDataForOrderCancelRequest(request, business.Id);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.Get(orderId);
            var checkResult = _orderDetailService.CheckOrderDetailIsCanncel(orderId);
            if (!checkResult.Status)
            {
                result.Head.Code = checkResult.Code;
                result.Head.Describe = checkResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            try
            {
                _orderService.BeginTran();
                foreach (var row in checkResult.List)
                {
                    //添加退款记录
                    var tbl_RefundDetail = _refundDetailService.Add(row);
                    //更新订单详情的状态：为已退款
                    _orderDetailService.UpdateOrderDetailForRefund(row);
                    //更新票的日售票数
                    _ticketService.UpdateTicketBySellCount(row);
                    //添加日志
                    _saleLogService.Add(tbl_RefundDetail);
                }
                //退激活票时，同步删除验票表存在的数据
                _ticketTestingService.Delete(tbl_Order.OrderNo);
                //提交事物
                _orderService.CommitTran();
            }
            catch
            {
                _orderService.RollbackTran();
                result.Head.Code = "114012";
                result.Head.Describe = "订单取消失败，系统出错";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Body = new FengjingSDK461.Model.Response.OrderCancelInfo
            {
                Message = "成功"
            };
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        /// <summary>
        /// 取消订单项
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public PageResult CancelOrderDetail(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderCancelRequest(data);
            if (request == null)
            {
                return PageDataResult.Fault();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.Fault();
            }
            return CancelOrderDetail(request, business);
        }

        /// <summary>
        /// 取消订单项
        /// </summary>
        /// <param name="orderInfo"></param>
        private PageResult CancelOrderDetail(OrderCancelRequest request, Tbl_OTABusiness business)
        {
            string orderId = request.Body.OrderInfo.OrderId;
            string otaOrderId = request.Body.OrderInfo.OtaOrderId;
            OrderCancelResponse result = new OrderCancelResponse
            {
                Head = HeadResult.V1
            };
            var validResult = _orderService.ValidDataForOrderCancelRequest(request, business.Id);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.Get(orderId, otaOrderId);
            if (tbl_Order == null)
            {
                result.Head.Code = "114004";
                result.Head.Describe = "订单取消失败，订单不存在";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var checkResult = _orderDetailService.CheckOrderDetailIsCanncel(request, orderId);
            if (!checkResult.Status)
            {
                result.Head.Code = checkResult.Code;
                result.Head.Describe = checkResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            try
            {
                if (checkResult.List.Count > 0)
                {
                    _orderService.BeginTran();
                    foreach (var row in checkResult.List)
                    {
                        //添加退款记录
                        var tbl_RefundDetail = _refundDetailService.Add(row);
                        //更新订单详情的状态：为已取消
                        _orderDetailService.UpdateOrderDetailForCanncel(row);
                        //退激活票时，同步删除验票表存在的数据
                        _ticketTestingService.Delete(row.Number);
                        //更新票的日售票数
                        _ticketService.UpdateTicketBySellCount(row);
                        //添加日志
                        _saleLogService.Add(tbl_RefundDetail);
                    }
                    //提交事物
                    _orderService.CommitTran();
                }
            }
            catch
            {
                _orderService.RollbackTran();
                result.Head.Code = "114012";
                result.Head.Describe = "订单取消失败，系统出错";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Body = new FengjingSDK461.Model.Response.OrderCancelInfo
            {
                Message = "成功"
            };
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public PageResult QueryOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderQueryRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            return QueryOrder(request, business);
        }

        private PageResult QueryOrder(OrderQueryRequest request, Tbl_OTABusiness business)
        {
            string orderId = request.Body.OrderId;
            OrderQueryResponse result = new OrderQueryResponse
            {
                Head = HeadResult.V1,
                Body = new OrderQueryBody()
            };
            var validResult = _orderService.ValidDataForOrderQueryRequest(request);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.Get(orderId, request.Body.OtaOrderId);
            if (tbl_Order == null)
            {
                result.Head.Code = "115002";
                result.Head.Describe = "查询订单异常，订单不存在";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Body.OrderInfo = new OrderQueryInfo
            {
                OrderId = tbl_Order.OrderNo,
                OrderQuantity = tbl_Order.BookCount,
                OrderPrice = tbl_Order.TotalAmount,
                VisitDate = tbl_Order.ValidityDateEnd.ToString("yyyy-MM-dd"),
                ContactPerson = new OrderQueryContactPerson
                {
                    Name = tbl_Order.Linkman,
                    Mobile = tbl_Order.Mobile,
                    CardNo = tbl_Order.IDCard,
                    CardType = ((CredentialsStatus)tbl_Order.IDType).GetDescriptionByName()
                },
                EticketInfo = new List<OrderQueryTicketInfo>()
            };
            var orderDetails = _orderDetailService.GetList(tbl_Order.OrderNo);
            var tickets = _ticketService.GetTickets(orderDetails.Select(a => a.TicketId).ToList());
            foreach (var row in orderDetails)
            {
                var ticket = tickets.FirstOrDefault(a => a.TicketId == row.TicketId);
                var orderQueryTicketInfo = new OrderQueryTicketInfo
                {
                    OtaOrderDetailId = row.OtaOrderDetailId,
                    EticektNo = row.CertificateNO,
                    SellPrice = row.Price,
                    MarketPrice = ticket == null ? 0 : ticket.MarkPrice.Value,
                    EticektSend = 1,
                    ProductId = row.TicketId,
                    ProductName = row.TicketName,
                    EticketQuantity = row.Quantity,
                    UseQuantity = row.UsedQuantity == null ? 0 : row.UsedQuantity.Value,
                    CreateTime = row.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    CancelTime = row.CancelTime.HasValue ? row.CancelTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    DelayCheckTime = row.DelayCheckTime.HasValue ? row.DelayCheckTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    UseEndDate = row.ValidityDateStart.ToString("yyyy-MM-dd"),
                    UseStartDate = row.ValidityDateEnd.ToString("yyyy-MM-dd"),
                };
                _orderDetailService.GetOrderDetailsDataStatus(row, orderQueryTicketInfo);
                result.Body.OrderInfo.EticketInfo.Add(orderQueryTicketInfo);
            }
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        /// <summary>
        /// 修改订单详情
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public PageResult UpdateOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderUpdateRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            return UpdateOrder(request, business);
        }

        private PageResult UpdateOrder(OrderUpdateRequest request, Tbl_OTABusiness business)
        {
            string orderId = request.Body.OrderInfo.OrderId;
            OrderUpdateResponse result = new OrderUpdateResponse
            {
                Head = HeadResult.V1,
            };
            var validResult = _orderService.ValidDataForOrderUpdateRequest(request);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.Get(orderId);
            if (tbl_Order == null)
            {
                result.Head.Code = "116011";
                result.Head.Describe = "修改订单异常，订单不存在";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            if (!tbl_Order.CanModify)
            {
                result.Head.Code = "116012";
                result.Head.Describe = "修改订单异常，订单不能修改";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            if (tbl_Order.CanModify && tbl_Order.CanModifyTime < request.Body.OrderInfo.VisitDate.ToDataTime())
            {
                result.Head.Code = "116013";
                result.Head.Describe = "修改订单异常，填写的游玩时间超过产品最后修改时间";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            try
            {
                _orderService.BeginTran();
                _orderService.UpdateOrder(tbl_Order, request);
                _orderDetailService.UpdateOrderDetail(request);
                _orderService.CommitTran();
                result.Body = new OrderUpdateResponseBody()
                {
                    OrderId = tbl_Order.OrderNo
                };
            }
            catch (Exception ex)
            {
                _orderService.RollbackTran();
                result.Head.Code = "116014";
                result.Head.Describe = "修改订单异常，订单修改失败";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        /// <summary>
        /// 创建单个产品订单并支付
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        public PageResult PaySingleOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderSingleCreateRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            return PaySingleOrder(request, business);
        }

        /// <summary>
        /// 创建单个产品订单并支付
        /// </summary>
        /// <param name="request"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        private PageResult PaySingleOrder(OrderSingleCreateRequest request, Tbl_OTABusiness business)
        {
            var orderInfo = request.Body.OrderInfo;
            OrderSingleCreateResponse result = new OrderSingleCreateResponse
            {
                Head = HeadResult.V1,
                Body = new OrderSingleCreateInfo
                {
                    Inventory = 0
                }
            };
            var validResult = _orderService.ValidDataForOrderSingleCreateRequest(request, result);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Ticket = _ticketService.GetTicket(orderInfo.Ticket.ProductId, business.ScenicId, orderInfo.VisitDate.ToDataTime());
            var validDataResult = _ticketService.ValidDataForOrderSingleCreateRequest(request, business, tbl_Ticket, result);
            if (!validDataResult.Status)
            {
                result.Head.Code = validDataResult.Code;
                result.Head.Describe = validDataResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.AddOrder(orderInfo, business);
            var tbl_OrderDetail = _orderDetailService.AddOrderDetail(orderInfo, tbl_Order, tbl_Ticket);
            _orderService.UpdateOrder(tbl_Order, tbl_OrderDetail);
            var tbl_Ticket_Testing = _ticketTestingService.AddTicketTesting(tbl_Order, tbl_OrderDetail);
            try
            {
                _orderService.BeginTran();
                _orderService.Add(tbl_Order);
                _orderDetailService.Add(tbl_OrderDetail);
                _ticketTestingService.Add(tbl_Ticket_Testing);
                _ticketService.UpdateTicketBySellCount(tbl_Ticket, tbl_OrderDetail);
                _noticeOrderConsumedService.Add(tbl_Order, tbl_OrderDetail, business);
                _saleLogService.Add(tbl_Order);
                _orderService.CommitTran();
            }
            catch (Exception ex)
            {
                _orderService.RollbackTran();
                result.Head.Code = "113021";
                result.Head.Describe = "订单创建异常，订单创建失败";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            result.Body.OrderId = tbl_Order.OrderNo;
            result.Body.OtaOrderId = tbl_Order.OTAOrderNo;
            result.Body.CertificateNo = tbl_OrderDetail.CertificateNO;
            result.Body.Code = tbl_OrderDetail.QRcode;
            result.Body.OrderStatus = "OREDER_SUCCESS";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }


        /// <summary>
        /// 下单验证接口
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        public PageResult VerifyOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderCreateRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            return VerifyOrder(request, business);
        }

        /// <summary>
        /// 下单验证接口
        /// </summary>
        /// <param name="request"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        private PageResult VerifyOrder(OrderCreateRequest request, Tbl_OTABusiness business)
        {
            var orderInfo = request.Body.OrderInfo;
            OrderCreateResponse result = new OrderCreateResponse
            {
                Head = HeadResult.V1,
                Body = new OrderCreateInfo
                {
                    Item = new List<OrderCreateItem>()
                }
            };
            var validResult = _orderService.ValidDataForOrderVerifyRequest(request);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            List<int> productIds = orderInfo.TicketList.Select(a => a.ProductId).ToList();
            var ticketIds = _otaTicketRelationService.GetTicketIds(business.Id, productIds);
            var tbl_Tickets = _ticketService.CheckIsTicketIds(ticketIds, business.ScenicId, orderInfo.VisitDate.ToDataTime());

            var validDataResult = _orderService.ValidDataForOrderCreateRequest(request, tbl_Tickets);
            if (!validDataResult.Status)
            {
                result.Head.Code = validDataResult.Code;
                result.Head.Describe = validDataResult.Message;
                result.Body.Item.Add(new OrderCreateItem
                {
                    ProductId = request.Body.OrderInfo.TicketList[0].ProductId.ToString(),
                    useDate = request.Body.OrderInfo.VisitDate,
                    quantity = 0
                });
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }

            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            result.Body.OrderStatus = "OREDER_SUCCESS";
            foreach (var row in tbl_Tickets)
            {
                result.Body.Item.Add(new OrderCreateItem
                {
                    ProductId = row.TicketId.ToString(),
                    useDate = request.Body.OrderInfo.VisitDate,
                    quantity = 500000
                });
            }

            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        /// <summary>
        /// 下单验证接口--单个产品
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        public PageResult VerifySingleOrder(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForOrderSingleCreateRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            return VerifySingleOrder(request, business);
        }

        /// <summary>
        /// 下单验证接口--单个产品
        /// </summary>
        /// <param name="request"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        private PageResult VerifySingleOrder(OrderSingleCreateRequest request, Tbl_OTABusiness business)
        {
            var orderInfo = request.Body.OrderInfo;
            OrderSingleCreateResponse result = new OrderSingleCreateResponse
            {
                Head = HeadResult.V1,
                Body = new OrderSingleCreateInfo
                {
                    Inventory = 0
                }
            };
            var validResult = _orderService.ValidDataForVerifyOrderRequest(request);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Ticket = _ticketService.GetTicket(orderInfo.Ticket.ProductId, business.ScenicId, orderInfo.VisitDate.ToDataTime());
            var validDataResult = _ticketService.ValidDataForOrderSingleCreateRequest(request, business, tbl_Ticket, result);
            if (!validDataResult.Status)
            {
                result.Head.Code = validDataResult.Code;
                result.Head.Describe = validDataResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            result.Body.OrderStatus = "OREDER_SUCCESS";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }
    }
}
