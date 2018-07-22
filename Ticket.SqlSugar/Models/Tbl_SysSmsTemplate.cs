using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_SysSmsTemplate")]
    public partial class Tbl_SysSmsTemplate
    {
           public Tbl_SysSmsTemplate(){


           }
           /// <summary>
           /// Desc:系统模板ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int SysSmsTemplateId {get;set;}

           /// <summary>
           /// Desc:模板类型，1：短信余额提醒
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TemplateType {get;set;}

           /// <summary>
           /// Desc:模板名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TemplateName {get;set;}

           /// <summary>
           /// Desc:模板内容
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TemplateContent {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? TriigerCondition {get;set;}

           /// <summary>
           /// Desc:数据状态(以二进制的方式存储,第1位:是否无效)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

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

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastUpdateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? LastUpdateUserId {get;set;}

    }
}
