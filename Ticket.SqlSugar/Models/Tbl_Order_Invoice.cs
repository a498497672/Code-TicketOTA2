using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Order_Invoice")]
    public partial class Tbl_Order_Invoice
    {
           public Tbl_Order_Invoice(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:发票抬头
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:发票号码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string No {get;set;}

           /// <summary>
           /// Desc:发票代码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Code {get;set;}

           /// <summary>
           /// Desc:发票类型（1：增值税普通发票；2增值税专用发票；3：普通发票）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

           /// <summary>
           /// Desc:关联的订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:订单金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Amount {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateDate {get;set;}

    }
}
