using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.Ticket
{
    public class TblTicketViewModel
    {
        public int Id { get; set; }
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public int MinOQ { get; set; }
        public int MaxOQ { get; set; }
        public int RuleId { get; set; }
    }
}
