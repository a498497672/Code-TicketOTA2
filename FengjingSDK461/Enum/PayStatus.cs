using System.ComponentModel;

namespace FengjingSDK461.Enum
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayStatus
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 1,

        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        Wechat = 2,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("现金")]
        ReadyMoney = 3,
    }
}
