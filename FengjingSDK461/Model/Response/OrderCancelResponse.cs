using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Response
{
    /// <summary>
    /// 取消订单应答
    /// </summary>
    public class OrderCancelResponse
    {
        public HeadResponse Head { get; set; }
        public OrderCancelInfo Body { get; set; }
    }

    public class OrderCancelInfo
    {
        public string Message { get; set; }
    }
}
