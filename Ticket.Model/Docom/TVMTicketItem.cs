namespace Ticket.Model.Docom
{
    public class TVMTicketItem
    {
        /// <summary>
        /// 门票名称
        /// </summary>
        public string TicketName { get; set; }
        /// <summary>
        /// 门票id
        /// </summary>
        public int TicketId { get; set; }
        /// <summary>
        /// 门票价格
        /// </summary>
        public decimal TicketPrice { get; set; }
        /// <summary>
        /// 最小起购数
        /// </summary>
        public int MinCount { get; set; }
        /// <summary>
        /// 最大订购数
        /// </summary>
        public int MaxCount { get; set; }
    }
}
