using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Request
{
    /// <summary>
    /// 取消
    /// </summary>
    public class CancelOrderRequest
    {
        /// <summary>
        /// 平台订单流水号
        /// </summary>
        public string orderSerialId { get; set; }
        /// <summary>
        /// 合作方订单号
        /// </summary>
        public string partnerOrderId { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 取消票数
        /// </summary>
        public int tickets { get; set; }
    }
}
