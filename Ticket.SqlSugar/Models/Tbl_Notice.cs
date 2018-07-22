using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Notice")]
    public partial class Tbl_Notice
    {
           public Tbl_Notice(){


           }
           /// <summary>
           /// Desc:系统通知ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
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
           /// Desc:通知内容
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NoticeContent {get;set;}

           /// <summary>
           /// Desc:通知标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:通知开始日期
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime StartDate {get;set;}

           /// <summary>
           /// Desc:通知结束日期.NULL表示长期有效
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? EndDate {get;set;}

           /// <summary>
           /// Desc:排序号(降序)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:数据状态(以二进制的方式存储,第1位:是否停用)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

    }
}
