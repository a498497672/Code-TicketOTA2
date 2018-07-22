using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TicketRelation")]
    public partial class Tbl_TicketRelation
    {
           public Tbl_TicketRelation(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int TicketRelationId {get;set;}

           /// <summary>
           /// Desc:门票ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

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
           /// Desc:售卖有效期开始
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ExpiryDateStart {get;set;}

           /// <summary>
           /// Desc:售卖有效期结束
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime ExpiryDateEnd {get;set;}

           /// <summary>
           /// Desc:市场价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MarkPrice {get;set;}

           /// <summary>
           /// Desc:销售价格
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal SalePrice {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OrderId {get;set;}

           /// <summary>
           /// Desc:价格策略类型  1 特殊时间段（T） 2 周末（W）
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

    }
}
