using System.ComponentModel;

namespace Ticket.Model.Enum
{
    public enum TicketFirstType
    {
        /// <summary>
        /// 散客票
        /// </summary>
        [Description("散客票")]
        散客票 = 1,
        /// <summary>
        /// 团体票
        /// </summary>
        [Description("团体票")]
        团体票 = 2,
        /// <summary>
        /// 年票
        /// </summary>
        [Description("年票")]
        年票 = 3,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        其他 = 4,
    }
}
