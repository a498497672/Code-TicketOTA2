using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_StockRefundDetail")]
    public partial class Tbl_StockRefundDetail
    {
           public Tbl_StockRefundDetail(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int DetailId {get;set;}

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
           /// Desc:退票订单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RefundNo {get;set;}

           /// <summary>
           /// Desc:门票产品Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

           /// <summary>
           /// Desc:门票产品名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:门票类型（1：年票；2：其他票）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketType {get;set;}

           /// <summary>
           /// Desc:条形码起始码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BarCodeStart {get;set;}

           /// <summary>
           /// Desc:条形码结束码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BarCodeEnd {get;set;}

           /// <summary>
           /// Desc:退票数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Quantity {get;set;}

           /// <summary>
           /// Desc:退票总价值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal TotalAmount {get;set;}

           /// <summary>
           /// Desc:退回仓库类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? StockType {get;set;}

           /// <summary>
           /// Desc:退票员Id（售票员）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SellerId {get;set;}

           /// <summary>
           /// Desc:创建人Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CreateUserName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? LastUpdateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastUpdateTime {get;set;}

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
           public string SellerName {get;set;}

    }
}
