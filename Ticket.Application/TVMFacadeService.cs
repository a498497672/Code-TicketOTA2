using DocomSDK.TVM;
using FengjingSDK461.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Ticket.Core.Service;
using Ticket.Infrastructure.Alipay;
using Ticket.Infrastructure.KouDaiLingQian;
using Ticket.Infrastructure.WxPay;
using Ticket.Model.Docom;
using Ticket.Model.Enum;
using Ticket.Model.Model;
using Ticket.SqlSugar.Models;
using Ticket.Utility.Config;
using Ticket.Utility.Extensions;
using Ticket.Utility.Helpers;
using Ticket.Utility.Key;

namespace Ticket.Application
{
    public class TVMFacadeService
    {
        private readonly TicketTestingService _ticketTestingService;
        private readonly OrderDetailService _orderDetailService;
        private readonly OrderService _orderService;
        private readonly TicketService _ticketService;
        private readonly SaleLogService _saleLogService;
        private readonly AlipayPayGateway _alipayPayGateway;
        private readonly WxPayGateway _wxPayGateway;
        private readonly ScenicService _scenicService;
        private readonly NoticeOrderConsumedService _noticeOrderConsumedService;

        public TVMFacadeService(
            TicketTestingService ticketTestingService,
            OrderDetailService orderDetailService,
            OrderService orderService,
            TicketService ticketService,
            SaleLogService saleLogService,
            AlipayPayGateway alipayPayGateway,
            WxPayGateway wxPayGateway,
            ScenicService scenicService,
            NoticeOrderConsumedService noticeOrderConsumedService)
        {
            _ticketTestingService = ticketTestingService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _ticketService = ticketService;
            _saleLogService = saleLogService;
            _alipayPayGateway = alipayPayGateway;
            _wxPayGateway = wxPayGateway;
            _scenicService = scenicService;
            _noticeOrderConsumedService = noticeOrderConsumedService;
        }
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <returns></returns>
        public TVMResult<GUITikcetConfig> GetTicketList(TVMDevice device)
        {
            TVMResult<GUITikcetConfig> result = new TVMResult<GUITikcetConfig>
            {
                ResultCode = 1,
                SysDate = DateTime.Now,
                Message = "查询失败"
            };

            var scenic = _scenicService.Get(UserKey.ScenicId);
            if (scenic == null)
            {
                return result;
            }
            var ticketList = _ticketService.GetScanTicketList(UserKey.ScenicId, DateTime.Now.Date, device.DeviceKey);
            //if (ticketList.Count <= 0)
            //{
            //    return result;
            //}
            List<TVMParkInfo> ParkInfos = new List<TVMParkInfo>();
            TVMParkInfo ParkInfo = new TVMParkInfo();
            ParkInfo.TicketList = new List<TVMTicketType>();
            ParkInfo.ParkName = scenic.ScenicName;//景区名称
            ParkInfo.ParkName_En = "";
            ParkInfo.ParkType = 1;  //景区类型
            ParkInfo.TicketIndex = 0;//景区序号 
            foreach (var row in ticketList)
            {
                TVMTicketType TicketType = new TVMTicketType();
                TicketType.TicketID = row.TicketId;
                TicketType.TicketName = row.TicketName;
                TicketType.TicketName_En = "";
                TicketType.TicketPrice = Convert.ToDouble(row.TicketPrice);
                ParkInfo.TicketList.Add(TicketType);
            }
            ParkInfos.Add(ParkInfo);
            result.ResultCode = 0;
            result.Message = "成功";
            result.Data = new GUITikcetConfig()
            {
                tvmSpotsList = ParkInfos
            };
            return result;
        }

