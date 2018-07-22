using FengjingSDK461.Enum;
using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Enum;
using Ticket.Model.Model;
using Ticket.Model.Result;
using Ticket.TaskEngine.Application.Model;
using Ticket.Utility.Extensions;
using Ticket.Model.Model.Order;
using SqlSugar;

namespace Ticket.Core.Service
{
    public class OrderDetailService
    {
        private readonly OrderDetailRepository _orderDetailRepository;
        private readonly TicketService _ticketService;
        private readonly TicketRuleRepository _ticketRuleRepository;

        public OrderDetailService(
            OrderDetailRepository orderDetailRepository,
            TicketService ticketService,
            TicketRuleRepository ticketRuleRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _ticketService = ticketService;
            _ticketRuleRepository = ticketRuleRepository;
        }


        public Tbl_OrderDetail Get(int id)
        {
            return _orderDetailRepository.FirstOrDefault(a => a.OrderDetailId == id);
        }

        public Tbl_OrderDetail Get(string orderNo)
        {
            return _orderDetailRepository.FirstOrDefault(a => a.OrderNo == orderNo);
        }

        public Tbl_OrderDetail Get(Guid number)
        {
            return _orderDetailRepository.FirstOrDefault(a => a.Number == number);
        }

        public Tbl_OrderDetail GetByBarCode(string barCode)
        {
            return _orderDetailRepository.FirstOrDefault(a => a.BarCode == barCode);
        }
        public OrderDetailsValidateModel GetByQRcode(string qRcode)
        {
            DateTime nowTime = DateTime.Now.Date;
            var data = _orderDetailRepository.db.Queryable<Tbl_OrderDetail, Tbl_Ticket>((a, b) =>
                new object[] {
                    JoinType.Left,a.TicketId==b.TicketId })
                .Where((a, b) =>
                    a.QRcode == qRcode &&
                    a.ValidityDateStart <= nowTime &&
                    a.ValidityDateEnd >= nowTime)
                .Select((a, b) => new OrderDetailsValidateModel
                {
                    OrderNo = a.OrderNo,
                    TicketId = a.TicketId,
                    OrderStatus = a.OrderStatus,
                    ValidityDateStart = a.ValidityDateStart,
                    ValidityDateEnd = a.ValidityDateEnd,
                    Price = a.Price,
                    DelayCheckTime = a.DelayCheckTime,
                    CheckWay = b.CheckWay
                }).First();
            return data;
        }
        public Tbl_OrderDetail GetByIDCard(string idCard)
        {
            return _orderDetailRepository.FirstOrDefault(a => a.IDCard == idCard && a.ValidityDateStart == DateTime.Now.Date);
        }

        /// <summary>
        /// 获取ota的能取票的订单项--取票机
        /// </summary>
        /// <param name="scenicId"></param>
        /// <param name="certificateNo"></param>
        /// <returns></returns>
        public Tbl_OrderDetail GetOtaByCertificateNo(int scenicId, string certificateNo)
        {
            DateTime today = DateTime.Now.Date;
            var entity = _orderDetailRepository.FirstOrDefault(a =>
            a.ScenicId == scenicId &&
            a.TicketSource == (int)TicketSourceStatus.Ota &&
            a.OrderStatus == (int)OrderDetailsDataStatus.Activate &&
            a.ValidityDateStart >= today &&
            a.ValidityDateEnd <= today &&
            a.CertificateNO == certificateNo);
            return entity;
        }

        /// <summary>
        /// 获取ota的能取票的订单项--取票机
        /// </summary>
        /// <param name="scenicId"></param>
        /// <param name="certificateNo"></param>
        /// <returns></returns>
        public List<Tbl_OrderDetail> GetOtaByIdCard(int scenicId, string idCard)
        {
            DateTime today = DateTime.Now.Date;
            var list = _orderDetailRepository.GetAllList(a =>
            a.ScenicId == scenicId &&
            a.TicketSource == (int)TicketSourceStatus.Ota &&
            a.OrderStatus == (int)OrderDetailsDataStatus.Activate &&
            a.ValidityDateStart >= today &&
            a.ValidityDateEnd <= today &&
            a.IDCard == idCard);
            return list;
        }

        public List<Tbl_OrderDetail> GetList(string orderNo)
        {
            return _orderDetailRepository.GetAllList(a => a.OrderNo == orderNo);
        }

        /// <summary>
        /// 创建订单详情
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tbl_Order"></param>
        /// <returns></returns>
        public List<Tbl_OrderDetail> AddOrderDetail(OrderInfo order, Tbl_Order tbl_Order)
        {
            List<Tbl_OrderDetail> orderDetails = new List<Tbl_OrderDetail>();
            orderDetails.AddRange(addQrCodeTicketOrderDetail(order, tbl_Order));
            return orderDetails;
        }

