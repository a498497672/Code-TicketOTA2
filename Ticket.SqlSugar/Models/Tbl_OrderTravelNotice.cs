using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_OrderTravelNotice")]
    public partial class Tbl_OrderTravelNotice
    {
           public Tbl_OrderTravelNotice(){


           }
           /// <summary>
           /// Desc:订单出行通知， 用于定时任务 （携程）
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:批次流水号  格式为处理日期（yyyy-MM-dd）+32 位去分隔符的 Guid
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SequenceId {get;set;}

           /// <summary>
           /// Desc:景区id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:分销商的身份标识（如携程 、美团等）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string IdentityKey {get;set;}

           /// <summary>
           /// Desc:OTA订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OtaOrderId {get;set;}

           /// <summary>
           /// Desc:订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:执行次数 超过3次 就不执行
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RunCount {get;set;}

    }
}
