namespace Ticket.Model.Model.TravelAgency
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public int BookCount { get; set; }
        public decimal TotalAmount { get; set; }       
        public int Min { get; set; }         
        public int Max { get; set; }
    }
}
