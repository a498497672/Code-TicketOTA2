using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TicketRule")]
    public partial class Tbl_TicketRule
    {
           public Tbl_TicketRule(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:规则名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RuleName {get;set;}

           /// <summary>
           /// Desc:是否可以提前预定(1:是0:否)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool CanBookAdvance {get;set;}

           /// <summary>
           /// Desc:可提前预定天数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? BookDay {get;set;}

           /// <summary>
           /// Desc:可提前预定小时数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? BookHour {get;set;}

           /// <summary>
           /// Desc:可提前预定分钟数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? BookMinute {get;set;}

           /// <summary>
           /// Desc:是否支持退款
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool CanRefund {get;set;}

           /// <summary>
           /// Desc:可退款天数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundDay {get;set;}

           /// <summary>
           /// Desc:可退款小时数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundHour {get;set;}

           /// <summary>
           /// Desc:可退款分钟数（和天数小时数一起使用）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundMinute {get;set;}

           /// <summary>
           /// Desc:是否支持修改订单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool CanModify {get;set;}

           /// <summary>
           /// Desc:可修改订单天数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModifyDay {get;set;}

           /// <summary>
           /// Desc:可修改订单小时数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModifyHour {get;set;}

           /// <summary>
           /// Desc:可修改订单分钟数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ModifyMinute {get;set;}

           /// <summary>
           /// Desc:订购说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TicketNotice {get;set;}

           /// <summary>
           /// Desc:入园说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string EnterNotice {get;set;}

           /// <summary>
           /// Desc:退订说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RefundNotice {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int DelayCheck {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:是否随时退
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool IsAnytimeRefund {get;set;}

    }
}
