using System;

namespace Ticket.Model.Model.TravelAgency
{
    public class TicketQueryModel : PageBase
    {
        public int OTABusinessId { get; set; }
        public string TicketName { get; set; }
        public DateTime PlayDate { get; set; }
    }
}