        public void Add(Tbl_OrderDetail tbl_OrderDetail)
        {
            _orderDetailRepository.Add(tbl_OrderDetail);
        }

        public void Add(List<Tbl_OrderDetail> tbl_OrderDetails)
        {
            _orderDetailRepository.Add(tbl_OrderDetails);
        }

        /// <summary>
        /// 创建订单详情 - 二维码票
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tbl_Order"></param>
        /// <returns></returns>
        public List<Tbl_OrderDetail> AddOrderDetails(OrderInfoCreateModel order, Tbl_Order tbl_Order)
        {
            if (order.TicketCategory == 1)
            {
                return new List<Tbl_OrderDetail>();
            }
            List<Tbl_OrderDetail> orderDetails = new List<Tbl_OrderDetail>();
            string ticketNames = string.Empty;
            decimal TotalMoney = 0;
            int TotalCount = 0;
            //企业Id
            int EnterpriseID = 0;
            int ScenicID = 0;
            foreach (var te in order.TicketItem)
            {
                var ticket = _ticketService.GetTicket(order.ValidityDate, te.TicketId);
                var ticketRule = _ticketRuleRepository.FirstOrDefault(a => a.Id == ticket.RuleId);
                //二维码票： 每种门票 一个订单详情，多个数量
                Tbl_OrderDetail tbl_OrderDetail = new Tbl_OrderDetail
                {
                    Number = Guid.NewGuid(),
                    OrderNo = tbl_Order.OrderNo,
                    EnterpriseId = ticket.EnterpriseId,
                    ScenicId = ticket.ScenicId,
                    OrderType = tbl_Order.OrderType,
                    WindowId = 0,
                    SellerId = tbl_Order.SellerId,
                    SellerType = 1,
                    OtaOrderDetailId = "",
                    OrderSource = (int)OrderSource.My,
                    TicketSource = order.TicketSource,
                    TicketCategory = order.TicketCategory,
                    UsedQuantity = 0,
                    TicketId = te.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.SalePrice,
                    Quantity = te.BookCount,
                    BarCode = "",
                    Stub = "",
                    CertificateNO = "",
                    OrderStatus = (int)OrderDetailsDataStatus.NoPay,
                    CreateTime = DateTime.Now,
                    ValidityDateStart = order.ValidityDate,
                    ValidityDateEnd = order.ValidityDate,
                    PrintCount = 0,
                    QRcodeUrl = "",
                    QRcode = "",
                    Mobile = tbl_Order.Mobile,
                    IDCard = tbl_Order.IDCard,
                    Linkman = tbl_Order.Linkman,
                    BuyUserId = tbl_Order.BuyUserId,
                    CanModify = ticketRule.CanModify,
                    CanRefund = ticketRule.CanRefund
                };
                UpdateOrderDetailRefundTimeAndModifyTime(order.ValidityDate, ticket, tbl_OrderDetail, ticketRule);
                orderDetails.Add(tbl_OrderDetail);
                //有效的门票信息
                EnterpriseID = ticket.EnterpriseId;
                ScenicID = ticket.ScenicId;
                ticketNames += ticket.TicketName + ",";
                TotalMoney += (ticket.SalePrice * te.BookCount);
                TotalCount += te.BookCount;
            }
            ticketNames = ticketNames.Substring(0, ticketNames.Length - 1);
            if (ticketNames.Length > 50)
            {
                ticketNames = ticketNames.Substring(0, 50);
            }
            tbl_Order.EnterpriseId = EnterpriseID;
            tbl_Order.ScenicId = ScenicID;
            tbl_Order.TicketName = ticketNames;
            tbl_Order.TotalAmount = TotalMoney;
            tbl_Order.BookCount = TotalCount;
            return orderDetails;
        }


