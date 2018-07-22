using System;

namespace Ticket.Model.Model.Ticket
{
    public class TblTicketQueryModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int ScenicId { get; set; }
        public int UserId { get; set; }
        public int TicketTypeId { get; set; }
        public DateTime PlayTime { get; set; }
    }
}
