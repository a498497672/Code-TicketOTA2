using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_StockScrap")]
    public partial class Tbl_StockScrap
    {
           public Tbl_StockScrap(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

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
           /// Desc:报废单号（规则：S2017000001）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ScrapNo {get;set;}

           /// <summary>
           /// Desc:报废总数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TotalQuantity {get;set;}

           /// <summary>
           /// Desc:报废总价值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal TotalAmount {get;set;}

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
           /// Desc:创建时间（默认报废日期为创建时间日期部分）
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

    }
}