        /// <summary>
        /// 创建单个产品订单详情
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tbl_Order"></param>
        /// <returns></returns>
        public Tbl_OrderDetail AddOrderDetail(OrderSingleInfo order, Tbl_Order tbl_Order, Tbl_Ticket tbl_Ticket)
        {
            var ticketRule = _ticketRuleRepository.FirstOrDefault(a => a.Id == tbl_Ticket.RuleId);
            //二维码票： 每种门票 一个订单详情，多个数量
            Tbl_OrderDetail tbl_OrderDetail = new Tbl_OrderDetail
            {
                Number = Guid.NewGuid(),
                OrderNo = tbl_Order.OrderNo,
                EnterpriseId = tbl_Ticket.EnterpriseId,
                ScenicId = tbl_Ticket.ScenicId,
                WindowId = 0,
                SellerId = 0,
                SellerType = 1,
                OtaOrderDetailId = "",
                OrderSource = (int)OrderSource.OTA,
                TicketSource = (int)TicketSourceStatus.Ota,
                TicketCategory = (int)TicketCategoryStatus.QrCodeElectronTicket,
                UsedQuantity = 0,
                TicketId = tbl_Ticket.TicketId,
                TicketName = tbl_Ticket.TicketName,
                Price = tbl_Ticket.SalePrice,
                SettlementPrice = tbl_Ticket.SettlementPrice,
                Quantity = order.Ticket.Quantity,
                BarCode = "",
                Stub = "",
                CertificateNO = "",
                OrderStatus = (int)OrderDetailsDataStatus.NoPay,
                CreateTime = DateTime.Now,
                ValidityDateStart = order.VisitDate.ToDataTime(),
                ValidityDateEnd = order.VisitDate.ToDataTime(),
                PrintCount = 0,
                QRcodeUrl = "",
                QRcode = "",
                Mobile = order.ContactPerson.Mobile,
                IDCard = tbl_Order.IDCard,
                Linkman = order.ContactPerson.Name,
                CanModify = ticketRule.CanModify,
                CanRefund = ticketRule.CanRefund
            };
            UpdateOrderDetailRefundTimeAndModifyTime(order.VisitDate.ToDataTime(), tbl_Ticket, tbl_OrderDetail, ticketRule);


            //修改订单信息
            tbl_Order.EnterpriseId = tbl_OrderDetail.EnterpriseId;
            tbl_Order.ScenicId = tbl_OrderDetail.ScenicId;
            tbl_Order.TicketName = tbl_OrderDetail.TicketName;
            tbl_Order.TotalAmount = tbl_Ticket.SalePrice * order.Ticket.Quantity;
            tbl_Order.BookCount = order.Ticket.Quantity;
            return tbl_OrderDetail;
        }

        /// <summary>
        /// 创建订单详情--小径平台
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tbl_Order"></param>
        /// <returns></returns>
        public List<Tbl_OrderDetail> AddOrderDetail(XJ_Order order, Tbl_Order tbl_Order)
        {
            List<Tbl_OrderDetail> orderDetails = new List<Tbl_OrderDetail>();
            string ticketNames = string.Empty;
            decimal TotalMoney = 0;
            int TotalCount = 0;
            //企业Id
            int EnterpriseID = 0;
            int ScenicID = 0;
            foreach (var te in order.TicketList)
            {
                var ticket = _ticketService.GetTicket(order.VisitDate, te.ProductId);
                var ticketRule = _ticketRuleRepository.FirstOrDefault(a => a.Id == ticket.RuleId);
                //二维码票： 每种门票 一个订单详情，多个数量
                Tbl_OrderDetail tbl_OrderDetail = new Tbl_OrderDetail
                {
                    Number = Guid.NewGuid(),
                    OrderNo = tbl_Order.OrderNo,
                    OtaOrderDetailId = te.OrderDetailId,
                    EnterpriseId = ticket.EnterpriseId,
                    ScenicId = ticket.ScenicId,
                    WindowId = 0,
                    SellerId = 0,
                    SellerType = 1,
                    OrderSource = (int)OrderSource.XiaoJing,
                    TicketSource = (int)TicketSourceStatus.Ota,
                    TicketCategory = (int)TicketCategoryStatus.QrCodeElectronTicket,
                    UsedQuantity = 0,
                    TicketId = te.ProductId,
                    TicketName = ticket.TicketName,
                    Price = ticket.SalePrice,
                    SettlementPrice = ticket.SettlementPrice,
                    Quantity = te.Quantity,
                    BarCode = te.CodeStr,
                    Stub = "",
                    CertificateNO = "",
                    OrderStatus = (int)OrderDetailsDataStatus.NoPay,
                    CreateTime = DateTime.Now,
                    ValidityDateStart = order.VisitDate,
                    ValidityDateEnd = order.VisitDate,
                    PrintCount = 0,
                    QRcodeUrl = "",
                    QRcode = te.CodeStr,
                    Mobile = order.ContactPerson.Mobile,
                    IDCard = tbl_Order.IDCard,
                    Linkman = order.ContactPerson.Name,
                    CanModify = ticketRule.CanModify,
                    CanRefund = ticketRule.CanRefund
                };
                UpdateOrderDetailRefundTimeAndModifyTime(order.VisitDate, ticket, tbl_OrderDetail, ticketRule);
                orderDetails.Add(tbl_OrderDetail);
                //有效的门票信息
                EnterpriseID = ticket.EnterpriseId;
                ScenicID = ticket.ScenicId;
                ticketNames += ticket.TicketName + ",";
                TotalMoney += (ticket.SalePrice * te.Quantity);
                TotalCount += te.Quantity;
            }
            ticketNames = ticketNames.Substring(0, ticketNames.Length - 1);
            if (ticketNames.Length > 50)
            {
                ticketNames = ticketNames.Substring(0, 50);
            }
            tbl_Order.EnterpriseId = EnterpriseID;
            tbl_Order.ScenicId = ScenicID;
            tbl_Order.TicketName = ticketNames;
            tbl_Order.TotalAmount = TotalMoney;
            tbl_Order.BookCount = TotalCount;
            return orderDetails;
        }

