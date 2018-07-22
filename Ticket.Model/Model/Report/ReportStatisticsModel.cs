using System.Collections.Generic;

namespace Ticket.Model.Model.Report
{
    public class ReportStatisticsModel
    {
        public decimal TotalAmount { get; set; }
        public int TotalCount { get; set; }
        public decimal AlipayAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal WxPayAmount { get; set; }
        public List<TicketSaleCount> List { get; set; }
        public decimal TotalRefundAmount { get; set; }
        public int TotalRefundCount { get; set; }
        public decimal AlipayRefundAmount { get; set; }
        public decimal CashRefundAmount { get; set; }
        public decimal WxPayRefundAmount { get; set; }
    }
}
