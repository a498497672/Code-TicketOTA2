using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Service;
using Ticket.Model.Model.TravelAgency;
using Ticket.Model.Result;

namespace Ticket.DistributorPlatform.Application
{
    public class TicketFacadeService
    {
        private readonly TicketService _ticketService;
        public TicketFacadeService(TicketService  ticketService)
        {
            _ticketService = ticketService;
        }

        public TPageResult<TicketViewModel> GetPageList(TicketQueryModel model)
        {
            return _ticketService.GetPageList(model);
        }
    }
}
