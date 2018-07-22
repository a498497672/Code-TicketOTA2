using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Repository;
using Ticket.Model.Enum;
using Ticket.Model.Result;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 退款
    /// </summary>
    public class RefundDetailService
    {
        private readonly RefundDetailRepository _refundDetailRepository;
        private readonly OrderService _orderService;
        private readonly OrderDetailRepository _orderDetailRepository;
        private readonly TicketTestingRepository _ticketTestingRepository;
        private readonly TicketRepository _ticketRepository;
        public RefundDetailService(
            RefundDetailRepository refundDetailRepository,
            OrderService orderService,
            OrderDetailRepository orderDetailRepository,
            TicketTestingRepository ticketTestingRepository,
            TicketRepository ticketRepository)
        {
            _refundDetailRepository = refundDetailRepository;
            _orderService = orderService;
            _orderDetailRepository = orderDetailRepository;
            _ticketTestingRepository = ticketTestingRepository;
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// 添加退款记录
        /// </summary>
        /// <param name="tbl_OrderDetail"></param>
        public Tbl_RefundDetail Add(Tbl_OrderDetail tbl_OrderDetail)
        {
            Tbl_RefundDetail tbl_RefundDetail = new Tbl_RefundDetail
            {
                OrderNo = tbl_OrderDetail.OrderNo,
                EnterpriseId = tbl_OrderDetail.EnterpriseId,
                ScenicId = tbl_OrderDetail.ScenicId,
                SellerId = tbl_OrderDetail.SellerId,
                SellerType = tbl_OrderDetail.SellerType,
                TicketSource = tbl_OrderDetail.TicketSource,
                TicketCategory = tbl_OrderDetail.TicketCategory,
                UsedQuantity = tbl_OrderDetail.UsedQuantity,
                TicketId = tbl_OrderDetail.TicketId,
                TicketName = tbl_OrderDetail.TicketName,
                Quantity = tbl_OrderDetail.Quantity,
                Price = tbl_OrderDetail.Price,
                BarCode = tbl_OrderDetail.BarCode,
                Stub = tbl_OrderDetail.Stub,
                CertificateNO = tbl_OrderDetail.CertificateNO,
                WindowId = tbl_OrderDetail.WindowId,
                IDCard = tbl_OrderDetail.IDCard,
                Linkman = tbl_OrderDetail.Linkman,
                Mobile = tbl_OrderDetail.Mobile,
                RefundStatus = 0,//退款状态
                RefundQuantity = tbl_OrderDetail.Quantity,
                RefundFee = 0,
                RefundTotalAmount = (tbl_OrderDetail.Price * tbl_OrderDetail.Quantity),
                RefundSummary = "",
                OrderTime = tbl_OrderDetail.CreateTime,
                ValidityDateStart = tbl_OrderDetail.ValidityDateStart,
                ValidityDateEnd = tbl_OrderDetail.ValidityDateEnd,
                PrintCount = tbl_OrderDetail.PrintCount,
                Qrcode = tbl_OrderDetail.QRcode,
                QrcodeUrl = tbl_OrderDetail.QRcodeUrl,
                OrderDetailId = tbl_OrderDetail.OrderDetailId,

                CreateTime = DateTime.Now,
                CreateUserId = 0
            };
            _refundDetailRepository.Add(tbl_RefundDetail);
            return tbl_RefundDetail;
        }

        /// <summary>
        /// 退票
        /// </summary>
        /// <returns></returns>
        public TResult RefundOrder(int orderDetailId, int userId)
        {
            var result = new TResult();
            var orderDetail = _orderDetailRepository.FirstOrDefault(o => o.OrderDetailId == orderDetailId);
            if (orderDetail == null)
            {
                return result.ErrorResult("订单信息有误");
            }
            var order = _orderService.Get(orderDetail.OrderNo);
            if (!orderDetail.CanRefund)
            {
                return result.ErrorResult("门票不支持退票");
            }
            if (orderDetail.CanRefundTime.HasValue && orderDetail.CanRefundTime < DateTime.Now.Date)
            {
                return result.ErrorResult("门票已过可退票时间，不能退票");
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Consume || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel)
            {
                return result.ErrorResult("门票已消费或已退票(取消)");
            }
            orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Refund;

            //印刷票 退票 库存状态修改为在售
            //记录条形码
            var barcode = string.Empty;
            if (orderDetail.TicketCategory == 1)
            {
                barcode = orderDetail.BarCode;
            }
            //退票后该条码能继续激活
            orderDetail.BarCode = "";
            Tbl_RefundDetail refDtl = new Tbl_RefundDetail
            {
                OrderNo = orderDetail.OrderNo,
                EnterpriseId = orderDetail.EnterpriseId,
                ScenicId = orderDetail.ScenicId,
                SellerId = orderDetail.SellerId,
                SellerType = orderDetail.SellerType,
                TicketSource = orderDetail.TicketSource,
                TicketCategory = orderDetail.TicketCategory,
                UsedQuantity = orderDetail.UsedQuantity,
                TicketId = orderDetail.TicketId,
                TicketName = orderDetail.TicketName,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                BarCode = orderDetail.BarCode,
                Stub = orderDetail.Stub,
                CertificateNO = orderDetail.CertificateNO,
                WindowId = orderDetail.WindowId,
                IDCard = orderDetail.IDCard,
                Linkman = orderDetail.Linkman,
                Mobile = orderDetail.Mobile,
                RefundStatus = 0,//退款状态
                RefundQuantity = orderDetail.Quantity,
                RefundFee = 0,
                RefundTotalAmount = (orderDetail.Price * orderDetail.Quantity),
                RefundSummary = "",
                OrderTime = orderDetail.CreateTime,
                ValidityDateStart = orderDetail.ValidityDateStart,
                ValidityDateEnd = orderDetail.ValidityDateEnd,
                PrintCount = orderDetail.PrintCount,
                Qrcode = orderDetail.QRcode,
                QrcodeUrl = orderDetail.QRcodeUrl,
                OrderDetailId = orderDetail.OrderDetailId,
                CreateTime = DateTime.Now,
                CreateUserId = userId
            };

            var ticketTesting = _ticketTestingRepository.FirstOrDefault(o => o.OrderDetailId == orderDetail.OrderDetailId || o.OrderDetailNumber == orderDetail.Number);
            var ticket = _ticketRepository.FirstOrDefault(o => o.TicketId == orderDetail.TicketId);

            try
            {
                _refundDetailRepository.BeginTran();
                _orderDetailRepository.Update(orderDetail);
                _ticketTestingRepository.Delete(ticketTesting);
                _refundDetailRepository.Add(refDtl);
                if (ticket != null)
                {
                    int? selCount = 0;
                    if (ticket.SellCount > orderDetail.Quantity)
                        selCount = ticket.SellCount - orderDetail.Quantity;

                    ticket.SellCount = ticket.SellCount.HasValue ? selCount : 0;
                    _ticketRepository.Update(ticket);
                }
                _refundDetailRepository.CommitTran();
            }
            catch
            {
                _refundDetailRepository.RollbackTran();
                return result.ErrorResult();
            }
            return result.SuccessResult();
        }

    }
}
