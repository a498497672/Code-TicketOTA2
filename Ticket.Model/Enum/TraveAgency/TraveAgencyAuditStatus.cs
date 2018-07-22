using System.ComponentModel;

namespace Ticket.Model.Enum.TraveAgency
{
    /// <summary>
    /// 审核状态(1:待审核;2:已审核;3:审核驳回;4:取消申请中;5:订单取消)
    /// </summary>
    public enum TraveAgencyAuditStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        WaitAudit = 1,
        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Audited = 2,
        /// <summary>
        /// 审核驳回
        /// </summary>
        [Description("审核驳回")]
        Reject = 3,
        /// <summary>
        /// 取消申请中
        /// </summary>
        [Description("取消申请中")]
        OrderCancelApplication = 4,
        /// <summary>
        /// 订单取消
        /// </summary>
        [Description("订单取消")]
        OrderCancel = 5,
    }
}
