using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 上架渠道 1 散客票购票 2 团队票购票 3 手机扫码购票'
    /// </summary>
    public enum ShelvesChannelEnum
    {
        /// <summary>
        /// 散客票购票
        /// </summary>
        [Description("散客票购票")]
        IndividualTicket = 1,

        /// <summary>
        /// 团队票购票
        /// </summary>
        [Description("团队票购票")]
        TeamTicket = 2,

        /// <summary>
        /// 手机扫码购票
        /// </summary>
        [Description("手机扫码购票")]
        MobileTicket = 3
    }
}
