using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Order")]
    public partial class Tbl_Order
    {
           public Tbl_Order(){


           }
           /// <summary>
           /// Desc:订单Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:订单编号;规则：按年月日时分秒+景区ID+8位随机码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:票的来源  1：景区自己的;2：OTA
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketSource {get;set;}

           /// <summary>
           /// Desc:支付类型(1:支付宝;2:微信；3：现金)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int PayType {get;set;}

           /// <summary>
           /// Desc:支付账号(各支付类型对应的账号)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayAccount {get;set;}

           /// <summary>
           /// Desc:结算中心交易号(各支付类型对应的结算中心产生的交易号)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PayTradeNo {get;set;}

           /// <summary>
           /// Desc:售票员ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SellerId {get;set;}

           /// <summary>
           /// Desc:门票名称,多个门票用逗号分开
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:预订数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int BookCount {get;set;}

           /// <summary>
           /// Desc:付款总计
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal TotalAmount {get;set;}

           /// <summary>
           /// Desc:联系人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Linkman {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:订单状态(1:等待付款;2:已支付;3:已取消;4退款后台处理中，5：已退款)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderStatus {get;set;}

           /// <summary>
           /// Desc:下单时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:付款时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? PayTime {get;set;}

           /// <summary>
           /// Desc:有效期开始
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ValidityDateStart {get;set;}

           /// <summary>
           /// Desc:有效期结束
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ValidityDateEnd {get;set;}

           /// <summary>
           /// Desc:取消时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CancelTime {get;set;}

           /// <summary>
           /// Desc:已使用数量(当已使用数量与预定数量相等时,则表示此订单已消费完结)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UsedQuantity {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? Price {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? IDType {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IDCard {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? BuyUserId {get;set;}

           /// <summary>
           /// Desc:订单来源(1:PC;2:触屏;3:APP)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Source {get;set;}

           /// <summary>
           /// Desc:OTA订单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OTAOrderNo {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OTABusinessId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool CanRefund {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CanRefundTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool CanModify {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CanModifyTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReceiverName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int OrderType {get;set;}

           /// <summary>
           /// Desc:组团方式(1:个人组团;2:单位组团;)
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int GroupWay {get;set;}

           /// <summary>
           /// Desc:(分销商单位名称:有关联)或(个人单位名称:无关联);)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OTABusinessName {get;set;}

           /// <summary>
           /// Desc:用户 微信、支付宝 的 支付码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AuthCode {get;set;}

    }
}
