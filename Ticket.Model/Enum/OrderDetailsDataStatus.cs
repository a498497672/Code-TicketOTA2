using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 订单明细状态
    /// </summary>
    public enum OrderDetailsDataStatus
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
        /// 已激活
        /// </summary>
        [Description("已激活")]
        Activate = 3,

        /// <summary>
        /// 已消费
        /// </summary>
        [Description("已消费")]
        Consume = 4,

        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refund = 5,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canncel = 6,

        /// <summary>
        /// 已过期（是一个逻辑状态，指超过有效期未使用的有效订单（支付成功&&有效期之外））
        /// </summary>
        [Description("已过期")]
        Expired = 7,

        /// <summary>
        /// 已取票
        /// </summary>
        [Description("已取票")]
        IsTaken = 9,
    }
}
