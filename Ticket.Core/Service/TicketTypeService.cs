using SqlSugar;
using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.Model.Model.Ticket;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    public class TicketTypeService
    {
        private readonly TicketTypeRepository _ticketTypeRepository;
        public TicketTypeService(TicketTypeRepository ticketTypeRepository)
        {
            _ticketTypeRepository = ticketTypeRepository;
        }

        public List<TicketTypeViewModel> GetList(int scenicId, int enterpriseUserId)
        {
            var ticketList = _ticketTypeRepository.db.Queryable<Tbl_EnterpriseUserTicket, Tbl_Ticket, Tbl_TicketType>((a, b, c) =>
                    new object[] {
                        JoinType.Left, a.TicketId == b.TicketId,
                        JoinType.Left, b.TypeId == c.Id
                    }).
                    Where((a, b, c) => a.EnterpriseUserId == enterpriseUserId && b.ScenicId == scenicId).
                    GroupBy((a, b, c) => new { c.Id, c.TypeName }).
                    //OrderBy((a, b, c) => c.CreateTime).
                    Select((a, b, c) => new TicketTypeViewModel
                    {
                        Id = c.Id,
                        Name = c.TypeName
                    }).ToList();
            return ticketList;
        }
    }
}
