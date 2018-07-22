using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ticket.TaskEngine.Application.Model
{
    /// <summary>
    /// 订单信息实体(旅行社订单)
    /// </summary>
    public class OrderModel
    {
        //torder_id int M   订单ID
        [JsonProperty(PropertyName = "torder_id")]
        public int OrderId { get; set; }
        //torder_no   string M   订单号
        [JsonProperty(PropertyName = "torder_no")]
        public string OrderNo { get; set; }
        //torder_cdate    string M   下单时间(格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "torder_cdate")]
        public string CreateTime { get; set; }
        //torder_pdate string M   游玩时间(格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "torder_pdate")]
        public string PlayDate { get; set; }
        //torder_paytype string M   支付类型：D定存，F到付
        [JsonProperty(PropertyName = "torder_paytype")]
        public string PayType { get; set; }
        //torder_talmoney string M   门票单价（结算价）
        [JsonProperty(PropertyName = "torder_talmoney")]
        public string Money { get; set; }
        //torder_linkname string M   下单人姓名
        [JsonProperty(PropertyName = "torder_linkname")]
        public string LinkName { get; set; }
        //torder_linkphone    string M   下单人手机号码
        [JsonProperty(PropertyName = "torder_linkphone")]
        public string LinkPhone { get; set; }
        //torder_linkicno string M   下单人身份证
        [JsonProperty(PropertyName = "torder_linkicno")]
        public string LinkIdCard { get; set; }
        //torder_channelcode  string M   分销商编码
        [JsonProperty(PropertyName = "torder_channelcode")]
        public string ChannelCode { get; set; }
        //torder_channelname  string M   分销商名称
        [JsonProperty(PropertyName = "torder_channelname")]
        public string ChannelName { get; set; }
        //torder_merchantcode string M   景区编码
        [JsonProperty(PropertyName = "torder_merchantcode")]
        public string MerchantCode { get; set; }
        //torder_merchantname string M   景区名称
        [JsonProperty(PropertyName = "torder_merchantname")]
        public string MerchantName { get; set; }
        //AuditState  int M   订单审核状态：0待审核，1已审核，2审核驳回，3订单取消,4取消申请中
        [JsonProperty(PropertyName = "AuditState")]
        public int AuditState { get; set; }
        //torderItems List<T_Biz_torderitems> M   订单明细集合
        [JsonProperty(PropertyName = "torderItems")]
        public List<OrderItem> OrderItems { get; set; }
    }

    /// <summary>
    /// 旅行社订单下发明细实体(T_Biz_torderitems)
    /// </summary>
    public class OrderItem
    {
        //titems_id int M   订单明细id
        [JsonProperty(PropertyName = "titems_id")]
        public int ItemId { get; set; }
        //titems_torderid int M   订单ID
        [JsonProperty(PropertyName = "titems_torderid")]
        public int OrderId { get; set; }
        //titems_torderno string M   订单号
        [JsonProperty(PropertyName = "titems_torderno")]
        public string OrderNo { get; set; }
        //titems_productcode  string M   产品编码
        [JsonProperty(PropertyName = "titems_productcode")]
        public string ProductCode { get; set; }
        //titems_proname  string M   产品名称
        [JsonProperty(PropertyName = "titems_proname")]
        public string ProductName { get; set; }
        //titems_productprice string M   产品售卖价格
        [JsonProperty(PropertyName = "titems_productprice")]
        public string Price { get; set; }
        //titems_productproparprice   string O   产品票面价格
        [JsonProperty(PropertyName = "titems_productproparprice")]
        public string ProparPrice { get; set; }
        //titems_number   int M   票数
        [JsonProperty(PropertyName = "titems_number")]
        public int Number { get; set; }
        //titems_refnumber    int M   退票数
        [JsonProperty(PropertyName = "titems_refnumber")]
        public int RefNumber { get; set; }
        //titems_codestr  string M   订单串码
        [JsonProperty(PropertyName = "titems_codestr")]
        public string Code { get; set; }
        //titems_throughnumber    string M   已入园人数
        [JsonProperty(PropertyName = "titems_throughnumber")]
        public string ThroughNumber { get; set; }
        //titems_notriggernumber  int M   未入园人数
        [JsonProperty(PropertyName = "titems_notriggernumber")]
        public int NoTriggerNumber { get; set; }
        //product_offlcode    string O   线下产品编码
        [JsonProperty(PropertyName = "product_offlcode")]
        public string OfflCode { get; set; }
        //titems_productsdate string M   游玩开始日期(日期格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "titems_productsdate")]
        public string StartDate { get; set; }
        //titems_productedate string M   游玩结束日期(日期格式：yyyyMMddHHmmss)
        [JsonProperty(PropertyName = "titems_productedate")]
        public string EndDate { get; set; }
        //titems_orderstate string M   明细订单状态：F-已释放订单，B-作废
        //R-取消，N-未付款，S-已付款，G-已改签，H-已取纸质票，O-线下已捡，M-已退款，E-退款审核中，P-部分退票  ，A-全部退票
        [JsonProperty(PropertyName = "titems_orderstate")]
        public string State { get; set; }
    }
}