        /// <summary>
        /// 订单提取
        /// </summary>
        /// <param name="number">取票号码：身份证或凭证码</param>
        /// <param name="CheckType">取票类型 1、凭证码。2、身份证。3、扫码</param>
        /// <returns></returns>
        public TVMResult<TVMTicketInfo[]> GetTicketOrder(string number, int CheckType)
        {
            TVMResult<TVMTicketInfo[]> result = new TVMResult<TVMTicketInfo[]>
            {
                ResultCode = 1,
                SysDate = DateTime.Now,
                Message = "失败"
            };
            var ticketInfos = new List<TVMTicketInfo>();
            List<Tbl_OrderDetail> orderDetails = new List<Tbl_OrderDetail>();
            if (CheckType == 2)
            {
                //身份证
                var list = _orderDetailService.GetOtaByIdCard(UserKey.ScenicId, number);
                if (list.Count <= 0)
                {
                    return result;
                }
                orderDetails.AddRange(list);
            }
            else
            {
                //凭证码
                var orderDetail = _orderDetailService.GetOtaByCertificateNo(UserKey.ScenicId, number);
                if (orderDetail == null)
                {
                    return result;
                }
                orderDetails.Add(orderDetail);
            }
            foreach (var orderDetail in orderDetails)
            {
                if (orderDetail.DelayCheckTime > DateTime.Now)
                {
                    //该票未到入园时间
                    continue;
                }
                string code = SecurityExtension.DesEncrypt(orderDetail.QRcode, AppSettingsConfig.QrCodeKey);
                ticketInfos.Add(new TVMTicketInfo
                {
                    OrderID = orderDetail.OrderNo,//订单详情id
                    TicketName = orderDetail.TicketName,//门票名称
                    GeneralPrice = (double)(orderDetail.SettlementPrice.HasValue ? orderDetail.SettlementPrice : 0),//门票票面价格
                    DiscountPrice = (double)orderDetail.Price,//门票销售价格
                    StartDate = orderDetail.ValidityDateStart,//开始日期
                    EndDate = orderDetail.ValidityDateEnd,//结束日期
                    ChannelType = "",//
                    ENSBARCODE = code,//加密二维码
                    sbarcode = orderDetail.CertificateNO,//凭证号
                    PeopleCount = orderDetail.Quantity,//人数
                    Remarks = orderDetail.OrderDetailId.ToString(),//订单详情id
                    StartTime = orderDetail.ValidityDateStart,
                    EndTime = orderDetail.ValidityDateEnd,
                    SellPlan = "",//
                });
            }
            if (ticketInfos.Count <= 0)
            {
                return result;
            }
            result.Data = ticketInfos.ToArray();
            result.ResultCode = 0;
            result.Message = "成功";
            return result;
        }

        /// <summary>
        /// 订单项核销
        /// </summary>
        /// <param name="qRcodes"></param>
        public TVMResult<bool> VerifyTicket(List<TVMTicketInfo> list)
        {
            TVMResult<bool> result = new TVMResult<bool>
            {
                ResultCode = 1,
                SysDate = DateTime.Now,
                Data = false,
                Message = "失败"
            };
            try
            {
                _orderService.BeginTran();
                foreach (var row in list)
                {
                    var tbl_OrderDetail = _orderDetailService.UpdatePrintTicketStatus(Convert.ToInt32(row.Remarks));
                    _noticeOrderConsumedService.Update(tbl_OrderDetail);
                }
                //提交事物
                _orderService.CommitTran();

                result.ResultCode = 0;
                result.Message = "成功";
                result.Data = true;
                return result;
            }
            catch
            {
                _orderService.RollbackTran();
                return result;
            }

        }

        public TVMExtendData GetScanTicketList(TVMExtendData model)
        {
            var queryData = _ticketService.CheckTVMTicketQueryDataRequest(model.Data);
            if (queryData == null)
            {
                model.Data = "";
                return model;
            }
            var ticketList = _ticketService.GetScanTicketList(UserKey.ScenicId, queryData.playTime, model.Device.DeviceKey);
            model.Data = JsonHelper.ObjectToJsonStr(ticketList);
            return model;
        }


