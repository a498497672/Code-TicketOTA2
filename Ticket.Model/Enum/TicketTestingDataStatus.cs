using System.ComponentModel;

namespace Ticket.Model.Enum
{
    public enum TicketTestingDataStatus
    {
        /// <summary>
        /// 已激活
        /// </summary>
        [Description("已激活")]
        Activate = 1,
        /// <summary>
        /// 部分使用
        /// </summary>
        [Description("部分使用")]
        PartEmploy =2,
        /// <summary>
        /// 已使用
        /// </summary>
        [Description("已使用")]
        Employ = 3,
    }
}
