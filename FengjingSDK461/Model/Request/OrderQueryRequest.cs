using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Request
{
    public class OrderQueryRequest
    {
        public HeadRequest Head { get; set; }
        public OrderQuery Body { get; set; }
    }
    public class OrderQuery
    {
        /// <summary>
        /// 订单 ID
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string OtaOrderId { get; set; }
    }
}
