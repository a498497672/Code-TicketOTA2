using System.ComponentModel;

namespace FengjingSDK461.Enum
{
    /// <summary>
    /// OTA类型
    /// </summary>
    public enum OtaType
    {
        /// <summary>
        /// 携程
        /// </summary>
        [Description("携程")]
        Ctrip = 1,

        /// <summary>
        /// 同程
        /// </summary>
        [Description("同程")]
        TongCheng = 2,

        /// <summary>
        /// 美团
        /// </summary>
        [Description("美团")]
        MeiTuan = 3,
    }
}
