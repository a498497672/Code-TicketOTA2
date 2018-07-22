using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 传感器的来源方式
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// 位置来源方式
        /// </summary>
        [Description("位置来源方式")]
        UnFound = 0,

        /// <summary>
        /// 条形码
        /// </summary>
        [Description("条形码")]
        BarCode = 1,

        /// <summary>
        /// 二维码
        /// </summary>
        [Description("二维码")]
        QRcode = 2,

        /// <summary>
        /// 年卡
        /// </summary>
        [Description("年卡")]
        YearTicket = 3,

        /// <summary>
        /// 指纹
        /// </summary>
        [Description("指纹")]
        Finger = 4,

        /// <summary>
        /// 掌静脉
        /// </summary>
        [Description("掌静脉")]
        PalmVein = 5,

        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        IdCard = 6,
    }
}
