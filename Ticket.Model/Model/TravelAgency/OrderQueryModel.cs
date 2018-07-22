using System;

namespace Ticket.Model.Model.TravelAgency
{
    /// <summary>
    /// 订单列表查询
    /// </summary>
    public class OrderQueryModel : PageBase
    {
        public int OTABusinessId { get; set; }
        public string OrderNo { get; set; }
        public string Linkman { get; set; }
        public string Mobile { get; set; }
        public int AuditStatus { get; set; }
        public int OrderStatus { get; set; }
        public int PlaceOrderType { get; set; }
        public DateTime? ValidityDate { get; set; }
    }
}