        /// <summary>
        /// 创建订单并支付
        /// </summary>
        /// <param name="quickPayment_Object"></param>
        /// <returns></returns>
        public TVMResult<TVMPayInfo> QuickPayment(QuickPayment_Object quickPayment_Object)
        {
            var result = new TVMResult<TVMPayInfo>
            {
                ResultCode = 1,
                SysDate = DateTime.Now,
                Message = "订单创建异常，订单创建失败"
            };
            TVMPayInfo payInfo = quickPayment_Object.PayData;

            if (payInfo.postOrder.Count == 0)
            {
                result.Message = "未找到门票信息";
                return result;
            }
            if (string.IsNullOrEmpty(payInfo.ScannerPayCode))
            {
                result.Message = "支付码未被扫描到，请联系管理员";
                return result;
            }
            int payType = 0;//道控支付类型 0：支付宝 1：微信
            if (!int.TryParse(payInfo.payType, out payType) || payType < 0 || payType >= 2)
            {
                result.Message = "支付方式不明确，请联系管理员";
                return result;
            }
            payType = payType == 1 ? (int)PayStatus.Wechat : (int)PayStatus.Alipay;
            var orderInfo = new OrderInfoModel
            {
                Code = payInfo.ScannerPayCode,//支付码
                PayType = payType,//支付类型 1：支付宝 2：微信
                ValidityDate = DateTime.Now.Date,
                Mobile = "",
                Linkman = "",
                TicketCategory = (int)TicketCategoryStatus.QrCodePrintTicket,
                TicketSource = (int)TicketSourceStatus.ScenicSpot,
                TicketItem = new List<TicketItemModel>()
            };
            foreach (var row in payInfo.postOrder)
            {
                orderInfo.TicketItem.Add(new TicketItemModel
                {
                    TicketId = row.ProductID,
                    BookCount = row.ProductCount
                });
            }

            var tbl_Tickets = _ticketService.GetTickets(payInfo.postOrder.Select(a => a.ProductID).ToList());
            var tbl_Order = _orderService.AddOrderForNoPay(orderInfo);
            var tbl_OrderDetails = _orderDetailService.AddOrderDetailForQrCodeNoPay(orderInfo, tbl_Order);
            _orderService.UpdateOrder(tbl_Order, tbl_OrderDetails);
            var tbl_Ticket_Testing = _ticketTestingService.addTicketTestings(tbl_Order, tbl_OrderDetails);
            _ticketService.UpdateTicketBySellCount(tbl_Tickets, tbl_OrderDetails);
            var tbl_SaleLog = _saleLogService.addSaleLog(tbl_Order);


            //if (orderInfo.PayType == (int)PayStatus.Wechat)
            //{
            //    var payResult = _wxPayGateway.OrderPay(tbl_Order.TicketName, tbl_Order.TotalAmount, payInfo.ScannerPayCode);
            //    if (!payResult.Success)
            //    {
            //        result.Message = payResult.Message;
            //        return result;
            //    }
            //    tbl_Order.PayTradeNo = payResult.OutTradeNo;
            //}
            //else if (orderInfo.PayType == (int)PayStatus.Alipay)
            //{
            //    var payResult = _alipayPayGateway.OrderPay(tbl_Order.TicketName, tbl_Order.TotalAmount.ToString(), payInfo.ScannerPayCode);
            //    if (!payResult.Success)
            //    {
            //        result.Message = payResult.Message;
            //        return result;
            //    }
            //    tbl_Order.PayTradeNo = payResult.OutTradeNo;
            //}

            var payResult = KouDaiLingQianGateway.Pay(tbl_Order.TotalAmount.ToString(), payInfo.ScannerPayCode, tbl_Order.OrderNo);
            if (!payResult.Success)
            {
                result.Message = payResult.Message;
                return result;
            }
            tbl_Order.PayTradeNo = payResult.OutTradeNo;

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
                result.ResultCode = 0;
                result.Message = "成功";
                result.Data = new TVMPayInfo()
                {
                    payResult = true,
                    payType = tbl_Order.PayType.ToString(),
                    PayMsg = "支付成功",
                    return_trade_no = tbl_Order.PayTradeNo,
                    m_PayPassOrderID = tbl_Order.OrderNo,
                    merchantCode = quickPayment_Object.PayData.merchantCode,//商户id
                    terminalNo = quickPayment_Object.PayData.terminalNo,//终端编号
                    TerminalType = quickPayment_Object.PayData.TerminalType,//终端类型
                    strPaymoney = tbl_Order.TotalAmount.ToString(),//支付金额
                    ScannerPayCode = quickPayment_Object.PayData.ScannerPayCode,//支付码
                    postOrder = quickPayment_Object.PayData.postOrder
                };
                return result;
            }
            catch (Exception ex)
            {
                if (orderInfo.PayType == (int)PayStatus.Wechat && !string.IsNullOrEmpty(tbl_Order.PayTradeNo))
                {
                    var isCancel = _wxPayGateway.Cancel(tbl_Order.PayTradeNo);
                    if (!isCancel)
                    {
                        result.Message = "系统异常，订单创建失败，请联系景区人员进行退款!";
                    }
                }
                else if (orderInfo.PayType == (int)PayStatus.Alipay && !string.IsNullOrEmpty(tbl_Order.PayTradeNo))
                {
                    var isCancel = _alipayPayGateway.Cancel(tbl_Order.PayTradeNo, tbl_Order.TotalAmount.ToString());
                    if (!isCancel)
                    {
                        result.Message = "系统异常，订单创建失败，请联系景区人员进行退款!";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询支付情况
        /// </summary>
        /// <param name="quickPayment_Object"></param>
        /// <returns></returns>
        public TVMResult<TVMPayInfo> QueryPay(QuickPayment_Object quickPayment_Object)
        {
            var result = new TVMResult<TVMPayInfo>
            {
                ResultCode = 1,
                SysDate = DateTime.Now,
                Message = "查询失败"
            };
            TVMPayInfo payInfo = quickPayment_Object.PayData;

            if (quickPayment_Object.PayData == null)
            {
                result.Message = "查询失败，信息丢失";
                return result;
            }

            //确认支付是否成功,每隔一段时间查询一次订单，共查询5次--订单有效时间10秒
            int queryTimes = 5;//查询次数计数器
            while (queryTimes-- > 0)
            {
                var tbl_Order = _orderService.GetByAuthCode(payInfo.ScannerPayCode);
                if (tbl_Order == null)
                {
                    Thread.Sleep(2000);
                    continue;
                }
                result.ResultCode = 0;
                result.Message = "成功";
                result.Data = new TVMPayInfo()
                {
                    payResult = true,
                    payType = tbl_Order.PayType.ToString(),
                    PayMsg = "支付成功",
                    return_trade_no = tbl_Order.PayTradeNo,
                    m_PayPassOrderID = tbl_Order.OrderNo,
                    merchantCode = quickPayment_Object.PayData.merchantCode,//商户id
                    terminalNo = quickPayment_Object.PayData.terminalNo,//终端编号
                    TerminalType = quickPayment_Object.PayData.TerminalType,//终端类型
                    strPaymoney = tbl_Order.TotalAmount.ToString(),//支付金额
                    ScannerPayCode = quickPayment_Object.PayData.ScannerPayCode,//支付码
                    postOrder = quickPayment_Object.PayData.postOrder
                };
                return result;
            }
            result.Message = "订单不存在";
            return result;
        }

        /// <summary>
        /// 销售门票请求票务详情
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TVMResult<List<SaleTicket_Result>> SaleTicket(QuickPayment_Object quickPayment_Object)
        {
            var result = new TVMResult<List<SaleTicket_Result>>
            {
                ResultCode = 1,
                SysDate = DateTime.Now,
                Message = "失败",
                Data = new List<SaleTicket_Result>()
            };
            TVMPayInfo payInfo = quickPayment_Object.PayData;
            if (quickPayment_Object.PayData == null)
            {
                result.Message = "查询失败，信息丢失";
                return result;
            }
            var tbl_Order = _orderService.Get(payInfo.m_PayPassOrderID);
            if (tbl_Order == null)
            {
                result.Message = "订单不存在";
                return result;
            }
            var orderDetails = _orderDetailService.GetList(payInfo.m_PayPassOrderID);
            try
            {
                _orderService.BeginTran();
                foreach (var row in orderDetails)
                {
                    var tbl_OrderDetail = _orderDetailService.UpdatePrintTicketStatus(row.OrderDetailId);
                    _noticeOrderConsumedService.Update(tbl_OrderDetail);
                }
                //提交事物
                _orderService.CommitTran();
            }
            catch
            {
                _orderService.RollbackTran();
            }
            foreach (var row in orderDetails)
            {
                string code = SecurityExtension.DesEncrypt(row.QRcode, AppSettingsConfig.QrCodeKey);
                result.Data.Add(new SaleTicket_Result
                {
                    OrderID = row.OrderNo,//订单详情id
                    StrInvoiceCode = code,//交易号
                    TicketName = row.TicketName,
                    TicketId = row.TicketId.ToString(),
                    TypeName = "",
                    StartDate = row.ValidityDateStart,
                    EndDate = row.ValidityDateEnd,
                    Price = Convert.ToDouble(row.Price),
                    GeneralPrice = Convert.ToDouble(row.Price * row.Quantity),
                    PeopleCount = row.Quantity,
                    ENSBARCODE = code,//加密的条码
                    Sbarcode = row.CertificateNO,//凭证号
                    Remarks = "限当日有效"
                });
            }
            result.ResultCode = 0;
            result.Message = "成功";
            return result;
        }
    }
}
