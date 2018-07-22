using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_EnterpriseUserOpLog")]
    public partial class Tbl_EnterpriseUserOpLog
    {
           public Tbl_EnterpriseUserOpLog(){


           }
           /// <summary>
           /// Desc:日志ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int EnterpriseUserOpLogId {get;set;}

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
           /// Desc:操作员工Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UserId {get;set;}

           /// <summary>
           /// Desc:操作内容
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LogContent {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:操作者IP
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ClientIp {get;set;}

           /// <summary>
           /// Desc:日志模块
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ModuleType {get;set;}

    }
}
