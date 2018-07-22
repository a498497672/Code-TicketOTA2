using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 二进制状态
    /// </summary>
    public enum DataStatus
    {
        /// <summary>
        /// 第一位
        /// </summary>
        [Description("第一位")]
        BitOne = 1,

        /// <summary>
        /// 第二位
        /// </summary>
        [Description("第二位")]
        BitTwo = 2,

        /// <summary>
        /// 第三位
        /// </summary>
        [Description("第三位")]
        BitThree = 4,

        /// <summary>
        /// 第四位
        /// </summary>
        [Description("第四位")]
        BitFour = 8,

        /// <summary>
        /// 第五位
        /// </summary>
        [Description("第五位")]
        BitFive = 16,

        /// <summary>
        /// 第六位
        /// </summary>
        [Description("第六位")]
        BitSix = 32
    }
}
