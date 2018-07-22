namespace Ticket.Infrastructure.Print.Request
{
    /// <summary>
    /// 打印报表
    /// </summary>
    public class PrintReportData
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        public int RefundTotalCount { get; set; }
        public decimal RefundTotalAmount { get; set; }
        /// <summary>
        /// 散客票
        /// </summary>
        public PrintTicketList PrintBulkTicket { get; set; }
        /// <summary>
        /// 团队票
        /// </summary>
        public PrintTicketList PrintTeamTicket { get; set; }
    }
}
