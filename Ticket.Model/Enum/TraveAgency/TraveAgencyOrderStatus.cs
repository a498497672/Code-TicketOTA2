using System.ComponentModel;

namespace Ticket.Model.Enum.TraveAgency
{
    /// <summary>
    /// 订单状态(1:未付款;2:已付款;3:已退款;4:已取票;5:线下已检)
    /// </summary>
    public enum TraveAgencyOrderStatus
    {
        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        NoPay = 1,
        /// <summary>
        /// 已付款
        /// </summary>
        [Description("已付款")]
        Success = 2,
        /// <summary>
        /// 已退款
        /// </summary>
        [Description("已退款")]
        Refund = 3,
        /// <summary>
        /// 已取纸质票
        /// </summary>
        [Description("已取纸质票")]
        IsTaken = 4,
        /// <summary>
        /// 已消费
        /// </summary>
        [Description("线下已检")]
        Consume = 5,
    }
}
