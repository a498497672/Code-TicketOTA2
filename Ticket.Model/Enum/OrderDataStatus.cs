using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderDataStatus
    {
        /// <summary>
        /// 等待付款
        /// </summary>
        [Description("等待付款")]
        NoPay = 1,

        /// <summary>
        /// 已支付
        /// </summary>
        [Description("已支付")]
        Success = 2,


        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canncel = 3
    }
}
