using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Enum
{
    public enum TicketOrderStatus
    {
        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Success = 1,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canncel = 2,
        /// <summary>
        /// 已消费
        /// </summary>
        [Description("已消费")]
        Consume = 3,
        /// <summary>
        /// 已过期（是一个逻辑状态，指超过有效期未使用的有效订单（支付成功&&有效期之外））
        /// </summary>
        [Description("已过期")]
        Expired = 4,
    }
}
