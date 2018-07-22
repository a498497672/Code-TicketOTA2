using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayStatus
    {
        /// <summary>
        /// 无支付方式
        /// </summary>
        [Description("无支付方式")]
        NoPayStatus = 0,
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
        /// 现金
        /// </summary>
        [Description("现金")]
        ReadyMoney = 3,
        /// <summary>
        /// 分销
        /// </summary>
        [Description("分销")]
        Distribution =4
    }
}
