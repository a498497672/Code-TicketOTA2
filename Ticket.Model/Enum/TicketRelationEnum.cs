using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 价格策略类型
    /// </summary>
    public enum TicketRelationEnum
    {
        /// <summary>
        /// 特殊时间段（T）
        /// </summary>
        [Description("特殊时间段（T）")]
        TimeSlot = 1,

        /// <summary>
        /// 周末（W）
        /// </summary>
        [Description("周末（W）")]
        Weekend = 2,
    }
}
