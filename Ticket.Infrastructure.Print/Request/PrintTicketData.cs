namespace Ticket.Infrastructure.Print.Request
{
    public class PrintTicketData
    {
        public int TicketId { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        public string TicketName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 小计
        /// </summary>
        public decimal TotalAmount { get; set; }

        public int TicketType { get; set; }
        public int PayType { get; set; }
    }
}
