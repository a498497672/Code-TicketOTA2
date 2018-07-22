using Newtonsoft.Json;

namespace Ticket.TaskEngine.Application.Model
{
    /// <summary>
    /// 订单信息实体(OTA订单)
    /// </summary>
    public class OtaOrderModel
    {
        //order_id int M   订单id
        [JsonProperty(PropertyName = "order_id")]
        public int OrderId { get; set; }
        //ProductCode string M   门票编号
        [JsonProperty(PropertyName = "ProductCode")]
        public string ProductCode { get; set; }
        //ProductName string M   门票名称
        [JsonProperty(PropertyName = "ProductName")]
        public string ProductName { get; set; }
        //ProductCount    int M   门票数量
        [JsonProperty(PropertyName = "ProductCount")]
        public int ProductCount { get; set; }
        //RefundCount int M   退票数量
        [JsonProperty(PropertyName = "RefundCount")]
        public string RefundCount { get; set; }
        //ProductPrice    Double M   门票售卖价格
        [JsonProperty(PropertyName = "ProductPrice")]
        public double ProductPrice { get; set; }
        //ProParPrice Double O   门票票面/结算价格
        [JsonProperty(PropertyName = "ProParPrice")]
        public double ProParPrice { get; set; }
        //CodeStr string M   门票凭证码
        [JsonProperty(PropertyName = "CodeStr")]
        public string Code { get; set; }
        //Amount  Double M   总金额
        [JsonProperty(PropertyName = "Amount")]
        public double Amount { get; set; }
        //DictionaryName  string M   门票类型
        [JsonProperty(PropertyName = "DictionaryName")]
        public string DictionaryName { get; set; }
        //CreateTime  string M   下单时间(格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "CreateTime")]
        public string CreateTime { get; set; }
        //ProductSDate string M   游玩开始日期(日期格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "ProductSDate")]
        public string StartDate { get; set; }
        //ProductEDate string M   游玩结束日期(日期格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "ProductEDate")]
        public string EndDate { get; set; }
        //OrderNo string M   订单号
        [JsonProperty(PropertyName = "OrderNo")]
        public string OrderNo { get; set; }
        //ThroughNumber   int M   已入园人数
        [JsonProperty(PropertyName = "ThroughNumber")]
        public int ThroughNumber { get; set; }
        //NotriggerNumber string M   未入园人数
        [JsonProperty(PropertyName = "NotriggerNumber")]
        public string NotriggerNumber { get; set; }
        //LinkName    string M   下单人姓名
        [JsonProperty(PropertyName = "LinkName")]
        public string LinkName { get; set; }
        //LinkPhone   string M   下单人手机号码
        [JsonProperty(PropertyName = "LinkPhone")]
        public string LinkPhone { get; set; }
        //LinkICNO    string M   身份证
        [JsonProperty(PropertyName = "LinkICNO")]
        public string IdCard { get; set; }
        //ChannelCode string M   渠道编码(OTA编码)
        [JsonProperty(PropertyName = "ChannelCode")]
        public string ChannelCode { get; set; }
        //ChannelName string M   渠道名
        [JsonProperty(PropertyName = "ChannelName")]
        public string ChannelName { get; set; }
        //merchantCode    string M   景区编码
        [JsonProperty(PropertyName = "merchantCode")]
        public string MerchantCode { get; set; }
        //merchantName    string M   景区名
        [JsonProperty(PropertyName = "merchantName")]
        public string MerchantName { get; set; }
        //OrderState  string M   订单状态
        [JsonProperty(PropertyName = "OrderState")]
        public string OrderState { get; set; }
        //product_offlcode    string M   线下产品编码
        [JsonProperty(PropertyName = "product_offlcode")]
        public string OfflCode { get; set; }

    }
}
