namespace Ticket.Model.Model.Report
{
    public class TicketSaleCount
    {
        public string TicketName { get; set; }
        public int TicketId { get; set; }
        public string ScenicName { get; set; }
        public string OrderNo { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
        public int RefundCount { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
