using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Request
{
    public class ConsumeNoticeRequest
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
        /// 消费票数
        /// </summary>
        public int tickets { get; set; }
        /// <summary>
        /// 消费日期(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string consumeDate { get; set; }
    }
}
