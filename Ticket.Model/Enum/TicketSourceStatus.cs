using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 票的来源  1：景区自己的; 2：OTA
    /// </summary>
    public enum TicketSourceStatus
    {
        /// <summary>
        /// 景区自己的
        /// </summary>
        [Description("景区自己的")]
        ScenicSpot = 1,

        /// <summary>
        /// OTA
        /// </summary>
        [Description("OTA")]
        Ota = 2,
    }
}
