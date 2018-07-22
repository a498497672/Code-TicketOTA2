using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_SaleLog")]
    public partial class Tbl_SaleLog
    {
           public Tbl_SaleLog(){


           }
           /// <summary>
           /// Desc:销售日志Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int SaleLogId {get;set;}

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
           /// Desc:操作内容
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LogContent {get;set;}

           /// <summary>
           /// Desc:订单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:票名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:售票数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Quantity {get;set;}

           /// <summary>
           /// Desc:售票金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TotalAmount {get;set;}

           /// <summary>
           /// Desc:退票数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundQuantity {get;set;}

           /// <summary>
           /// Desc:退票手续费
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? RefundFee {get;set;}

           /// <summary>
           /// Desc:退票金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? RefundAmount {get;set;}

           /// <summary>
           /// Desc:激活码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ActivationCode {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:数据状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:景区销售员ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

    }
}
