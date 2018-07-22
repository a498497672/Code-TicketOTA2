using FengjingSDK461.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Ticket.Core.Service;
using Ticket.Model.Enum;
using Ticket.SqlSugar.Models;
using Ticket.TaskEngine.Application.Enum;
using Ticket.TaskEngine.Application.Model;
using Ticket.TaskEngine.Application.OrderIssuedsService;
using Ticket.Utility.Config;
using Ticket.Utility.Extensions;
using Ticket.Utility.Helpers;

namespace Ticket.TaskEngine.Application.Service
{
    public class OrderFacadeService
    {
        private readonly OrderService _orderService;
        private readonly TicketService _ticketService;
        private readonly OrderDetailService _orderDetailService;
        private readonly SaleLogService _saleLogService;
        private readonly TicketTestingService _ticketTestingService;
        private readonly OtaTicketRelationService _otaTicketRelationService;
        private readonly OtaBusinessService _otaBusinessService;
        private readonly RefundDetailService _refundDetailService;
        private readonly XJ_OrderIssuedsSoapClient _orderIssuedsSoapClient;
        private readonly string _merCode = ConfigurationManager.AppSettings["service:MerCode"];
        private readonly string _key = ConfigurationManager.AppSettings["service:Key"];
        private readonly TicketConsumeService _ticketConsumeService;


        public OrderFacadeService(
            OrderService orderService,
            TicketService ticketService,
            OrderDetailService orderDetailService,
            SaleLogService saleLogService,
            TicketTestingService ticketTestingService,
            OtaTicketRelationService otaTicketRelationService,
            OtaBusinessService otaBusinessService,
            RefundDetailService refundDetailService,
            TicketConsumeService ticketConsumeService)
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _orderDetailService = orderDetailService;
            _saleLogService = saleLogService;
            _ticketTestingService = ticketTestingService;
            _otaTicketRelationService = otaTicketRelationService;
            _otaBusinessService = otaBusinessService;
            _refundDetailService = refundDetailService;
            _orderIssuedsSoapClient = new XJ_OrderIssuedsSoapClient();
            _ticketConsumeService = ticketConsumeService;
        }

        /// <summary>
        /// 同步旅行社订单
        /// </summary>
        public void SynchronizingOrder()
        {
            string timeStamp = DateTime.Now.GetTimeStamp();
            var sign = Md5HashHelper.HashPassword(_merCode + _key + timeStamp);
            //订单类型(4 - OTA订单, 5 - 旅行社订单)
            var order = _orderIssuedsSoapClient.GetOrderIssuedLine(_merCode, timeStamp, sign, "", "", 5);
            var resultData = JsonHelper.JsonToObject<ResultData>(order);
            if (resultData.IsTrue && resultData.ResultCode == "200")
            {
                var orderModel = JsonConvert.DeserializeObject<List<OrderModel>>(resultData.ResultJson);

                var orderList = orderModel.Where(a => a.AuditState == (int)OrderAuditState.Audited || a.AuditState == (int)OrderAuditState.OrderCancellation).Take(200).OrderByDescending(a => a.CreateTime).ToList();
                var channelCodeList = orderList.Select(a => a.ChannelCode).Distinct().ToList();
                var businessList = _otaBusinessService.GetList(channelCodeList, 2);
                foreach (var row in orderList)
                {
                    //判断分销商是否存在
                    var business = businessList.FirstOrDefault(a => a.Code == row.ChannelCode);
                    if (business == null)
                    {
                        continue;
                    }
                    var productCodeList = row.OrderItems.Select(a => a.ProductCode).Distinct().ToList();
                    var tickets = _ticketService.GetListByBusiness(productCodeList);
                    //判断产品是否存在
                    if (tickets.Count != productCodeList.Count)
                    {
                        continue;
                    }
                    XJ_Order orderInfo = PopulateOrder(row, business, tickets);
                    if (row.AuditState == (int)OrderAuditState.Audited)
                    {
                        AuditedAction(row, tickets, orderInfo);
                    }
                    if (row.AuditState == (int)OrderAuditState.OrderCancellation)
                    {
                        OrderCancellationAction(row, orderInfo);
                    }
                }
            }
        }

        private void OrderCancellationAction(OrderModel row, XJ_Order orderInfo)
        {
            //取消订单
            //验证OTA订单id是否已存在
            var otaOrder = _orderService.GetOrderBy(row.OrderNo);
            if (otaOrder != null)
            {
                foreach (var orderDetail in orderInfo.TicketList)
                {
                    if (orderDetail.State == (int)OrderDetailState.Cancel)
                    {
                        //M-已退款--退款
                        var result = RefundOrderDetail(orderDetail);
                        if (result)
                        {
                            UpdateIssuedLine(orderDetail);
                        }
                        Console.Write("\n取消旅行社订单：" + (result == true ? "成功" : "失败") + "  订单号：" + otaOrder.OTAOrderNo);
                    }
                }
            }
        }

