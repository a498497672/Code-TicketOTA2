using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TravelAgencyOrderDetail")]
    public partial class Tbl_TravelAgencyOrderDetail
    {
           public Tbl_TravelAgencyOrderDetail(){


           }
           /// <summary>
           /// Desc:旅行社下单详情Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

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
           public int TicketId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Quantity {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ValidityDateStart {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ValidityDateEnd {get;set;}

           /// <summary>
           /// Desc:订单状态(1:未付款;2:已付款;3:已退款;4:已取纸质票;5线下已检)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderStatus {get;set;}

    }
}
