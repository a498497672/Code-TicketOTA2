using Ticket.Core.Repository;

namespace Ticket.Core.Service
{
    //年票
    public class YearTicketService
    {
        private readonly YearTicketRepository _yearTicketRepository;
        public YearTicketService(YearTicketRepository yearTicketRepository)
        {
            _yearTicketRepository = yearTicketRepository;
        }
    }
}