        /// <summary>
        /// 创建订单详情 - 二维码票
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tbl_Order"></param>
        /// <returns></returns>
        public List<Tbl_OrderDetail> AddOrderDetailForQrCodeNoPay(OrderInfoModel order, Tbl_Order tbl_Order)
        {
            if (order.TicketCategory == 1)
            {
                return new List<Tbl_OrderDetail>();
            }
            List<Tbl_OrderDetail> orderDetails = new List<Tbl_OrderDetail>();
            string ticketNames = string.Empty;
            decimal TotalMoney = 0;
            int TotalCount = 0;
            //企业Id
            int EnterpriseID = 0;
            int ScenicID = 0;
            foreach (var te in order.TicketItem)
            {
                var ticket = _ticketService.GetTicket(order.ValidityDate, te.TicketId);
                var ticketRule = _ticketRuleRepository.FirstOrDefault(a => a.Id == ticket.RuleId);

                //二维码票： 每种门票 一个订单详情，多个数量
                Tbl_OrderDetail tbl_OrderDetail = new Tbl_OrderDetail
                {
                    Number = Guid.NewGuid(),
                    OrderNo = tbl_Order.OrderNo,
                    EnterpriseId = ticket.EnterpriseId,
                    ScenicId = ticket.ScenicId,
                    OrderType = tbl_Order.OrderType,
                    WindowId = 0,
                    SellerId = 0,
                    SellerType = 1,
                    OtaOrderDetailId = "",
                    OrderSource = (int)OrderSource.My,
                    TicketSource = order.TicketSource,
                    TicketCategory = order.TicketCategory,
                    UsedQuantity = 0,
                    TicketId = te.TicketId,
                    TicketName = ticket.TicketName,
                    Price = ticket.SalePrice,
                    SettlementPrice = ticket.SettlementPrice,
                    Quantity = te.BookCount,
                    BarCode = "",
                    Stub = "",
                    CertificateNO = "",
                    OrderStatus = (int)OrderDetailsDataStatus.NoPay,
                    CreateTime = DateTime.Now,
                    ValidityDateStart = order.ValidityDate,
                    ValidityDateEnd = order.ValidityDate,
                    PrintCount = 0,
                    QRcodeUrl = "",
                    QRcode = "",
                    Mobile = order.Mobile,
                    IDCard = "",
                    Linkman = order.Linkman,
                    CanModify = ticketRule.CanModify,
                    CanRefund = ticketRule.CanRefund
                };
                UpdateOrderDetailRefundTimeAndModifyTime(order.ValidityDate, ticket, tbl_OrderDetail, ticketRule);
                orderDetails.Add(tbl_OrderDetail);
                //有效的门票信息
                EnterpriseID = ticket.EnterpriseId;
                ScenicID = ticket.ScenicId;
                ticketNames += ticket.TicketName + ",";
                TotalMoney += (ticket.SalePrice * te.BookCount);
                TotalCount += te.BookCount;
            }
            ticketNames = ticketNames.Substring(0, ticketNames.Length - 1);
            if (ticketNames.Length > 50)
            {
                ticketNames = ticketNames.Substring(0, 50);
            }
            tbl_Order.EnterpriseId = EnterpriseID;
            tbl_Order.ScenicId = ScenicID;
            tbl_Order.TicketName = ticketNames;
            tbl_Order.TotalAmount = TotalMoney;
            tbl_Order.BookCount = TotalCount;
            return orderDetails;
        }

        /// <summary>
        /// 检查订单详情的状态，是否还能取消。
        /// </summary>
        /// <param name="orderDetailId">订单详情id</param>
        /// <returns></returns>
        public DataValidResult<Tbl_OrderDetail> CheckOrderDetailIsCanncel(string orderNo)
        {
            var result = new DataValidResult<Tbl_OrderDetail>() { Status = false };
            var list = GetList(orderNo);
            var canncel = list.FirstOrDefault(a => (a.OrderStatus == (int)OrderDetailsDataStatus.Canncel || a.OrderStatus == (int)OrderDetailsDataStatus.Refund));
            if (canncel != null)
            {
                result.Code = "000000";
                result.Message = "订单已取消，重复取消";
                return result;
            }
            var consume = list.FirstOrDefault(a => a.OrderStatus == (int)OrderDetailsDataStatus.Consume);
            if (consume != null)
            {
                result.Code = "114009";
                result.Message = "订单取消失败，订单已消费，订单不能取消";
                return result;
            }
            var noPay = list.FirstOrDefault(a => a.OrderStatus == (int)OrderDetailsDataStatus.IsTaken);
            if (noPay != null)
            {
                result.Code = "114013";
                result.Message = "订单取消失败，订单已取票，不能取消";
                return result;
            }
            var canRefund = list.FirstOrDefault(a => a.CanRefund == false);
            if (canRefund != null)
            {
                result.Code = "114010";
                result.Message = "订单取消失败，订单中包含不支持取消的产品，订单不能取消";
                return result;
            }
            var canRefundTime = list.FirstOrDefault(a => a.CanRefundTime < DateTime.Now.Date);
            if (canRefundTime != null)
            {
                result.Code = "114011";
                result.Message = "订单取消失败，未消费但已过期，订单不能取消";
                return result;
            }
            var count = list.Count(a => (a.OrderStatus == (int)OrderDetailsDataStatus.Success) || (a.OrderStatus == (int)OrderDetailsDataStatus.Activate));
            if (count == list.Count)
            {
                result.Status = true;
                result.List = list;
                return result;
            }
            result.Code = "114012";
            result.Message = "订单取消失败，系统出错";
            return result;
        }

