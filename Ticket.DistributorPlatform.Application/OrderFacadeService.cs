using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Service;
using Ticket.Model.Enum.TraveAgency;
using Ticket.Model.Model.TravelAgency;
using Ticket.Model.Result;
using Ticket.Utility.Helpers;

namespace Ticket.DistributorPlatform.Application
{
    public class OrderFacadeService
    {
        private readonly TravelAgencyOrderService _travelAgencyOrderService;
        private readonly TravelAgencyOrderDetailService _travelAgencyOrderDetailService;
        private readonly TicketService _ticketService;
        public OrderFacadeService(
            TravelAgencyOrderService travelAgencyOrderService,
            TravelAgencyOrderDetailService travelAgencyOrderDetailService,
            TicketService ticketService)
        {
            _travelAgencyOrderService = travelAgencyOrderService;
            _travelAgencyOrderDetailService = travelAgencyOrderDetailService;
            _ticketService = ticketService;
        }

        public TPageResult<OrderViewModel> GetList(OrderQueryModel model)
        {
            return _travelAgencyOrderService.GetList(model);
        }

        public OrderDetailViewModel Get(int id)
        {
            var order = _travelAgencyOrderService.Get(id);
            var orderDetails = _travelAgencyOrderDetailService.GetList(order.OrderNo);
            var tbl_Tickets = _ticketService.GetList(orderDetails.Select(a => a.TicketId).ToList());
            var data = new OrderDetailViewModel
            {
                Id = order.Id,
                ValidityDate = order.ValidityDateStart,
                PlaceOrderTypeName = ((TraveAgencyPlaceOrderType)order.PlaceOrderType).GetDescription(),
                IdCard = order.IdCard,
                Linkman = order.Linkman,
                Mobile = order.Mobile,
                Remark = order.Remark,
                AuditStatus = order.AuditStatus,
                RejectTime = order.RejectTime,
                RejectReason = order.RejectReason,
                OrderStatus = order.OrderStatus,
                PlaceOrderName = order.PlaceOrderName,
                TicketItem = new List<TicketItemModel>()
            };
            foreach (var row in orderDetails)
            {
                var ticket = tbl_Tickets.FirstOrDefault(a => a.TicketId == row.TicketId);
                data.TicketItem.Add(new TicketItemModel
                {
                    TicketId = row.TicketId,
                    TicketName = row.TicketName,
                    BookCount = row.Quantity,
                    Price = row.Price,
                    TotalAmount = row.Quantity * row.Price,
                    Min = ticket.MinOQ,
                    Max = ticket.MaxOQ
                });
            }
            data.TicketItemJson = JsonSerializeHelper.ToJsonForlowercase(data.TicketItem);
            return data;
        }

        public TResult Add(OrderAddModel model)
        {
            var result = new TResult();
            if (model.TicketItem.Count <= 0)
            {
                return result.FailureResult("请选择您要购买的产品");
            }
            var ticketIds = model.TicketItem.Select(a => a.TicketId).ToList();
            if (ticketIds.Count <= 0)
            {
                result.FailureResult("请选择您要购买的产品");
            }
            var tickets = _ticketService.GetTicketIds(ticketIds, model.ValidityDate);
            foreach (var row in model.TicketItem)
            {
                var ticket = tickets.First(a => a.TicketId == row.TicketId);
                row.TicketName = ticket.TicketName;
                row.Price = ticket.SalePrice;
            }
            try
            {
                _travelAgencyOrderService.BeginTran();
                var order = _travelAgencyOrderService.Add(model);
                _travelAgencyOrderDetailService.Add(model, order);
                //提交事物
                _travelAgencyOrderService.CommitTran();
            }
            catch
            {
                _travelAgencyOrderService.RollbackTran();
            }
            return result.SuccessResult();
        }

        public TResult Update(OrderDetailViewModel model)
        {
            var result = new TResult();
            if (model.TicketItem.Count <= 0)
            {
                return result.FailureResult("请选择您要购买的产品");
            }
            var ticketIds = model.TicketItem.Select(a => a.TicketId).ToList();
            if (ticketIds.Count <= 0)
            {
                result.FailureResult("请选择您要购买的产品");
            }
            var tickets = _ticketService.GetTicketIds(ticketIds, model.ValidityDate);
            foreach (var row in model.TicketItem)
            {
                var ticket = tickets.First(a => a.TicketId == row.TicketId);
                row.TicketName = ticket.TicketName;
                row.Price = ticket.SalePrice;
            }
            var order = _travelAgencyOrderService.Get(model.Id);
            try
            {
                _travelAgencyOrderService.BeginTran();
                _travelAgencyOrderService.Update(order, model);
                _travelAgencyOrderDetailService.Delete(order.OrderNo);
                _travelAgencyOrderDetailService.Add(new OrderAddModel
                {
                    ValidityDate = model.ValidityDate,
                    TicketItem = model.TicketItem
                }, order);
                //提交事物
                _travelAgencyOrderService.CommitTran();
            }
            catch
            {
                _travelAgencyOrderService.RollbackTran();
            }
            return result.SuccessResult();
        }

        public TResult Delete(int id)
        {
            var result = new TResult();
            var order = _travelAgencyOrderService.Get(id);
            if (order.AuditStatus != (int)TraveAgencyAuditStatus.WaitAudit && order.AuditStatus != (int)TraveAgencyAuditStatus.Reject && order.AuditStatus != (int)TraveAgencyAuditStatus.OrderCancel)
            {
                return result.FailureResult("待审核和审核驳回的订单，才能删除");
            }
            try
            {
                _travelAgencyOrderService.BeginTran();
                _travelAgencyOrderService.Delete(order);
                _travelAgencyOrderDetailService.Delete(order.OrderNo);
                //提交事物
                _travelAgencyOrderService.CommitTran();
            }
            catch
            {
                _travelAgencyOrderService.RollbackTran();
            }
            return result.SuccessResult();
        }

        public TResult OrderCancel(int id)
        {
            var result = new TResult();
            var order = _travelAgencyOrderService.Get(id);
            if (order.AuditStatus != (int)TraveAgencyAuditStatus.Audited)
            {
                return result.FailureResult("已审核的订单，才能申请取消订单");
            }
            _travelAgencyOrderService.UpdateForOrderCancel(order);
            return result.SuccessResult();
        }
    }
}
