using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 检票来源
    /// </summary>
    public enum CheckTicketWayType
    {
        /// <summary>
        /// 云票务后台
        /// </summary>
        [Description("云票务后台")]
        TicketBackground = 1,

        /// <summary>
        /// 景区有园门闸机
        /// </summary>
        [Description("景区有园门闸机")]
        ScenicGate = 2,

        /// <summary>
        /// 景区无园门闸机
        /// </summary>
        [Description("景区无园门闸机")]
        NoScenicGate = 3,

        /// <summary>
        /// App
        /// </summary>
        [Description("App")]
        App = 4,
    }
}
