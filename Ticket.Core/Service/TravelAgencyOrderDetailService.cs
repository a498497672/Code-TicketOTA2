using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Enum.TraveAgency;
using Ticket.Model.Model.TravelAgency;

namespace Ticket.Core.Service
{
    public class TravelAgencyOrderDetailService
    {
        private readonly TravelAgencyOrderDetailRepository _travelAgencyOrderDetailRepository;
        public TravelAgencyOrderDetailService(
            TravelAgencyOrderDetailRepository travelAgencyOrderDetailRepository)
        {
            _travelAgencyOrderDetailRepository = travelAgencyOrderDetailRepository;
        }

        public List<Tbl_TravelAgencyOrderDetail> GetList(string orderNo)
        {
            return _travelAgencyOrderDetailRepository.GetAllList(a => a.OrderNo == orderNo);
        }

        public void Add(OrderAddModel model, Tbl_TravelAgencyOrder tbl_TravelAgencyOrder)
        {
            foreach (var row in model.TicketItem)
            {
                var order = new Tbl_TravelAgencyOrderDetail
                {
                    OrderNo = tbl_TravelAgencyOrder.OrderNo,
                    ValidityDateStart = model.ValidityDate,
                    ValidityDateEnd = model.ValidityDate,
                    Price = row.Price,
                    Quantity = row.BookCount,
                    TicketId = row.TicketId,
                    TicketName = row.TicketName,
                    OrderStatus = (int)TraveAgencyOrderStatus.NoPay
                };
                _travelAgencyOrderDetailRepository.Add(order);
            }
        }

        public void Delete(string orderNo)
        {
            var list = _travelAgencyOrderDetailRepository.GetAllList(a => a.OrderNo == orderNo);
            foreach (var row in list)
            {
                _travelAgencyOrderDetailRepository.Delete(row);
            }
        }
    }
}
