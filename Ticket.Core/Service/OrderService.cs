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
using Ticket.Utility.Helpers;
using Ticket.Utility.Validation;
using Ticket.Model.Model.Order;

namespace Ticket.Core.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailService _orderDetailService;
        private readonly OrderDetailRepository _orderDetailRepository;

        public OrderService(
            OrderRepository orderRepository,
            OrderDetailService orderDetailService,
            OrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
            _orderDetailRepository = orderDetailRepository;
        }

        public void BeginTran()
        {
            _orderRepository.BeginTran();
        }
        public void CommitTran()
        {
            _orderRepository.CommitTran();
        }
        public void RollbackTran()
        {
            _orderRepository.RollbackTran();
        }

        public Tbl_Order Get(string orderNo)
        {
            var order = _orderRepository.FirstOrDefault(o => o.OrderNo == orderNo);
            return order;
        }
        /// <summary>
        /// 根据支付码进行查询
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public Tbl_Order GetByAuthCode(string authCode)
        {
            DateTime startData = DateTime.Now.Date;
            DateTime endData = startData.AddDays(1);
            var order = _orderRepository.FirstOrDefault(a => a.AuthCode == authCode && a.CreateTime >= startData && a.CreateTime < endData);
            return order;
        }

        public Tbl_Order Get(string orderNo, string otaOrderNo)
        {
            if (string.IsNullOrEmpty(otaOrderNo))
            {
                return _orderRepository.FirstOrDefault(o => o.OrderNo == orderNo);
            }
            return _orderRepository.FirstOrDefault(o => o.OrderNo == orderNo && o.OTAOrderNo == otaOrderNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otaOrderNo">OTA的订单id</param>
        /// <returns></returns>
        public Tbl_Order GetOrderBy(string otaOrderNo)
        {
            return _orderRepository.FirstOrDefault(o => o.OTAOrderNo == otaOrderNo);
        }

        /// <summary>
        /// 获取售票员订单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sellerId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public TPageResult<OrderViewModel> GetOrderList(int page, int pageSize, int sellerId, string keyword)
        {
            var result = new TPageResult<OrderViewModel>();

            var total = 0;
            List<Tbl_Order> orderList = new List<Tbl_Order>();

            if (!string.IsNullOrEmpty(keyword))
            {
                var orderDetail = _orderDetailRepository.FirstOrDefault(a => a.CertificateNO == keyword);
                if (orderDetail == null)
                {
                    return result.SuccessResult(null, 0);
                }
                orderList = _orderRepository.GetPageList(pageSize, page, out total,
                   a => a.TicketSource == (int)TicketSourceStatus.ScenicSpot && a.SellerId == sellerId && a.OrderNo == orderDetail.OrderNo
                   , a => a.CreateTime, false);
            }
            else
            {
                orderList = _orderRepository.GetPageList(pageSize, page, out total,
                   a => a.TicketSource == (int)TicketSourceStatus.ScenicSpot && a.SellerId == sellerId
                   , a => a.CreateTime, false);
            }

            if (orderList.Count <= 0)
            {
                return result.SuccessResult(null, 0);
            }
            var orderNos = orderList.Select(a => a.OrderNo).ToList();
            var orderDetails = _orderDetailRepository.GetAllList(a => orderNos.Contains(a.OrderNo));

            var orderViewList = new List<OrderViewModel>();
            foreach (var row in orderList)
            {
                OrderViewModel relist = new OrderViewModel();
                relist.OrderNo = row.OrderNo;
                relist.Linkman = row.Linkman;
                relist.IDCard = row.IDCard;
                relist.CreateTime = row.CreateTime;
                relist.Mobile = row.Mobile;
                relist.ValidityDateStart = row.ValidityDateStart;
                relist.ValidityDateEnd = row.ValidityDateEnd;
                relist.BookCount = row.BookCount;
                relist.TotalAmount = row.TotalAmount;
                relist.PayType = row.PayType;
                //未过有效期 允许退票
                relist.CanRefund = row.ValidityDateEnd >= DateTime.Now.Date;
                relist.ListDtl = new List<OdtDtl>();
                var details = orderDetails.Where(a => a.OrderNo == row.OrderNo).ToList();
                foreach (var ent in details)
                {
                    relist.IsPrintT = ent.TicketCategory == 1;
                    relist.IsTwo = ent.TicketCategory == 2;
                    OdtDtl dtlEnt = new OdtDtl
                    {
                        OrderDetailId = ent.OrderDetailId,
                        TicketCategory = ent.TicketCategory,
                        TicketName = ent.TicketName,
                        Quantity = ent.Quantity,
                        Price = ent.Price,
                        TotalAmount = (ent.Price * ent.Quantity),
                        CertificateNO = ent.CertificateNO,
                        Mobile = ent.Mobile,
                        IDCard = ent.IDCard,
                        PrintCount = ent.PrintCount,
                        OrderStatus = ent.OrderStatus,
                        CanRefund = ((ent.OrderStatus == (int)OrderDetailsDataStatus.Activate || ent.OrderStatus == (int)OrderDetailsDataStatus.IsTaken) && row.ValidityDateEnd >= DateTime.Now.Date)
                    };
                    relist.ListDtl.Add(dtlEnt);
                }
                orderViewList.Add(relist);
            }
            return result.SuccessResult(orderViewList, total);
        }

        /// <summary>
        /// 获取ota订单
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="scenicId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public TPageResult<OtaOrderViewModel> GetOtaOrderList(int page, int pageSize, int scenicId, string keyword)
        {
            var result = new TPageResult<OtaOrderViewModel>();

            var total = 0;
            List<Tbl_Order> orderList = new List<Tbl_Order>();

            if (!string.IsNullOrEmpty(keyword))
            {
                var orderDetail = _orderDetailRepository.FirstOrDefault(a => a.CertificateNO == keyword);
                if (orderDetail == null)
                {
                    return result.SuccessResult(null, 0);
                }
                orderList = _orderRepository.GetPageList(pageSize, page, out total,
                   a => a.TicketSource == (int)TicketSourceStatus.Ota && a.ScenicId == scenicId && a.OrderNo == orderDetail.OrderNo
                   , a => a.CreateTime, false);
            }
            else
            {
                orderList = _orderRepository.GetPageList(pageSize, page, out total,
                       a => a.TicketSource == (int)TicketSourceStatus.Ota && a.ScenicId == scenicId
                       , a => a.CreateTime, false);
            }

            if (orderList.Count <= 0)
            {
                return result.SuccessResult(null, 0);
            }
            var orderNos = orderList.Select(a => a.OrderNo).ToList();
            var orderDetails = _orderDetailRepository.GetAllList(a => orderNos.Contains(a.OrderNo));

            var orderViewList = new List<OtaOrderViewModel>();
            foreach (var row in orderList)
            {
                OtaOrderViewModel relist = new OtaOrderViewModel();
                relist.OrderNo = row.OrderNo;
                relist.OtaOrderNo = row.OTAOrderNo;
                relist.BookCount = row.BookCount;
                relist.TotalAmount = row.TotalAmount;
                relist.CreateTime = row.CreateTime;
                relist.TravelDate = row.ValidityDateStart;
                relist.LinkName = row.Linkman;
                relist.ReceiverName = row.ReceiverName;
                relist.Mobile = row.Mobile;
                relist.IDCard = row.IDCard;
                relist.OTABusinessId = row.OTABusinessId;
                relist.OTABusinessName = row.OTABusinessName;
                relist.Details = new List<OtaOrderDetailViewModel>();
                //今天
                var today = DateTime.Now.Date;
                var details = orderDetails.Where(a => a.OrderNo == row.OrderNo).ToList();
                foreach (var p in details)
                {
                    OtaOrderDetailViewModel dtlEnt = new OtaOrderDetailViewModel
                    {
                        OrderDetailId = p.OrderDetailId,
                        Price = p.Price,
                        Qunatity = p.Quantity,
                        TicketName = p.TicketName,
                        CertificateNo = p.CertificateNO,
                        OrderStatus = p.OrderStatus == (int)OtaOrderStatus.Success && p.ValidityDateEnd < today ? (int)OtaOrderStatus.Expired : p.OrderStatus,
                        TicketCategory = p.TicketCategory,
                        sentTimes = p.EticektSendQuantity,
                        PrintCount = p.PrintCount
                    };
                    relist.Details.Add(dtlEnt);
                }
                orderViewList.Add(relist);
            }
            return result.SuccessResult(orderViewList, total);
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="request"></param>
        public void UpdateOrder(Tbl_Order tbl_Order, OrderUpdateRequest request)
        {
            var person = request.Body.OrderInfo.ContactPerson;
            tbl_Order.IDType = GetIdCardType(person.CardType);
            tbl_Order.IDCard = person.CardNo;
            tbl_Order.Mobile = person.Mobile;
            tbl_Order.Linkman = person.Name;
            tbl_Order.ValidityDateStart = request.Body.OrderInfo.VisitDate.ToDataTime();
            tbl_Order.ValidityDateEnd = request.Body.OrderInfo.VisitDate.ToDataTime();
            _orderRepository.Update(tbl_Order);
        }

        /// <summary>
        /// 修改订单--小径平台
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="request"></param>
        public void UpdateOrder(Tbl_Order tbl_Order, XJ_Order request)
        {
            var person = request.ContactPerson;
            tbl_Order.IDType = GetIdCardType(person.CardType);
            tbl_Order.IDCard = person.CardNo;
            tbl_Order.Mobile = person.Mobile;
            tbl_Order.Linkman = person.Name;
            tbl_Order.ValidityDateStart = request.VisitDate;
            tbl_Order.ValidityDateEnd = request.VisitDate;
            _orderRepository.Update(tbl_Order);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Tbl_Order AddOrder(OrderInfo request, Tbl_OTABusiness business)
        {
            //创建订单号
            string orderNo = OrderHelper.GenerateOrderNo();
            var order = Get(orderNo);
            if (order != null)
            {
                orderNo = OrderHelper.GenerateOrderNo();
            }
            int idType = GetIdCardType(request.ContactPerson.CardType);

            //订单
            Tbl_Order tbl_Order = new Tbl_Order
            {
                OrderNo = orderNo,
                OTABusinessId = business.Id,
                OTAOrderNo = request.OrderOtaId,
                TicketSource = (int)TicketSourceStatus.Ota,
                PayType = (int)PayStatus.NoPayStatus,
                PayAccount = "",
                PayTradeNo = "",
                SellerId = 0,
                Price = 0,
                Linkman = request.ContactPerson.Name,
                Mobile = request.ContactPerson.Mobile,
                OrderStatus = (int)OrderDataStatus.NoPay,
                CreateTime = DateTime.Now,
                ValidityDateStart = request.VisitDate.ToDataTime(),
                ValidityDateEnd = request.VisitDate.ToDataTime(),
                UsedQuantity = 0,
                Remark = "",
                IDType = idType,
                IDCard = request.ContactPerson.CardNo,
                CreateUserId = 0
            };
            return tbl_Order;
        }

        public void Add(Tbl_Order tbl_Order)
        {
            _orderRepository.Add(tbl_Order);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Tbl_Order AddOrder(OrderInfoCreateModel order)
        {
            //创建订单号
            string orderNo = OrderHelper.GenerateOrderNo();

            //订单
            Tbl_Order list = new Tbl_Order
            {
                OrderNo = orderNo,
                TicketSource = order.TicketSource,
                PayType = order.PayType,
                PayAccount = "",
                PayTradeNo = "",
                SellerId = order.UserId,
                Price = 0,
                Linkman = order.Linkman,
                Mobile = order.Mobile,
                OrderStatus = (int)OrderDataStatus.NoPay,
                CreateTime = DateTime.Now,
                ValidityDateStart = order.ValidityDate,
                ValidityDateEnd = order.ValidityDate,
                UsedQuantity = 0,
                Remark = order.Remark,
                IDType = order.IdType,
                IDCard = order.IdCard,
                CreateUserId = order.UserId,
                OrderType = 0,
                OTABusinessName = "",
                GroupWay = 0
            };
            return list;
        }

        /// <summary>
        /// 创建单个产品订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Tbl_Order AddOrder(OrderSingleInfo request, Tbl_OTABusiness business)
        {
            //创建订单号
            string orderNo = OrderHelper.GenerateOrderNo();
            var order = Get(orderNo);
            if (order != null)
            {
                orderNo = OrderHelper.GenerateOrderNo();
            }
            int idType = GetIdCardType(request.ContactPerson.CardType);

            //订单
            Tbl_Order tbl_Order = new Tbl_Order
            {
                OrderNo = orderNo,
                OTABusinessId = business.Id,
                OTAOrderNo = request.OrderOtaId,
                TicketSource = (int)TicketSourceStatus.Ota,
                PayType = (int)PayStatus.NoPayStatus,
                PayAccount = "",
                PayTradeNo = "",
                SellerId = 0,
                Price = 0,
                Linkman = request.ContactPerson.Name,
                Mobile = request.ContactPerson.Mobile,
                OrderStatus = (int)OrderDataStatus.NoPay,
                CreateTime = DateTime.Now,
                ValidityDateStart = request.VisitDate.ToDataTime(),
                ValidityDateEnd = request.VisitDate.ToDataTime(),
                UsedQuantity = 0,
                Remark = "",
                IDType = idType,
                IDCard = request.ContactPerson.CardNo,
                CreateUserId = 0
            };
            return tbl_Order;
        }

        /// <summary>
        /// 创建订单--小径平台
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Tbl_Order AddOrder(XJ_Order request)
        {
            //创建订单号
            string orderNo = OrderHelper.GenerateOrderNo();
            var order = Get(orderNo);
            if (order != null)
            {
                orderNo = OrderHelper.GenerateOrderNo();
            }
            int idType = GetIdCardType(request.ContactPerson.CardType);

            //订单
            Tbl_Order tbl_Order = new Tbl_Order
            {
                OrderNo = orderNo,
                OTABusinessId = request.OTABusinessId,
                OTAOrderNo = request.OrderOtaId,
                TicketSource = (int)TicketSourceStatus.Ota,
                PayType = (int)PayStatus.NoPayStatus,
                PayAccount = "",
                PayTradeNo = "",
                SellerId = 0,
                Price = 0,
                Linkman = request.ContactPerson.Name,
                Mobile = request.ContactPerson.Mobile,
                OrderStatus = (int)OrderDataStatus.NoPay,
                CreateTime = DateTime.Now,
                ValidityDateStart = request.VisitDate,
                ValidityDateEnd = request.VisitDate,
                UsedQuantity = 0,
                Remark = "",
                IDType = idType,
                IDCard = request.ContactPerson.CardNo,
                CreateUserId = 0
            };
            return tbl_Order;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Tbl_Order AddOrderForNoPay(OrderInfoModel order)
        {
            //创建订单号
            string orderNo = OrderHelper.GenerateOrderNo();
            //订单
            Tbl_Order list = new Tbl_Order
            {
                OrderNo = orderNo,
                TicketSource = order.TicketSource,
                PayType = order.PayType,
                PayAccount = "",
                PayTradeNo = "",
                AuthCode = order.Code,
                SellerId = 0,
                Price = 0,
                Linkman = order.Linkman,
                Mobile = order.Mobile,
                OrderStatus = (int)OrderDataStatus.NoPay,
                CreateTime = DateTime.Now,
                ValidityDateStart = order.ValidityDate,
                ValidityDateEnd = order.ValidityDate,
                UsedQuantity = 0,
                Remark = "TVM购票",
                IDCard = "",
                CreateUserId = 0,
                OrderType = 0,
                OTABusinessName = "",
                GroupWay = 0
            };
            return list;
        }




        public int GetIdCardType(string cardType)
        {
            int idType = 0;
            switch (cardType)
            {
                case "ID_CARD": idType = (int)CredentialsStatus.IdCard; break;
                case "HUZHAO": idType = (int)CredentialsStatus.Passport; break;
                case "TAIBAO": idType = (int)CredentialsStatus.Taiwan; break;
                case "GANGAO": idType = (int)CredentialsStatus.HongKongMacauLaissezPasser; break;
            }
            return idType;
        }

        /// <summary>
        /// 更改订单支付状态和可退款时间和可修改时间
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="tbl_OrderDetails"></param>
        public void UpdateOrder(Tbl_Order tbl_Order, List<Tbl_OrderDetail> tbl_OrderDetails)
        {
            //更改订单状态
            tbl_Order.PayTime = DateTime.Now;
            tbl_Order.OrderStatus = (int)OrderDataStatus.Success;
            //更改可修改时间
            var isCanModify = tbl_OrderDetails.Count(a => a.CanModify == false);
            tbl_Order.CanModify = isCanModify > 0 ? false : true;
            if (tbl_Order.CanModify)
            {
                var tbl_OrderDetail = tbl_OrderDetails.OrderBy(a => a.CanModifyTime).FirstOrDefault();
                tbl_Order.CanModifyTime = tbl_OrderDetail.CanModifyTime;
            }
            //更改可退款时间
            var isCanRefund = tbl_OrderDetails.Count(a => a.CanRefund == false);
            tbl_Order.CanRefund = isCanRefund > 0 ? false : true;
            if (tbl_Order.CanRefund)
            {
                var tbl_OrderDetail = tbl_OrderDetails.OrderBy(a => a.CanRefundTime).FirstOrDefault();
                tbl_Order.CanRefundTime = tbl_OrderDetail.CanRefundTime;
            }
        }

        /// <summary>
        /// 更改订单支付状态和可退款时间和可修改时间
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="tbl_OrderDetails"></param>
        public void UpdateOrder(Tbl_Order tbl_Order, Tbl_OrderDetail tbl_OrderDetail)
        {
            //更改订单状态
            tbl_Order.PayTime = DateTime.Now;
            tbl_Order.OrderStatus = (int)OrderDataStatus.Success;
            //更改可修改时间
            tbl_Order.CanModify = tbl_OrderDetail.CanModify;
            if (tbl_Order.CanModify)
            {
                tbl_Order.CanModifyTime = tbl_OrderDetail.CanModifyTime;
            }
            //更改可退款时间
            tbl_Order.CanRefund = tbl_OrderDetail.CanRefund;
            if (tbl_Order.CanRefund)
            {
                tbl_Order.CanRefundTime = tbl_OrderDetail.CanRefundTime;
            }
        }

        /// <summary>
        /// 验证订单基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderVerifyRequest(OrderCreateRequest request)
        {
            var orderInfo = request.Body.OrderInfo;
            var result = new DataValidResult { Status = false };
            if (orderInfo.ContactPerson == null)
            {
                result.Code = "113001";
                result.Message = "创建订单异常，取票人信息为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Name))
            {
                result.Code = "113002";
                result.Message = "创建订单异常，取票人姓名不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113003";
                result.Message = "创建订单异常，取票人手机号码不能为空";
                return result;
            }
            if (!RegexValidation.IsCellPhone(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113004";
                result.Message = "创建订单异常，取票人手机号码异常";
                return result;
            }
            if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardType))
            {
                if (orderInfo.ContactPerson.CardType.ToUpper() == "ID_CARD" && !string.IsNullOrEmpty(orderInfo.ContactPerson.CardNo))
                {
                    if (!RegexValidation.IsIdCard(orderInfo.ContactPerson.CardNo))
                    {
                        result.Code = "113005";
                        result.Message = "创建订单异常，游客身份证信息输入有误";
                        return result;
                    }
                }
            }

            if (orderInfo.OrderPrice <= 0)
            {
                result.Code = "113007";
                result.Message = "创建订单异常，订单总金额不能小于0";
                return result;
            }
            if (orderInfo.OrderQuantity <= 0)
            {
                result.Code = "113008";
                result.Message = "创建订单异常，订票总数量不能小于1";
                return result;
            }
            if (!orderInfo.VisitDate.IsDataTime())
            {
                result.Code = "113009";
                result.Message = "创建订单异常，游玩日期格式不合法";
                return result;
            }
            if (orderInfo.TicketList == null || orderInfo.TicketList.Count <= 0)
            {
                result.Code = "113010";
                result.Message = "创建订单异常，购买产品的数量不能小于1";
                return result;
            }
            if (orderInfo.TicketList.Sum(a => a.Quantity) != orderInfo.OrderQuantity)
            {
                result.Code = "113011";
                result.Message = "创建订单异常，购买产品的总数量和订票总数量不符";
                return result;
            }
            if (orderInfo.TicketList.Sum(a => a.SellPrice * a.Quantity) != orderInfo.OrderPrice)
            {
                result.Code = "113012";
                result.Message = "创建订单异常，购买产品的总金额和订票总金额不符";
                return result;
            }
            foreach (var row in orderInfo.TicketList)
            {
                if (row.Quantity <= 0)
                {
                    result.Code = "113013";
                    result.Message = "创建订单异常，购买产品的游客人数不能小于1";
                    return result;
                }
                if (row.ProductId <= 0)
                {
                    result.Code = "113014";
                    result.Message = "创建订单异常，购买产品的id不合法";
                    return result;
                }
                if (orderInfo.TicketList == null)
                {
                    result.Code = "113015";
                    result.Message = "创建订单异常，购买产品的数据不合法";
                    return result;
                }
                if (row.SellPrice <= 0)
                {
                    result.Code = "113016";
                    result.Message = "创建订单异常，购买产品的金额不合法";
                    return result;
                }
            }

            if (orderInfo.OrderPayStatus > 1 || orderInfo.OrderPayStatus < 0)
            {
                result.Code = "113017";
                result.Message = "创建订单异常，是否收款超出范围";
                return result;
            }

            result.Status = true;
            return result;
        }

        /// <summary>
        /// 创建订单基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderCreateRequest(OrderCreateRequest request, OrderCreateResponse response)
        {
            var orderInfo = request.Body.OrderInfo;
            var result = new DataValidResult { Status = false };
            if (orderInfo.ContactPerson == null)
            {
                result.Code = "113001";
                result.Message = "创建订单异常，取票人信息为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Name))
            {
                result.Code = "113002";
                result.Message = "创建订单异常，取票人姓名不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113003";
                result.Message = "创建订单异常，取票人手机号码不能为空";
                return result;
            }
            if (!RegexValidation.IsCellPhone(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113004";
                result.Message = "创建订单异常，取票人手机号码异常";
                return result;
            }
            if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardType))
            {
                if (orderInfo.ContactPerson.CardType.ToUpper() == "ID_CARD" && !string.IsNullOrEmpty(orderInfo.ContactPerson.CardNo))
                {
                    if (!RegexValidation.IsIdCard(orderInfo.ContactPerson.CardNo))
                    {
                        result.Code = "113005";
                        result.Message = "创建订单异常，游客身份证信息输入有误";
                        return result;
                    }
                }
            }

            if (string.IsNullOrEmpty(orderInfo.OrderOtaId))
            {
                result.Code = "113006";
                result.Message = "创建订单异常，OTA订单id不能为空";
                return result;
            }
            if (orderInfo.OrderPrice <= 0)
            {
                result.Code = "113007";
                result.Message = "创建订单异常，订单总金额不能小于0";
                return result;
            }
            if (orderInfo.OrderQuantity <= 0)
            {
                result.Code = "113008";
                result.Message = "创建订单异常，订票总数量不能小于1";
                return result;
            }
            if (!orderInfo.VisitDate.IsDataTime())
            {
                result.Code = "113009";
                result.Message = "创建订单异常，游玩日期格式不合法";
                return result;
            }
            if (orderInfo.TicketList == null || orderInfo.TicketList.Count <= 0)
            {
                result.Code = "113010";
                result.Message = "创建订单异常，购买产品的数量不能小于1";
                return result;
            }
            if (orderInfo.TicketList.Sum(a => a.Quantity) != orderInfo.OrderQuantity)
            {
                result.Code = "113011";
                result.Message = "创建订单异常，购买产品的总数量和订票总数量不符";
                return result;
            }
            if (orderInfo.TicketList.Sum(a => a.SellPrice * a.Quantity) != orderInfo.OrderPrice)
            {
                result.Code = "113012";
                result.Message = "创建订单异常，购买产品的总金额和订票总金额不符";
                return result;
            }
            foreach (var row in orderInfo.TicketList)
            {
                if (row.Quantity <= 0)
                {
                    result.Code = "113013";
                    result.Message = "创建订单异常，购买产品的游客人数不能小于1";
                    return result;
                }
                if (row.ProductId <= 0)
                {
                    result.Code = "113014";
                    result.Message = "创建订单异常，购买产品的id不合法";
                    return result;
                }
                if (orderInfo.TicketList == null)
                {
                    result.Code = "113015";
                    result.Message = "创建订单异常，购买产品的数据不合法";
                    return result;
                }
                if (row.SellPrice <= 0)
                {
                    result.Code = "113016";
                    result.Message = "创建订单异常，购买产品的金额不合法";
                    return result;
                }
            }

            if (orderInfo.OrderPayStatus > 1 || orderInfo.OrderPayStatus < 0)
            {
                result.Code = "113017";
                result.Message = "创建订单异常，是否收款超出范围";
                return result;
            }
            //验证OTA订单id是否已存在
            var tbl_Order = GetOrderBy(request.Body.OrderInfo.OrderOtaId);
            if (tbl_Order != null)
            {
                var tbl_OrderDetails = _orderDetailService.GetList(tbl_Order.OrderNo);
                result.Code = "000000";
                result.Message = "成功,重复提交订单";
                response.Body = new OrderCreateInfo
                {
                    OtaOrderId = tbl_Order.OTAOrderNo,
                    OrderId = tbl_Order.OrderNo,
                    OrderStatus = "OREDER_SUCCESS",
                    Item = new List<OrderCreateItem>()
                };
                foreach (var row in tbl_OrderDetails)
                {
                    response.Body.Item.Add(new OrderCreateItem
                    {
                        OtaOrderDetailId = row.OtaOrderDetailId,
                        ProductId = row.TicketId.ToString(),
                        useDate = row.ValidityDateStart.ToString("yyyy-MM-dd"),
                        CertificateNo = row.CertificateNO,
                        quantity = 500000
                    });
                }
                return result;
            }
            result.Status = true;
            return result;
        }

        /// <summary>
        /// 下单验证基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForVerifyOrderRequest(OrderSingleCreateRequest request)
        {
            var orderInfo = request.Body.OrderInfo;
            var result = new DataValidResult { Status = false };
            if (orderInfo.ContactPerson == null)
            {
                result.Code = "113001";
                result.Message = "创建订单异常，取票人信息为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Name))
            {
                result.Code = "113002";
                result.Message = "创建订单异常，取票人姓名不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113003";
                result.Message = "创建订单异常，取票人手机号码不能为空";
                return result;
            }
            if (!RegexValidation.IsCellPhone(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113004";
                result.Message = "创建订单异常，取票人手机号码异常";
                return result;
            }
            if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardType))
            {
                switch (orderInfo.ContactPerson.CardType.ToUpper())
                {
                    case "ID_CARD":
                        if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardNo))
                        {
                            if (!RegexValidation.IsIdCard(orderInfo.ContactPerson.CardNo))
                            {
                                result.Code = "113005";
                                result.Message = "创建订单异常，游客身份证信息输入有误";
                                return result;
                            }
                        }
                        break;
                    case "HUZHAO":
                    case "TAIBAO":
                    case "GANGAO":
                    case "OTHER":
                        break;
                }
            }
            if (orderInfo.OrderQuantity <= 0)
            {
                result.Code = "113008";
                result.Message = "创建订单异常，订票总数量不能小于1";
                return result;
            }
            if (!orderInfo.VisitDate.IsDataTime())
            {
                result.Code = "113009";
                result.Message = "创建订单异常，游玩日期格式不合法";
                return result;
            }
            if (orderInfo.VisitDate.ToDataTime() < DateTime.Now.Date)
            {
                result.Code = "113009";
                result.Message = "创建订单异常，游玩日期不合法";
                return result;
            }
            if (orderInfo.Ticket == null)
            {
                result.Code = "113015";
                result.Message = "创建订单异常，购买产品的数据不合法";
                return result;
            }
            if (orderInfo.Ticket.Quantity <= 0)
            {
                result.Code = "113013";
                result.Message = "创建订单异常，购买产品的游客人数不能小于1";
                return result;
            }
            if (orderInfo.Ticket.ProductId <= 0)
            {
                result.Code = "113014";
                result.Message = "创建订单异常，购买产品的id不合法";
                return result;
            }

            result.Status = true;
            return result;
        }

        /// <summary>
        /// 创建单个订单基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderSingleCreateRequest(OrderSingleCreateRequest request, OrderSingleCreateResponse response)
        {
            var orderInfo = request.Body.OrderInfo;
            var result = new DataValidResult { Status = false };
            if (orderInfo.ContactPerson == null)
            {
                result.Code = "113001";
                result.Message = "创建订单异常，取票人信息为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Name))
            {
                result.Code = "113002";
                result.Message = "创建订单异常，取票人姓名不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113003";
                result.Message = "创建订单异常，取票人手机号码不能为空";
                return result;
            }
            if (!RegexValidation.IsCellPhone(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "113004";
                result.Message = "创建订单异常，取票人手机号码异常";
                return result;
            }
            if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardType))
            {
                switch (orderInfo.ContactPerson.CardType.ToUpper())
                {
                    case "ID_CARD":
                        if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardNo))
                        {
                            if (!RegexValidation.IsIdCard(orderInfo.ContactPerson.CardNo))
                            {
                                result.Code = "113005";
                                result.Message = "创建订单异常，游客身份证信息输入有误";
                                return result;
                            }
                        }
                        break;
                    case "HUZHAO":
                    case "TAIBAO":
                    case "GANGAO":
                    case "OTHER":
                        break;
                }
            }

            if (string.IsNullOrEmpty(orderInfo.OrderOtaId))
            {
                result.Code = "113006";
                result.Message = "创建订单异常，OTA订单id不能为空";
                return result;
            }

            if (orderInfo.OrderQuantity <= 0)
            {
                result.Code = "113008";
                result.Message = "创建订单异常，订票总数量不能小于1";
                return result;
            }
            if (!orderInfo.VisitDate.IsDataTime())
            {
                result.Code = "113009";
                result.Message = "创建订单异常，游玩日期格式不合法";
                return result;
            }
            if (orderInfo.VisitDate.ToDataTime() < DateTime.Now.Date)
            {
                result.Code = "113009";
                result.Message = "创建订单异常，游玩日期不合法";
                return result;
            }
            if (orderInfo.Ticket == null)
            {
                result.Code = "113015";
                result.Message = "创建订单异常，购买产品的数据不合法";
                return result;
            }
            if (orderInfo.Ticket.Quantity != orderInfo.OrderQuantity)
            {
                result.Code = "113011";
                result.Message = "创建订单异常，购买产品的总数量和订票总数量不符";
                return result;
            }

            if (orderInfo.Ticket.Quantity <= 0)
            {
                result.Code = "113013";
                result.Message = "创建订单异常，购买产品的游客人数不能小于1";
                return result;
            }
            if (orderInfo.Ticket.ProductId <= 0)
            {
                result.Code = "113014";
                result.Message = "创建订单异常，购买产品的id不合法";
                return result;
            }
            //验证OTA订单id是否已存在
            var tbl_Order = GetOrderBy(request.Body.OrderInfo.OrderOtaId);
            if (tbl_Order != null)
            {
                var tbl_OrderDetail = _orderDetailService.Get(tbl_Order.OrderNo);
                result.Code = "000000";
                result.Message = "成功,重复提交订单";
                response.Body.OrderId = tbl_Order.OrderNo;
                response.Body.OtaOrderId = tbl_Order.OTAOrderNo;
                response.Body.CertificateNo = tbl_OrderDetail.CertificateNO;
                response.Body.Code = tbl_OrderDetail.QRcode;
                response.Body.OrderStatus = "OREDER_SUCCESS";
                return result;
            }
            result.Status = true;
            return result;
        }

        /// <summary>
        /// 验证订单数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderCreateRequest(OrderCreateRequest request, List<Tbl_Ticket> tbl_Tickets)
        {
            var result = new DataValidResult { Status = false };
            if (tbl_Tickets.Count <= 0)
            {
                result.Code = "113019";
                result.Message = "创建订单异常，选择的游玩日期超出选购产品的有效期或者选购的产品无效";
                return result;
            }
            if (tbl_Tickets.Count != request.Body.OrderInfo.TicketList.Count)
            {
                result.Code = "113025";
                result.Message = "创建订单异常，选购的产品中包含已超出产品有效期或无效的产品";
                return result;
            }
            foreach (var row in request.Body.OrderInfo.TicketList)
            {
                var ticket = tbl_Tickets.FirstOrDefault(a => a.TicketId == row.ProductId && a.SalePrice == row.SellPrice);
                if (ticket == null)
                {
                    result.Code = "113020";
                    result.Message = "创建订单异常，选购产品的价格与原始产品价格不一致";
                    return result;
                }
                var sCount = ticket == null ? 0 : ticket.StockCount;
                var sellCount = ticket == null ? 0 : ticket.SellCount;
                if (sCount > 0 && (sellCount + row.Quantity) > sCount)
                {
                    //开启了库存限制，购买数量超过了库存
                    result.Code = "113026";
                    result.Message = "创建订单异常，选购产品的数量超过了购买限制";
                    return result;
                }
            }
            result.Status = true;
            return result;
        }


        /// <summary>
        /// 取消订单基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <param name="otaBusinessId"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderCancelRequest(OrderCancelRequest request, int otaBusinessId)
        {
            var result = new DataValidResult { Status = false };
            if (string.IsNullOrEmpty(request.Body.OrderInfo.OrderId))
            {
                result.Code = "114001";
                result.Message = "订单取消失败，订单id不能为空";
                return result;
            }
            if (request.Body.OrderInfo.OrderQuantity <= 0)
            {
                result.Code = "114003";
                result.Message = "订单取消失败，订单总票数不能小于1";
                return result;
            }
            var tbl_Order = Get(request.Body.OrderInfo.OrderId);
            if (tbl_Order == null)
            {
                result.Code = "114004";
                result.Message = "订单取消失败，订单不存在";
                return result;
            }
            if (tbl_Order.OTABusinessId != otaBusinessId)
            {
                result.Code = "114005";
                result.Message = "订单取消失败，订单存在，但不属于该调用者";
                return result;
            }
            result.Status = true;
            return result;
        }

        /// <summary>
        /// 查询订单基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderQueryRequest(OrderQueryRequest request)
        {
            var result = new DataValidResult { Status = false };
            if (string.IsNullOrEmpty(request.Body.OrderId))
            {
                result.Code = "115001";
                result.Message = "查询订单异常，订单id不能为空";
                return result;
            }
            result.Status = true;
            return result;
        }

        /// <summary>
        /// 修改订单基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForOrderUpdateRequest(OrderUpdateRequest request)
        {
            var orderInfo = request.Body.OrderInfo;
            var result = new DataValidResult { Status = false };
            if (orderInfo.ContactPerson == null)
            {
                result.Code = "116001";
                result.Message = "修改订单异常，取票人信息为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Name))
            {
                result.Code = "116002";
                result.Message = "修改订单异常，取票人姓名不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "116003";
                result.Message = "修改订单异常，取票人手机号码不能为空";
                return result;
            }
            if (!RegexValidation.IsCellPhone(orderInfo.ContactPerson.Mobile))
            {
                result.Code = "116004";
                result.Message = "修改订单异常，取票人手机号码异常";
                return result;
            }
            if (!string.IsNullOrEmpty(orderInfo.ContactPerson.CardType))
            {
                switch (orderInfo.ContactPerson.CardType.ToUpper())
                {
                    case "ID_CARD":
                        if (string.IsNullOrEmpty(orderInfo.ContactPerson.CardNo))
                        {
                            result.Code = "116005";
                            result.Message = "修改订单异常，游客身份证信息不能为空";
                            return result;
                        }
                        if (!RegexValidation.IsIdCard(orderInfo.ContactPerson.CardNo))
                        {
                            result.Code = "116006";
                            result.Message = "修改订单异常，游客身份证信息输入有误";
                            return result;
                        }
                        break;
                    case "HUZHAO":
                    case "TAIBAO":
                    case "GANGAO":
                    case "OTHER":
                        break;
                    default:
                        result.Code = "116007";
                        result.Message = "修改订单异常，取票人证件类型异常";
                        return result;
                }
            }
            else
            {
                result.Code = "116008";
                result.Message = "修改订单异常，取票人证件类型不能为空";
                return result;
            }
            if (!orderInfo.VisitDate.IsDataTime())
            {
                result.Code = "116009";
                result.Message = "修改订单异常，游玩日期格式不合法";
                return result;
            }

            if (orderInfo.VisitDate.ToDataTime() < DateTime.Now.Date)
            {
                result.Code = "116010";
                result.Message = "修改订单异常，游玩日期不能小于今天";
                return result;
            }
            result.Status = true;
            return result;
        }

        /// <summary>
        /// (重)发送入园凭证短信 基础数据验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DataValidResult ValidDataForMessageSendRequest(MessageSendRequest request)
        {
            var orderInfo = request.Body.OrderInfo;
            var result = new DataValidResult { Status = false };
            if (string.IsNullOrEmpty(orderInfo.OrderId))
            {
                result.Code = "117001";
                result.Message = "(重)发送入园凭证短信异常，订单id不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(orderInfo.phoneNumber))
            {
                result.Code = "117002";
                result.Message = "(重)发送入园凭证短信异常，重发手机号码不能为空";
                return result;
            }
            if (!RegexValidation.IsCellPhone(orderInfo.phoneNumber))
            {
                result.Code = "117003";
                result.Message = "(重)发送入园凭证短信异常，重发手机号码异常";
                return result;
            }
            result.Status = true;
            return result;
        }
    }
}
