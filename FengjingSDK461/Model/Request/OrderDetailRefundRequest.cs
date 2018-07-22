using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 订单详情退款请求
    /// </summary>
    public class OrderDetailRefundRequest : RequestBase
    {
        public int OrderDetailId { get; set; }
    }
}
