using Ticket.Core.Repository;

namespace Ticket.Core.Service
{
    public class TicketRelationService
    {
        private readonly TicketRelationRepository _ticketRelationRepository;

        public TicketRelationService(TicketRelationRepository ticketRelationRepository)
        {
            _ticketRelationRepository = ticketRelationRepository;
        }
    }
}
