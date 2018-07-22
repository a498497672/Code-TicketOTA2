using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TravelAgencyOrder")]
    public partial class Tbl_TravelAgencyOrder
    {
           public Tbl_TravelAgencyOrder(){


           }
           /// <summary>
           /// Desc:旅行社下单Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:渠道商(旅行社)Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OTABusinessId {get;set;}

           /// <summary>
           /// Desc:渠道商(旅行社)名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OTABusinessName {get;set;}

           /// <summary>
           /// Desc:订单编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:支付类型(1:线上支付;2:线下支付)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int PayType {get;set;}

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
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IdCard {get;set;}

           /// <summary>
           /// Desc:审核状态(1:待审核;2:已审核;3:审核驳回;4取消申请中;5:订单取消;6订单过期)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int AuditStatus {get;set;}

           /// <summary>
           /// Desc:订单状态(1:未付款;2:已付款;3:已退款;4:已取纸质票;5线下已检)
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
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:下单方式 1 旅行社下单 ; 2 景区代下单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int PlaceOrderType {get;set;}

           /// <summary>
           /// Desc:驳回时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? RejectTime {get;set;}

           /// <summary>
           /// Desc:驳回原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RejectReason {get;set;}

           /// <summary>
           /// Desc:下单人，起到记录的作用
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PlaceOrderName {get;set;}

    }
}
