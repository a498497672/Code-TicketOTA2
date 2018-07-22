using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_RefundDetail")]
    public partial class Tbl_RefundDetail
    {
           public Tbl_RefundDetail(){


           }
           /// <summary>
           /// Desc:退款明细Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int RefundDetailId {get;set;}

           /// <summary>
           /// Desc:退款ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OrderId {get;set;}

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
           /// Desc:订单明细表ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OrderDetailId {get;set;}

           /// <summary>
           /// Desc:窗口ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? WindowId {get;set;}

           /// <summary>
           /// Desc:售票员ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SellerId {get;set;}

           /// <summary>
           /// Desc:1：可能景区的:2：可能是OTA
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SellerType {get;set;}

           /// <summary>
           /// Desc:1：景区自己的:2：OTA
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketSource {get;set;}

           /// <summary>
           /// Desc:1：印刷票，2：二维码打印票，3：二维码电子票
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketCategory {get;set;}

           /// <summary>
           /// Desc:已使用数量(当已使用数量与数量相等时,则表示此订单已消费完结)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UsedQuantity {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

           /// <summary>
           /// Desc:门票名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Quantity {get;set;}

           /// <summary>
           /// Desc:价格
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

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
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IDCard {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Linkman {get;set;}

           /// <summary>
           /// Desc:退款状态(0:申请中;1：退款成功，2：退款失败。3：拒接退款)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RefundStatus {get;set;}

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
           /// Desc:退款总金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal RefundTotalAmount {get;set;}

           /// <summary>
           /// Desc:退款说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RefundSummary {get;set;}

           /// <summary>
           /// Desc:下单时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime OrderTime {get;set;}

           /// <summary>
           /// Desc:验证时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CheckTime {get;set;}

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
           /// Desc:打印次数0：表示待打印
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PrintCount {get;set;}

           /// <summary>
           /// Desc:二维码URL，先不存
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QrcodeUrl {get;set;}

           /// <summary>
           /// Desc:二维码串
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Qrcode {get;set;}

           /// <summary>
           /// Desc:创建人Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

    }
}
