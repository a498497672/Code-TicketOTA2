using System;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Enum;
using Ticket.Utility.Extensions;

namespace Ticket.Core.Service
{
    public class SaleLogService
    {
        private readonly SaleLogRepository _saleLogRepository;

        public SaleLogService(SaleLogRepository saleLogRepository)
        {
            _saleLogRepository = saleLogRepository;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="order"></param>
        public Tbl_SaleLog addSaleLog(Tbl_Order order)
        {
            var model = new Tbl_SaleLog
            {
                EnterpriseId = order.EnterpriseId,
                ScenicId = order.ScenicId,
                LogContent = ActionStatus.SaleTicket.GetDescriptionByName(),
                OrderNo = order.OrderNo,
                TicketName = order.TicketName,
                Quantity = order.BookCount,
                TotalAmount = order.TotalAmount,
                ActivationCode = "",
                CreateTime = DateTime.Now,
                DataStatus = 0,
                CreateUserId = 0
            };
            return model;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="order"></param>
        public void Add(Tbl_Order order)
        {
            var model = new Tbl_SaleLog
            {
                EnterpriseId = order.EnterpriseId,
                ScenicId = order.ScenicId,
                LogContent = ActionStatus.SaleTicket.GetDescriptionByName(),
                OrderNo = order.OrderNo,
                TicketName = order.TicketName,
                Quantity = order.BookCount,
                TotalAmount = order.TotalAmount,
                ActivationCode = "",
                CreateTime = DateTime.Now,
                DataStatus = 0,
                CreateUserId = 0
            };
            _saleLogRepository.Add(model);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="tbl_RefundDetail"></param>
        public void Add(Tbl_RefundDetail tbl_RefundDetail)
        {
            var tbl_SaleLog = new Tbl_SaleLog
            {
                EnterpriseId = tbl_RefundDetail.EnterpriseId,
                ScenicId = tbl_RefundDetail.ScenicId,
                LogContent = ActionStatus.RefundTicket.GetDescriptionByName(),
                OrderNo = tbl_RefundDetail.OrderNo,
                TicketName = tbl_RefundDetail.TicketName,
                Quantity = tbl_RefundDetail.Quantity,
                TotalAmount = tbl_RefundDetail.Price,
                RefundQuantity = tbl_RefundDetail.Quantity,
                RefundFee = tbl_RefundDetail.RefundFee,
                RefundAmount = tbl_RefundDetail.RefundTotalAmount,
                ActivationCode = tbl_RefundDetail.BarCode,
                CreateTime = DateTime.Now,
                DataStatus = 0,
                CreateUserId = 0
            };
            _saleLogRepository.Add(tbl_SaleLog);
        }
    }
}
