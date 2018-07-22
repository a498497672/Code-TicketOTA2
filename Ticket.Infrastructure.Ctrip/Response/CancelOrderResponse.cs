using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Response
{

    public class CancelOrderResponse
    {
        public HeaderResponse header { get; set; }
        public string body { get; set; }
    }

    public class CancelOrderBodyRespose
    {
        /// <summary>
        /// 供应商确认类型
        /// 1.取消已确认（当 confirmType =1/2 时可同步返回确认结果）
        /// 2.取消待确认（当 confirmType =2 时需异步返回确认结果的）
        /// </summary>
        public int supplierConfirmType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CancelOrderitemRespose> items { get; set; }
    }

    public class CancelOrderitemRespose
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }
    }
}
