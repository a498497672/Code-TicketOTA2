using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Request
{
    public class QueryOrderRequest
    {
        /// <summary>
        /// 平台订单流水号
        /// </summary>
        public string orderSerialId { get; set; }
        /// <summary>
        /// 合作方订单号
        /// </summary>
        public string partnerOrderId { get; set; }
    }
}
