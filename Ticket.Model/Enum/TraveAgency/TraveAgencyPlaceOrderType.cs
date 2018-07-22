using System.ComponentModel;

namespace Ticket.Model.Enum.TraveAgency
{
    /// <summary>
    /// 下单方式 1 旅行社下单 ; 2 景区代下单
    /// </summary>
    public enum TraveAgencyPlaceOrderType
    {
        /// <summary>
        /// 旅行社下单
        /// </summary>
        [Description("旅行社下单")]
        TraveAgency = 1,
        /// <summary>
        /// 景区代下单
        /// </summary>
        [Description("景区代下单")]
        ScenicSpot = 2,
    }
}