        private void AuditedAction(OrderModel row, List<Tbl_Ticket> tickets, XJ_Order orderInfo)
        {
            //验证OTA订单id是否已存在
            var otaOrder = _orderService.GetOrderBy(row.OrderNo);
            if (otaOrder == null)
            {
                //OTA订单id已存在, 修改订单
                //订单已审核
                var isCreate = AddOrder(orderInfo, tickets);
                if (isCreate)
                {
                    foreach (var orderDetail in orderInfo.TicketList)
                    {
                        UpdateIssuedLine(orderDetail);
                    }
                }
                Console.Write("\n 同步旅行社订单：" + (isCreate == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
            }
            else
            {
                foreach (var orderDetail in orderInfo.TicketList)
                {
                    if (orderDetail.State == (int)OrderDetailState.HasChange)
                    {
                        //G-已改签--修改订单
                        var result = UpdateOrderDetail(orderDetail);
                        if (result)
                        {
                            UpdateIssuedLine(orderDetail);
                        }
                        Console.Write("\n同步旅行社订单，已改签：" + (result == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
                        continue;
                    }
                    if (orderDetail.State == (int)OrderDetailState.FullRefund)
                    {
                        //全部退票
                        var result = RefundOrderDetail(orderDetail);
                        if (result)
                        {
                            UpdateIssuedLine(orderDetail);
                        }
                        Console.Write("\n同步旅行社订单，全部退票：" + (result == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
                        continue;
                    }
                    UpdateIssuedLine(orderDetail);
                }
            }
        }

        private static XJ_Order PopulateOrder(OrderModel row, Tbl_OTABusiness business, List<Tbl_Ticket> tickets)
        {
            var orderInfo = new XJ_Order
            {
                OrderOtaId = row.OrderNo,
                OTABusinessId = business.Id,
                OrderPrice = row.Money.ToDouble(),
                OrderQuantity = row.OrderItems.Sum(a => a.Number),
                TicketList = new List<XJ_ProductItem>(),
                VisitDate = row.PlayDate.ToDataTimeFormat(),
                ContactPerson = new XJ_ContactPerson
                {
                    BuyName = row.LinkName,
                    Name = row.LinkName,
                    Mobile = row.LinkPhone,
                    CardType = "ID_CARD",
                    CardNo = row.LinkIdCard
                }
            };
            foreach (var item in row.OrderItems)
            {
                var ticket = tickets.FirstOrDefault(a => a.Code == item.ProductCode);
                orderInfo.TicketList.Add(new XJ_ProductItem
                {
                    ProductId = ticket.TicketId,
                    ProductName = ticket.TicketName,
                    SellPrice = ticket.SalePrice,
                    Quantity = item.Number,
                    OrderNo = item.OrderNo,
                    OrderDetailId = item.ItemId.ToString(),
                    CodeStr = item.Code,
                    StartDate = item.StartDate.ToDataTimeFormat(),
                    EndDate = item.EndDate.ToDataTimeFormat(),
                    State = StateAction.GetState(item.State)
                });
            }

            return orderInfo;
        }

        /// <summary>
        /// 同步OTA订单
        /// </summary>
        public void SynchronizingOtaOrder()
        {
            XJ_OrderIssuedsSoapClient client = new XJ_OrderIssuedsSoapClient();
            string timeStamp = DateTime.Now.GetTimeStamp();
            var sign = Md5HashHelper.HashPassword(_merCode + _key + timeStamp);
            //订单类型(4 - OTA订单, 5 - 旅行社订单)
            var order = client.GetOrderIssuedLine(_merCode, timeStamp, sign, "", "", 4);
            var resultData = JsonHelper.JsonToObject<ResultData>(order);
            if (resultData.IsTrue && resultData.ResultCode == "200")
            {
                var orderModel = JsonConvert.DeserializeObject<List<OtaOrderModel>>(resultData.ResultJson);
                var orderList = orderModel.Take(200).OrderByDescending(a => a.CreateTime).ToList();
                var channelCodeList = orderList.Select(a => a.ChannelCode).Distinct().ToList();
                var businessList = _otaBusinessService.GetList(channelCodeList, 1);
                var productCodeList = orderList.Select(a => a.ProductCode).Distinct().ToList();
                var tickets = _ticketService.GetListByBusiness(productCodeList);

                foreach (var row in orderList)
                {
                    //判断分销商是否存在
                    var business = businessList.FirstOrDefault(a => a.Code == row.ChannelCode);
                    if (business == null)
                    {
                        continue;
                    }
                    var ticket = tickets.FirstOrDefault(a => a.Code == row.ProductCode);
                    //判断产品是否存在
                    if (ticket == null)
                    {
                        continue;
                    }

                    XJ_Order orderInfo = PopulateOTAOrder(row, business, ticket);
                    OrderOtaAction(tickets, row, orderInfo);
                }
            }
        }

        private void OrderOtaAction(List<Tbl_Ticket> tickets, OtaOrderModel row, XJ_Order orderInfo)
        {
            //验证OTA订单id是否已存在
            var otaOrder = _orderService.GetOrderBy(row.OrderNo);
            if (otaOrder == null)
            {
                if (row.OrderState != OrderDetailState.Paid.GetDescriptionByName())
                {
                    return;
                }
                //OTA订单id已存在, 修改订单
                //订单已审核
                var isCreate = AddOrder(orderInfo, tickets);
                if (isCreate)
                {
                    foreach (var orderDetail in orderInfo.TicketList)
                    {
                        UpdateIssuedLineForOTA(orderDetail);
                    }
                }
                Console.Write("\n同步OTA订单，创建订单：" + (isCreate == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
            }
            else
            {
                foreach (var orderDetail in orderInfo.TicketList)
                {
                    if (orderDetail.State == (int)OrderDetailState.HasChange)
                    {
                        //G-已改签--修改订单
                        var result = UpdateOrderDetail(orderDetail);
                        if (result)
                        {
                            UpdateIssuedLineForOTA(orderDetail);
                        }
                        Console.Write("\n同步OTA订单，已改签：" + (result == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
                        continue;
                    }
                    if (orderDetail.State == (int)OrderDetailState.FullRefund)
                    {
                        //A-全部退票
                        var result = RefundOrderDetail(orderDetail);
                        if (result)
                        {
                            UpdateIssuedLineForOTA(orderDetail);
                        }
                        Console.Write("\n同步OTA订单，全部退票：" + (result == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
                        continue;
                    }
                    if (orderDetail.State == (int)OrderDetailState.Refunded || orderDetail.State == (int)OrderDetailState.Cancel)
                    {
                        //取消订单
                        var result = RefundOrderDetail(orderDetail);
                        if (result)
                        {
                            UpdateIssuedLineForOTA(orderDetail);
                        }
                        Console.Write("\n同步OTA订单，取消订单：" + (result == true ? "成功" : "失败") + "  订单号：" + orderInfo.OrderOtaId);
                        continue;
                    }
                    UpdateIssuedLineForOTA(orderDetail);
                }
            }
        }

        private static XJ_Order PopulateOTAOrder(OtaOrderModel row, Tbl_OTABusiness business, Tbl_Ticket ticket)
        {
            var orderInfo = new XJ_Order
            {
                OrderOtaId = row.OrderNo,
                OTABusinessId = business.Id,
                OrderPrice = row.ProductPrice,
                OrderQuantity = row.ProductCount,
                TicketList = new List<XJ_ProductItem>(),
                VisitDate = row.StartDate.ToDataTimeFormat(),
                ContactPerson = new XJ_ContactPerson
                {
                    BuyName = row.LinkName,
                    Name = row.LinkName,
                    Mobile = row.LinkPhone,
                    CardType = "ID_CARD",
                    CardNo = row.IdCard
                }
            };
            orderInfo.TicketList.Add(new XJ_ProductItem
            {
                ProductId = ticket.TicketId,
                ProductName = ticket.TicketName,
                SellPrice = ticket.SalePrice,
                Quantity = row.ProductCount,
                OrderNo = row.OrderNo,
                OrderDetailId = row.OrderId.ToString(),
                CodeStr = row.Code,
                StartDate = row.StartDate.ToDataTimeFormat(),
                EndDate = row.EndDate.ToDataTimeFormat(),
                State = StateAction.GetState(row.OrderState)
            });
            return orderInfo;
        }

        /// <summary>
        /// 渠道订单下发结果通知--旅行社订单
        /// </summary>
        /// <param name="item"></param>
        private void UpdateIssuedLine(XJ_ProductItem item)
        {
            string timeStamp = DateTime.Now.GetTimeStamp();
            var sign = Md5HashHelper.HashPassword(_merCode + _key + timeStamp);
            var update = _orderIssuedsSoapClient.UpdateIssuedLine(_merCode, item.OrderNo, item.CodeStr, sign, timeStamp, "", "", 5);
            var resultDataUpdate = JsonHelper.JsonToObject<ResultData>(update);
            if (resultDataUpdate.IsTrue && resultDataUpdate.ResultCode == "200")
            {
                //成功
            }
            Console.Write("\n渠道订单下发结果通知--旅行社订单：" + (resultDataUpdate.IsTrue == true ? "成功" : "失败"));
        }

        /// <summary>
        /// 渠道订单下发结果通知--OTA订单
        /// </summary>
        /// <param name="item"></param>
        private void UpdateIssuedLineForOTA(XJ_ProductItem item)
        {
            string timeStamp = DateTime.Now.GetTimeStamp();
            var sign = Md5HashHelper.HashPassword(_merCode + _key + timeStamp);
            var update = _orderIssuedsSoapClient.UpdateIssuedLine(_merCode, item.OrderNo, item.CodeStr, sign, timeStamp, "", "", 4);
            var resultDataUpdate = JsonHelper.JsonToObject<ResultData>(update);
            if (resultDataUpdate.IsTrue && resultDataUpdate.ResultCode == "200")
            {
                //成功
            }
            Console.Write("\n渠道订单下发结果通知--OTA订单：" + (resultDataUpdate.IsTrue == true ? "成功" : "失败"));
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="tbl_Tickets"></param>
        /// <returns></returns>
        private bool AddOrder(XJ_Order orderInfo, List<Tbl_Ticket> tbl_Tickets)
        {
            var tbl_Order = _orderService.AddOrder(orderInfo);
            var tbl_OrderDetails = _orderDetailService.AddOrderDetail(orderInfo, tbl_Order);
            _orderService.UpdateOrder(tbl_Order, tbl_OrderDetails);
            var tbl_Ticket_Testing = _ticketTestingService.XJ_addTicketTestings(tbl_Order, tbl_OrderDetails);
            _ticketService.UpdateTicketBySellCount(tbl_Tickets, tbl_OrderDetails);
            var tbl_SaleLog = _saleLogService.addSaleLog(tbl_Order);
            try
            {
                using (SqlConnection connection = new SqlConnection(DbConfig.TicketConnectionString))
                {
                    connection.Open();
                    var trans = connection.BeginTransaction();
                    SqlBulkInsert.Inert(tbl_Order, connection, trans);
                    SqlBulkInsert.Inert(tbl_OrderDetails, connection, trans);
                    SqlBulkInsert.Inert(tbl_Ticket_Testing, connection, trans);
                    _ticketService.UpdateTicket(tbl_Tickets, connection, trans);
                    SqlBulkInsert.Inert(tbl_SaleLog, connection, trans);
                    trans.Commit();
                }

                foreach (var row in tbl_OrderDetails)
                {
                    for (var i = 0; i < row.Quantity; i++)
                    {
                        _ticketConsumeService.Add(new Tbl_TicketConsume
                        {
                            OrderNo = row.OrderNo,
                            OtaOrderNo = tbl_Order.OTAOrderNo,
                            TicketTestingId = 0,
                            TicketCategory = row.TicketCategory,
                            BarCode = row.BarCode,
                            QRcode = row.QRcode,
                            OrderDetailNumber = row.Number,
                            OrderSource = (int)OrderSource.My,
                            SendStatus = false,
                            CreateTime = row.ValidityDateEnd,
                            SendCount = 0,
                            TicketId = row.TicketId
                        });
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //"订单创建异常，订单创建失败";
                return false;
            }
        }

        /// <summary>
        /// 订单详情--退款
        /// </summary>
        private bool RefundOrderDetail(XJ_ProductItem item)
        {
            //判断门票是否可以退票和过了退票有效期
            var result = _orderDetailService.XJ_CheckOrderDetailIsCanncel(item.OrderDetailId);
            if (result == null)
            {
                return false;
            }
            try
            {
                _orderService.BeginTran();
                //添加退款记录
                var tbl_RefundDetail = _refundDetailService.Add(result);
                //更新订单详情的状态：为已退款
                _orderDetailService.UpdateOrderDetailForRefund(result);
                //更新票的日售票数
                _ticketService.UpdateTicketBySellCount(result);
                //退激活票时，同步删除验票表存在的数据
                _ticketTestingService.Delete(result.OrderNo);
                //添加日志
                _saleLogService.Add(tbl_RefundDetail);
                //提交事物
                _orderService.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _orderService.RollbackTran();
                return false;
            }
        }

        /// <summary>
        /// 修改订单详情
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool UpdateOrderDetail(XJ_ProductItem item)
        {
            var result = _orderDetailService.XJ_CheckOrderDetailIsUpdate(item.OrderDetailId);
            if (result == null)
            {
                return false;
            }
            try
            {

                //_orderService.UpdateOrder(tbl_Order, orderInfo);
                _orderDetailService.XJ_UpdateOrderDetail(result, item);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
