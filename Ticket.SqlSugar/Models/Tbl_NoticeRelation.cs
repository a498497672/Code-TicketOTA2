using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_NoticeRelation")]
    public partial class Tbl_NoticeRelation
    {
           public Tbl_NoticeRelation(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int NoticeRelationId {get;set;}

           /// <summary>
           /// Desc:系统通知ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int NoticeId {get;set;}

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
           /// Desc:通知用户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UserId {get;set;}

           /// <summary>
           /// Desc:数据状态(以二进制的方式存储,第1位:是否已读)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

    }
}