        /// <summary>
        /// 检查订单详情的状态，是否还能取消。
        /// </summary>
        /// <param name="orderDetailId">订单编号</param>
        /// <returns></returns>
        public DataValidResult<Tbl_OrderDetail> CheckOrderDetailIsCanncel(OrderCancelRequest orderCancelRequest, string orderNo)
        {
            var result = new DataValidResult<Tbl_OrderDetail>()
            {
                Status = false,
                List = new List<Tbl_OrderDetail>()
            };
            var list = GetList(orderNo);
            var items = orderCancelRequest.Body.OrderInfo.Items;
            if (items == null || items.Count <= 0)
            {
                result.Code = "114012";
                result.Message = "订单取消失败，系统出错";
                return result;
            }
            foreach (var row in items)
            {
                var orderDetail = list.FirstOrDefault(a => a.OtaOrderDetailId == row.ItemId);
                if (orderDetail == null)
                {
                    result.Code = "114004";
                    result.Message = "订单取消失败，订单不存在";
                    return result;
                }
                if(orderDetail.Quantity!=row.Quantity)
                {
                    result.Code = "114014";
                    result.Message = "订单取消失败，订单取消数量不正确";
                    return result;
                }
                if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
                {
                    continue;
                }
                if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Consume)
                {
                    result.Code = "114009";
                    result.Message = "订单取消失败，订单已消费，订单不能取消";
                    return result;
                }
                if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.IsTaken)
                {
                    result.Code = "114013";
                    result.Message = "订单取消失败，订单已取票，不能取消";
                    return result;
                }
                if (orderDetail.CanRefund == false)
                {
                    result.Code = "114010";
                    result.Message = "订单取消失败，订单不支持取消";
                    return result;
                }
                if (orderDetail.CanRefundTime < DateTime.Now.Date)
                {
                    result.Code = "114011";
                    result.Message = "订单取消失败，订单已过期，不可退";
                    return result;
                }
                if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Success || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Activate)
                {
                    result.List.Add(orderDetail);
                    continue;
                }
                result.Code = "114012";
                result.Message = "订单取消失败，系统出错";
                return result;
            }
            result.Status = true;
            return result;
        }




        /// <summary>
        /// 检查单个订单详情的状态，是否还能取消。
        /// </summary>
        /// <param name="orderDetailId">订单详情id</param>
        /// <returns></returns>
        public DataValidResult<Tbl_OrderDetail> CheckOrderDetailIsCanncel(int orderDetailId)
        {
            var result = new DataValidResult<Tbl_OrderDetail>() { Status = false };
            var orderDetail = _orderDetailRepository.FirstOrDefault(a => a.OrderDetailId == orderDetailId);
            if (orderDetail == null)
            {
                //130001=订单不存在 
                result.Code = "130001";
                result.Message = "取消订单异常，订单不存在";
                return result;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel)
            {
                //订单已取消
                result.Code = "130002";
                result.Message = "取消订单异常，订单已取消";
                return result;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Consume)
            {
                //订单已消费
                result.Code = "130003";
                result.Message = "取消订单异常，订单已消费";
                return result;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.NoPay)
            {
                //订单未支付
                result.Code = "130004";
                result.Message = "取消订单异常，订单未支付";
                return result;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
            {
                //订单已退款
                result.Code = "130005";
                result.Message = "取消订单异常，订单已退款";
                return result;
            }
            if ((orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Success) || (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Activate))
            {
                if (orderDetail.CanRefund && orderDetail.CanRefundTime < DateTime.Now.Date)
                {
                    //"门票已过了最后取消时间";
                    result.Code = "130006";
                    result.Message = "取消订单异常，未消费但已过期";
                    return result;
                }
                result.Status = true;
                result.Item = orderDetail;
                return result;
            }
            result.Code = "130007";
            result.Message = "取消订单异常，取消失败!";
            return result;
        }


        /// <summary>
        /// 更新订单详情
        /// </summary>
        /// <param name="request"></param>
        public void UpdateOrderDetail(OrderUpdateRequest request)
        {
            var person = request.Body.OrderInfo.ContactPerson;
            var orderDetails = GetList(request.Body.OrderInfo.OrderId);
            foreach (var row in orderDetails)
            {
                row.IDCard = person.CardNo;
                row.Mobile = person.Mobile;
                row.Linkman = person.Name;
                row.ValidityDateStart = request.Body.OrderInfo.VisitDate.ToDataTime();
                row.ValidityDateEnd = request.Body.OrderInfo.VisitDate.ToDataTime();
                _orderDetailRepository.Update(row);
            }
        }

        /// <summary>
        /// 修改--小径平台
        /// </summary>
        /// <param name="orderDeail"></param>
        /// <param name="item"></param>
        public void XJ_UpdateOrderDetail(Tbl_OrderDetail orderDeail, XJ_ProductItem item)
        {
            orderDeail.ValidityDateStart = item.StartDate;
            orderDeail.ValidityDateEnd = item.EndDate;
            _orderDetailRepository.Update(orderDeail);
        }


        /// <summary>
        /// 更新订单详情的状态：为已退款
        /// </summary>
        /// <param name="tbl_OrderDetail"></param>
        public void UpdateOrderDetailForRefund(Tbl_OrderDetail tbl_OrderDetail)
        {
            tbl_OrderDetail.OrderStatus = (int)OrderDetailsDataStatus.Refund;
            _orderDetailRepository.Update(tbl_OrderDetail);
        }

        /// <summary>
        /// 更新订单详情的状态：为已取消
        /// </summary>
        /// <param name="tbl_OrderDetail"></param>
        public void UpdateOrderDetailForCanncel(Tbl_OrderDetail tbl_OrderDetail)
        {
            tbl_OrderDetail.OrderStatus = (int)OrderDetailsDataStatus.Canncel;
            _orderDetailRepository.Update(tbl_OrderDetail);
        }

        public void GetOrderDetailsDataStatus(Tbl_OrderDetail tbl_OrderDetail, OrderQueryTicketInfo orderQueryTicketInfo)
        {
            if (tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Success || tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Activate)
            {
                orderQueryTicketInfo.Status = "OREDER_SUCCESS"; //已支付未消费
                orderQueryTicketInfo.OrderStatus = (int)TicketOrderStatus.Success;
            }
            else if (tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel || tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
            {
                orderQueryTicketInfo.Status = "OREDER_CANCEL";//已取消
                orderQueryTicketInfo.OrderStatus = (int)TicketOrderStatus.Canncel;
            }
            else if (tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Consume || tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.IsTaken)
            {
                orderQueryTicketInfo.Status = "OREDER_CONSUMED";//已消费
                orderQueryTicketInfo.OrderStatus = (int)TicketOrderStatus.Consume;
            }
            else if (tbl_OrderDetail.CanRefundTime < DateTime.Now.Date)
            {
                orderQueryTicketInfo.Status = "OREDER_OVERDUE";//已过期
                orderQueryTicketInfo.OrderStatus = (int)TicketOrderStatus.Expired;
            }
        }

        /// <summary>
        /// 创建订单详情 - 二维码票
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tbl_Order"></param>
        /// <returns></returns>
        private List<Tbl_OrderDetail> addQrCodeTicketOrderDetail(OrderInfo order, Tbl_Order tbl_Order)
        {
            List<Tbl_OrderDetail> orderDetails = new List<Tbl_OrderDetail>();
            string ticketNames = string.Empty;
            decimal TotalMoney = 0;
            int TotalCount = 0;
            //企业Id
            int EnterpriseID = 0;
            int ScenicID = 0;
            foreach (var te in order.TicketList)
            {
                var ticket = _ticketService.GetTicket(order.VisitDate.ToDataTime(), te.ProductId);
                var ticketRule = _ticketRuleRepository.FirstOrDefault(a => a.Id == ticket.RuleId);
                //二维码票： 每种门票 一个订单详情，多个数量
                Tbl_OrderDetail tbl_OrderDetail = new Tbl_OrderDetail
                {
                    Number = Guid.NewGuid(),
                    OrderNo = tbl_Order.OrderNo,
                    EnterpriseId = ticket.EnterpriseId,
                    ScenicId = ticket.ScenicId,
                    WindowId = 0,
                    SellerId = 0,
                    SellerType = 1,
                    OtaOrderDetailId = te.ItemId,
                    OrderSource = (int)OrderSource.OTA,
                    TicketSource = (int)TicketSourceStatus.Ota,
                    TicketCategory = (int)TicketCategoryStatus.QrCodeElectronTicket,
                    UsedQuantity = 0,
                    TicketId = te.ProductId,
                    TicketName = ticket.TicketName,
                    Price = ticket.SalePrice,
                    SettlementPrice = ticket.SettlementPrice,
                    Quantity = te.Quantity,
                    BarCode = "",
                    Stub = "",
                    CertificateNO = "",
                    OrderStatus = (int)OrderDetailsDataStatus.NoPay,
                    CreateTime = DateTime.Now,
                    ValidityDateStart = order.VisitDate.ToDataTime(),
                    ValidityDateEnd = order.VisitDate.ToDataTime(),
                    PrintCount = 0,
                    QRcodeUrl = "",
                    QRcode = "",
                    Mobile = order.ContactPerson.Mobile,
                    IDCard = tbl_Order.IDCard,
                    Linkman = order.ContactPerson.Name,
                    CanModify = ticketRule.CanModify,
                    CanRefund = ticketRule.CanRefund
                };
                UpdateOrderDetailRefundTimeAndModifyTime(order.VisitDate.ToDataTime(), ticket, tbl_OrderDetail, ticketRule);
                orderDetails.Add(tbl_OrderDetail);
                //有效的门票信息
                EnterpriseID = ticket.EnterpriseId;
                ScenicID = ticket.ScenicId;
                ticketNames += ticket.TicketName + ",";
                TotalMoney += (ticket.SalePrice * te.Quantity);
                TotalCount += te.Quantity;
            }
            ticketNames = ticketNames.Substring(0, ticketNames.Length - 1);
            if (ticketNames.Length > 50)
            {
                ticketNames = ticketNames.Substring(0, 50);
            }
            tbl_Order.EnterpriseId = EnterpriseID;
            tbl_Order.ScenicId = ScenicID;
            tbl_Order.TicketName = ticketNames;
            tbl_Order.TotalAmount = TotalMoney;
            tbl_Order.BookCount = TotalCount;
            return orderDetails;
        }

        /// <summary>
        /// 更新订单详情的可退款时间和可修改时间
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ticket"></param>
        /// <param name="tbl_OrderDetail"></param>
        private static void UpdateOrderDetailRefundTimeAndModifyTime(DateTime visitDate, Tbl_Ticket ticket, Tbl_OrderDetail tbl_OrderDetail, Tbl_TicketRule ticketRule)
        {
            //是否支持退款
            if (ticketRule.CanRefund)
            {
                int refundDay = ticketRule.RefundDay == null ? 0 : ticketRule.RefundDay.Value;
                int refundHour = ticketRule.RefundHour == null ? 0 : ticketRule.RefundHour.Value;
                int refundMinute = ticketRule.RefundMinute == null ? 0 : ticketRule.RefundMinute.Value;
                if (ticketRule.IsAnytimeRefund)//随时退
                {
                    tbl_OrderDetail.CanRefundTime = null;
                }
                else
                {
                    var canRefundTime = visitDate.Date.AddDays(1).AddSeconds(-1);
                    if (refundDay == 0 && refundHour == 0 && refundMinute == 0)
                    {
                        tbl_OrderDetail.CanRefundTime = canRefundTime;
                    }
                    else
                    {
                        canRefundTime = canRefundTime.AddDays(refundDay);
                        if (refundDay >= 0)
                        {
                            canRefundTime = canRefundTime.AddHours(refundHour);
                            canRefundTime = canRefundTime.AddMinutes(refundMinute);
                        }
                        else
                        {
                            canRefundTime = canRefundTime.AddHours(-refundHour);
                            canRefundTime = canRefundTime.AddMinutes(-refundMinute);
                        }
                        tbl_OrderDetail.CanRefundTime = canRefundTime;
                    }
                }
            }
            //是否支持修改已支付订单
            if (ticketRule.CanModify)
            {
                int modifyDay = ticketRule.ModifyDay == null ? 0 : ticketRule.ModifyDay.Value;
                int modifyHour = ticketRule.ModifyHour == null ? 0 : ticketRule.ModifyHour.Value;
                int modifyMinute = ticketRule.ModifyMinute == null ? 0 : ticketRule.ModifyMinute.Value;
                if (modifyDay == 0 && modifyHour == 0 && modifyMinute == 0)
                {
                    tbl_OrderDetail.CanModifyTime = null;
                }
                else
                {
                    var canModifyTime = visitDate.AddDays(modifyDay);
                    if (modifyDay >= 0)
                    {
                        canModifyTime = canModifyTime.AddHours(modifyHour);
                        canModifyTime = canModifyTime.AddMinutes(modifyMinute);
                    }
                    else
                    {
                        canModifyTime = canModifyTime.AddHours(-modifyHour);
                        canModifyTime = canModifyTime.AddMinutes(-modifyMinute);
                    }
                    tbl_OrderDetail.CanModifyTime = canModifyTime;
                }
            }
            //延时验票时间
            double delayCheckMinutes = Convert.ToDouble(ticketRule.DelayCheck);
            tbl_OrderDetail.DelayCheckTime = tbl_OrderDetail.CreateTime.AddMinutes(delayCheckMinutes);
        }

        public void UpdataForEticektSendQuantity(Guid number)
        {
            var entity = _orderDetailRepository.FirstOrDefault(a => a.Number == number);
            if (entity != null)
            {
                entity.EticektSendQuantity += 1;
                _orderDetailRepository.Update(entity);
            }
        }

        /// <summary>
        /// 验证票的有效期
        /// </summary>
        /// <param name="orderDetailId"></param>
        /// <returns></returns>
        public bool CheckTicketValidity(int orderDetailId)
        {
            var orderDetail = _orderDetailRepository.FirstOrDefault(a => a.OrderDetailId == orderDetailId);
            ////是否已使用
            //if (orderDetail.OrderStatus == 4)
            //{
            //    Logger.Info(Resource.Voice.AlreadyUsed);
            //    voiceStr = Resource.Voice.AlreadyUsed;
            //    return false;
            //}
            ////是否已退票
            //if (orderDetail.OrderStatus == 5)
            //{
            //    Logger.Info(Resource.Voice.RefundTicket);
            //    voiceStr = Resource.Voice.RefundTicket;
            //    return false;
            //}
            //票的有效期
            if (!(orderDetail.ValidityDateStart <= DateTime.Now))
            {
                //Logger.Info("不在有效期:" + Resource.Voice.TooEarly);
                //voiceStr = Resource.Voice.TooEarly;
                return false;
            }
            if (!(DateTime.Now <= orderDetail.ValidityDateEnd.AddDays(1).AddSeconds(-1)))
            {
                //Logger.Info("不在有效期:" + Resource.Voice.Overdue);
                //voiceStr = Resource.Voice.Overdue;
                return false;
            }
            //if (ticketDetail.OrderStatus == 3)
            //{
            //    if (ticketDetail.CreateTime.AddMinutes(delayCheck) > DateTime.Now)
            //    {
            //        Logger.Info(Resource.Voice.BeforeCheckin);
            //        voiceStr = Resource.Voice.BeforeCheckin;
            //        return false;
            //    }
            //    Logger.Info("门票有效！");
            //    voiceStr = Resource.Voice.Welcome;
            //    return true;
            //}
            return true;
        }

        /// <summary>
        /// 把已激活的票变成已使用的状态
        /// </summary>
        /// <param name="number"></param>
        /// <param name="usedQuantity"></param>
        /// <returns></returns>
        public Tbl_OrderDetail Update(Guid number, int usedQuantity)
        {
            var entity = _orderDetailRepository.FirstOrDefault(a => a.Number == number);
            if (entity != null)
            {
                entity.OrderStatus = (int)OrderDetailsDataStatus.Consume;//订单状态 , 已使用
                entity.CheckTime = DateTime.Now;//验票时间
                entity.UsedQuantity = usedQuantity;//已消费数
                _orderDetailRepository.Update(entity);
            }
            return entity;
        }

        public void Update(Tbl_OrderDetail entity)
        {
            _orderDetailRepository.Update(entity);
        }

        /// <summary>
        /// 获取订单详情--小径平台
        /// </summary>
        /// <param name="otaOrderDetailId"></param>
        /// <returns></returns>
        public Tbl_OrderDetail XJ_CheckOrderDetailIsUpdate(string otaOrderDetailId)
        {
            return _orderDetailRepository.FirstOrDefault(a => a.OtaOrderDetailId == otaOrderDetailId);
        }

        /// <summary>
        /// 检查订单详情的状态，是否还能取消。--小径平台
        /// </summary>
        /// <param name="orderDetailId">订单详情id</param>
        /// <returns></returns>
        public Tbl_OrderDetail XJ_CheckOrderDetailIsCanncel(string otaOrderDetailId)
        {
            var result = new DataValidResult<Tbl_OrderDetail>() { Status = false };
            var orderDetail = _orderDetailRepository.FirstOrDefault(a => a.OtaOrderDetailId == otaOrderDetailId);
            if (orderDetail == null)
            {
                //130001=订单不存在 
                return null;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel)
            {
                //订单已取消
                return null;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Consume)
            {
                //订单已消费
                return null;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.NoPay)
            {
                //订单未支付
                return null;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
            {
                //订单已退款
                return null;
            }
            return orderDetail;
        }

        /// <summary>
        /// 订单详情更新为出票状态
        /// </summary>
        /// <param name="qRcode">二维码</param>
        /// <returns></returns>
        public Tbl_OrderDetail UpdatePrintTicketStatus(int orderDetailId)
        {
            var orderDetail = _orderDetailRepository.FirstOrDefault(a => a.OrderDetailId == orderDetailId);
            if (orderDetail == null)
            {
                return null;
            }
            //增加打印次数
            orderDetail.UsedQuantity = orderDetail.Quantity;
            orderDetail.PrintCount++;
            orderDetail.OrderStatus = (int)OrderDetailsDataStatus.IsTaken;
            _orderDetailRepository.Update(orderDetail);
            return orderDetail;
        }
    }
}
