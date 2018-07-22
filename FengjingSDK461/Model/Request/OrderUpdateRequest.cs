namespace FengjingSDK461.Model.Request
{
    public class OrderUpdateRequest
    {
        public HeadRequest Head { get; set; }
        public OrderUpdateBody Body { get; set; }
    }

    public class OrderUpdateBody
    {
        public OrderUpdateInfo OrderInfo { get; set; }
    }

    public class OrderUpdateInfo
    {
        /// <summary>
        /// 订单 ID
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        ///  游玩日期	“yyyy-MM-dd”
        /// </summary>
        public string VisitDate { get; set; }

        public OrderUpdateContactPerson ContactPerson { get; set; }
    }

    public class OrderUpdateContactPerson
    {
        /// <summary>
        /// 购买人姓名
        /// </summary>
        public string BuyName { get; set; }
        /// <summary>
        /// 游玩人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 游玩人手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 取票人证件类型 身份证 : ID_CARD,    护照 : HUZHAO,    台胞证 : TAIBAO ,港澳通行证: GANGAO 其它：OTHER
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 取票人证件号
        /// </summary>
        public string CardNo { get; set; }
    }
}
