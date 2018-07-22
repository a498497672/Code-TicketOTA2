using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Refund_tmp")]
    public partial class Tbl_Refund_tmp
    {
           public Tbl_Refund_tmp(){


           }
           /// <summary>
           /// Desc:退款Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int RefundId {get;set;}

           /// <summary>
           /// Desc:退款产生的NO,唯一
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RefundOrderNO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:
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
           /// Desc:1：印刷票，2：二维码打印票，3：二维码电子票
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketType {get;set;}

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
           /// Nullable:True
           /// </summary>           
           public int? SellerId {get;set;}

           /// <summary>
           /// Desc:预订总数量
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
           /// Nullable:False
           /// </summary>           
           public string Linkman {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:条形码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BarCode {get;set;}

           /// <summary>
           /// Desc:存根（不记录）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Stub {get;set;}

           /// <summary>
           /// Desc:凭证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CertificateNO {get;set;}

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
           /// Desc:退款数量与预订总数量相等，说明对应的子订单全数退款
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundQuantity {get;set;}

           /// <summary>
           /// Desc:退款手续费
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal RefundFee {get;set;}

           /// <summary>
           /// Desc:退款说明
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RefundSummary {get;set;}

           /// <summary>
           /// Desc:退款状态(0:申请中;1：退款成功，2：退款失败。3：拒接退款)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RefundStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:价格
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:证件类型(1:身份证)
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
           /// Desc:创建人Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

    }
}
