using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Request
{
    public class QueryOrderRequest
    {
        /// <summary>
        /// 携程处理批次流水号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string SupplierOrderId { get; set; }
    }
}
