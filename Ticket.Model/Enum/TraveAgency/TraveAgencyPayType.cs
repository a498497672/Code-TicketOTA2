using System.ComponentModel;

namespace Ticket.Model.Enum.TraveAgency
{
    /// <summary>
    /// 支付类型(1:线上支付;2:线下支付)
    /// </summary>
    public enum TraveAgencyPayType
    {
        /// <summary>
        /// 线上支付
        /// </summary>
        [Description("线上支付")]
        OnLine = 1,
        /// <summary>
        /// 线下支付
        /// </summary>
        [Description("线下支付")]
        UnderLine = 2,
    }
}
