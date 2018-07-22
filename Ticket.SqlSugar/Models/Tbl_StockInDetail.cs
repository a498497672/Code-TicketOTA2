using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_StockInDetail")]
    public partial class Tbl_StockInDetail
    {
           public Tbl_StockInDetail(){


           }
           /// <summary>
           /// Desc:入库详情Id
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
           /// Desc:入库单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string InNo {get;set;}

           /// <summary>
           /// Desc:入库门票类型Id
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
           /// Desc:入库单价
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:入库门票数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Quantity {get;set;}

           /// <summary>
           /// Desc:总价格
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal TotalAmount {get;set;}

           /// <summary>
           /// Desc:仓库类型（1：印刷库；2：电子票库）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int StockType {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:创建人
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
