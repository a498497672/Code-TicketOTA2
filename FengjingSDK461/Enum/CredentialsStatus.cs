using System.ComponentModel;

namespace FengjingSDK461.Enum
{
    /// <summary>
    /// 证件类型
    /// </summary>
    public enum CredentialsStatus
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("OTHER")]
        Other = 0,

        /// <summary>
        /// 身份证
        /// </summary>
        [Description("ID_CARD")]
        IdCard = 1,

        /// <summary>
        /// 护照
        /// </summary>
        [Description("HUZHAO")]
        Passport = 2,

        /// <summary>
        /// 台胞证
        /// </summary>
        [Description("TAIBAO")]
        Taiwan = 3,

        /// <summary>
        /// 港澳通行证
        /// </summary>
        [Description("GANGAO")]
        HongKongMacauLaissezPasser = 4,

    }
}
