using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_StockScrapDetail")]
    public partial class Tbl_StockScrapDetail
    {
           public Tbl_StockScrapDetail(){


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
           /// Desc:报废单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ScrapNo {get;set;}

           /// <summary>
           /// Desc:报废门票产品Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TickectId {get;set;}

           /// <summary>
           /// Desc:门票产品名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TickectName {get;set;}

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
           /// Desc:报废数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Quantity {get;set;}

           /// <summary>
           /// Desc:报废单价
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:报废总价值
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
           /// Desc:报废员Id（售票员）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SellerId {get;set;}

           /// <summary>
           /// Desc:报废原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

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
