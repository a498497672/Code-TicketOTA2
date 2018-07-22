using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 订单来源
    /// </summary>
    public enum OrderSource
    {
        /// <summary>
        /// 自己平台
        /// </summary>
        [Description("自己平台")]
        My = 0,

        /// <summary>
        /// OTA平台
        /// </summary>
        [Description("OTA平台")]
        OTA = 1,

        /// <summary>
        /// 小径平台
        /// </summary>
        [Description("小径平台")]
        XiaoJing = 2
    }
}
