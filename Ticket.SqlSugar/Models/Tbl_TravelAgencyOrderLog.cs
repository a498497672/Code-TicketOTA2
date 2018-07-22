using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TravelAgencyOrderLog")]
    public partial class Tbl_TravelAgencyOrderLog
    {
           public Tbl_TravelAgencyOrderLog(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:订单id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TravelAgercyOrderId {get;set;}

           /// <summary>
           /// Desc:订单id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string EnterpriseUserName {get;set;}

           /// <summary>
           /// Desc:用户Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseUserId {get;set;}

           /// <summary>
           /// Desc:操作日志类型(1:审核通过;2:审核驳回;3同意订单取消;4:支付订单;5打印订单)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

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

    }
}
