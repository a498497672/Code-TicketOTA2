using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_NoticeOrderConsumed")]
    public partial class Tbl_NoticeOrderConsumed
    {
           public Tbl_NoticeOrderConsumed(){


           }
           /// <summary>
           /// Desc:订单核销表  --- id
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
           /// Desc:分销商的身份标识
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string IdentityKey {get;set;}

           /// <summary>
           /// Desc:ota订单详情id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OtaOrderDetailId {get;set;}

           /// <summary>
           /// Desc:订单详情编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public Guid OrderDetailNumber {get;set;}

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
           /// Desc:订单产品总数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Count {get;set;}

           /// <summary>
           /// Desc:是否取票
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool IsTaken {get;set;}

           /// <summary>
           /// Desc:取票时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? UseDate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:执行次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RunCount {get;set;}

    }
}
