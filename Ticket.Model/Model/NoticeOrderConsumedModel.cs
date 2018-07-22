using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model
{

    public class request
    {
        public Header header { get; set; }
        public NoticeOrderConsumedModel body { get; set; }
    }

    public class Header
    {
        public string accountId { get; set; }
        public string serviceName { get; set; }
        public string requestTime { get; set; }
        public string version { get; set; }
        public string sign { get; set; }
    }



    /// <summary>
    /// 消费通知接口
    /// </summary>
    public class NoticeOrderConsumedModel
    {
        /// <summary>
        /// OTA订单号
        /// </summary>
        public string otaOrderId { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string vendorOrderId { get; set; }
        /// <summary>
        /// 实际使用日期”yyyy-MM-dd”
        /// </summary>
        public string useDate { get; set; }
        /// <summary>
        /// 订单产品总数量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public int useCount { get; set; }
        /// <summary>
        /// 取消数量
        /// </summary>
        public int cancelCount { get; set; }
    }
}
