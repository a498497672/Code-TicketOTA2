using System;
using System.Collections.Generic;
using Ticket.Core.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Model.Result;
using Ticket.Model.Model.TravelAgency;
using Ticket.Utility.Extensions;
using Ticket.SqlSugar.Models;
using Ticket.Utility.Helpers;
using Ticket.Model.Enum.TraveAgency;
using Ticket.Model.Enum;

namespace Ticket.Core.Service
{
    public class TravelAgencyOrderService
    {
        private readonly TravelAgencyOrderRepository _travelAgencyOrderRepository;
        private readonly OtaBusinessService _otaBusinessService;
        public TravelAgencyOrderService(
            TravelAgencyOrderRepository travelAgencyOrderRepository,
            OtaBusinessService otaBusinessService)
        {
            _travelAgencyOrderRepository = travelAgencyOrderRepository;
            _otaBusinessService = otaBusinessService;
        }

        public void BeginTran()
        {
            _travelAgencyOrderRepository.BeginTran();
        }
        public void CommitTran()
        {
            _travelAgencyOrderRepository.CommitTran();
        }
        public void RollbackTran()
        {
            _travelAgencyOrderRepository.RollbackTran();
        }

        public Tbl_TravelAgencyOrder Get(int id)
        {
            return _travelAgencyOrderRepository.Single(a => a.Id == id);
        }

        public TPageResult<OrderViewModel> GetList(OrderQueryModel model)
        {
            var where = PredicateBuilder.True<Tbl_TravelAgencyOrder>();
            where = PredicateBuilder.And(@where, x => x.OTABusinessId == model.OTABusinessId);
            if (!string.IsNullOrEmpty(model.OrderNo))
            {
                where = PredicateBuilder.And(@where, x => x.OrderNo.Contains(model.OrderNo));
            }
            if (!string.IsNullOrEmpty(model.Mobile))
            {
                where = PredicateBuilder.And(@where, x => x.Mobile == model.Mobile);
            }
            if (!string.IsNullOrEmpty(model.Linkman))
            {
                where = PredicateBuilder.And(@where, x => x.Linkman.Contains(model.Linkman));
            }
            if (model.OrderStatus > 0)
            {
                where = PredicateBuilder.And(@where, x => x.OrderStatus == model.OrderStatus);
            }
            if (model.AuditStatus > 0)
            {
                where = PredicateBuilder.And(@where, x => x.AuditStatus == model.AuditStatus);
            }
            if (model.PlaceOrderType > 0)
            {
                where = PredicateBuilder.And(@where, x => x.PlaceOrderType == model.PlaceOrderType);
            }
            if (model.ValidityDate.HasValue)
            {
                where = PredicateBuilder.And(@where, x => x.ValidityDateStart == model.ValidityDate.Value);
            }
            var total = 0;
            var list = _travelAgencyOrderRepository.GetPageList(model.Limit, model.Page, out total, where, a => a.CreateTime, false);
            var result = new TPageResult<OrderViewModel>();
            var data = list.Select(a => new OrderViewModel
            {
                Id = a.Id,
                IdCard = a.IdCard,
                Mobile = a.Mobile,
                OTABusinessName = a.OTABusinessName,
                AuditStatus = a.AuditStatus,
                BookCount = a.BookCount,
                CreateTime = a.CreateTime,
                Linkman = a.Linkman,
                OrderNo = a.OrderNo,
                OrderStatus = a.OrderStatus,
                PlaceOrderType = a.PlaceOrderType,
                TotalAmount = a.TotalAmount,
                ValidityDate = a.ValidityDateStart.ToString("yyyy-MM-dd"),
                PlaceOrderName = a.PlaceOrderName
            }).ToList();
            return result.SuccessResult(data, total);
        }

        public Tbl_TravelAgencyOrder Add(OrderAddModel model)
        {
            var business = _otaBusinessService.Get(model.OtaBusinessId);
            var order = new Tbl_TravelAgencyOrder
            {
                OrderNo = OrderHelper.GenerateOrderNo(),
                OTABusinessId = model.OtaBusinessId,
                OTABusinessName = business.FullName,
                ValidityDateStart = model.ValidityDate,
                ValidityDateEnd = model.ValidityDate,
                BookCount = model.TicketItem.Sum(a => a.BookCount),
                TotalAmount = model.TicketItem.Sum(a => a.BookCount * a.Price),
                IdCard = model.IdCard,
                Linkman = model.Linkman,
                Mobile = model.Mobile,
                Remark = model.Remark,
                PlaceOrderName = model.PlaceOrderName,
                CreateTime = DateTime.Now,
                PayType = (int)TraveAgencyPayType.UnderLine,
                PlaceOrderType = (int)TraveAgencyPlaceOrderType.TraveAgency,
                AuditStatus = (int)TraveAgencyAuditStatus.WaitAudit,
                OrderStatus = (int)TraveAgencyOrderStatus.NoPay
            };
            _travelAgencyOrderRepository.Add(order);
            return order;
        }

        public void Delete(Tbl_TravelAgencyOrder tbl_TravelAgencyOrder)
        {
            _travelAgencyOrderRepository.Delete(tbl_TravelAgencyOrder);
        }

        public void Update(Tbl_TravelAgencyOrder tbl_TravelAgencyOrder, OrderDetailViewModel model)
        {
            tbl_TravelAgencyOrder.ValidityDateStart = model.ValidityDate;
            tbl_TravelAgencyOrder.ValidityDateEnd = model.ValidityDate;
            tbl_TravelAgencyOrder.BookCount = model.TicketItem.Sum(a => a.BookCount);
            tbl_TravelAgencyOrder.TotalAmount = model.TicketItem.Sum(a => a.BookCount * a.Price);
            tbl_TravelAgencyOrder.IdCard = model.IdCard;
            tbl_TravelAgencyOrder.Linkman = model.Linkman;
            tbl_TravelAgencyOrder.Mobile = model.Mobile;
            tbl_TravelAgencyOrder.Remark = model.Remark;
            tbl_TravelAgencyOrder.PlaceOrderName = model.PlaceOrderName;
            tbl_TravelAgencyOrder.AuditStatus = (int)TraveAgencyAuditStatus.WaitAudit;
            _travelAgencyOrderRepository.Update(tbl_TravelAgencyOrder);
        }

        public void UpdateForOrderCancel(Tbl_TravelAgencyOrder tbl_TravelAgencyOrder)
        {
            tbl_TravelAgencyOrder.AuditStatus = (int)TraveAgencyAuditStatus.OrderCancelApplication;
            _travelAgencyOrderRepository.Update(tbl_TravelAgencyOrder);
        }

        /// <summary>
        /// 闸机入园更新[旅行社]状态
        /// </summary>
        /// <param name="orderNo"></param>
        public void UpdateForConsume(Tbl_OrderDetail tbl_OrderDetail, Tbl_Ticket_Testing tbl_Ticket_Testing)
        {
            if (tbl_OrderDetail.OrderSource == (int)OrderSource.OTA && tbl_Ticket_Testing.UsedQuantity <= 1)
            {
                var travelAgencyOrder = _travelAgencyOrderRepository.FirstOrDefault(a => a.OrderNo == tbl_Ticket_Testing.OrderNo);
                if (travelAgencyOrder != null)
                {
                    travelAgencyOrder.OrderStatus = (int)TraveAgencyOrderStatus.Consume;
                    _travelAgencyOrderRepository.Update(travelAgencyOrder);
                }
            }
        }
    }
}
