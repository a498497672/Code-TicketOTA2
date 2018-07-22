using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Response
{
    /// <summary>
    /// 取消
    /// </summary>
    public class CancelOrderResponse
    {
        /// <summary>
        /// 0.订单未找到 1.订单取消成功 2.订单取消审核中 3.订单取消失败
        /// </summary>
        public int refundStatus { get; set; }
        /// <summary>
        /// 取消失败原因说明
        /// </summary>
        public string remark { get; set; }
    }
}
