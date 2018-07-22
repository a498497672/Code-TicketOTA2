using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.TaskEngine.Application.Enum
{
    /// <summary>
    /// 订单审核状态
    /// </summary>
    public enum OrderAuditState
    {
        /// <summary>
        /// 待审核
        /// </summary>
        PendingAudit = 0,
        /// <summary>
        /// 已审核
        /// </summary>
        Audited = 1,
        /// <summary>
        /// 审核驳回
        /// </summary>
        AuditReject = 2,
        /// <summary>
        /// 订单取消
        /// </summary>
        OrderCancellation = 3,
        /// <summary>
        /// 取消申请中
        /// </summary>
        CancellationOfApplication = 4
    }
}
