using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Response
{
    public class OrderUpdateResponse
    {
        public HeadResponse Head { get; set; }
        public OrderUpdateResponseBody Body { get; set; }
    }

    public class OrderUpdateResponseBody
    {
        /// <summary>
        /// 订单 ID
        /// </summary>
        public string OrderId { get; set; }
    }
}
