using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Stock")]
    public partial class Tbl_Stock
    {
           public Tbl_Stock(){


           }
           /// <summary>
           /// Desc:库存主键Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int StockId {get;set;}

           /// <summary>
           /// Desc:企业Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:景区Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:条形码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BarCode {get;set;}

           /// <summary>
           /// Desc:关联门票类型Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

           /// <summary>
           /// Desc:门票类型名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:票类型（1：年票；2：其他票）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketType {get;set;}

           /// <summary>
           /// Desc:仓库类型（1：印刷库；2：电子票库）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int StockType {get;set;}

           /// <summary>
           /// Desc:单价
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:数量
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int Quantity {get;set;}

           /// <summary>
           /// Desc:数据状态(1:在库;2:在售;3:已销售;4:已报废)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

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

    }
}
