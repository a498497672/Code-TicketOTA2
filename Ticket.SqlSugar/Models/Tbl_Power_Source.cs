using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Power_Source")]
    public partial class Tbl_Power_Source
    {
           public Tbl_Power_Source(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:资源名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:所属站点:1:云票务风景网内部系统;2:云票务售票系统,3:云票务景区管理系统
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Site {get;set;}

           /// <summary>
           /// Desc:功能类别:1:模块;2:页面;3:按钮
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Type {get;set;}

           /// <summary>
           /// Desc:父级Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ParentId {get;set;}

           /// <summary>
           /// Desc:Http协议请求方式 (get;post;put;delete)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RequestMode {get;set;}

           /// <summary>
           /// Desc:控制器名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ControllerName {get;set;}

           /// <summary>
           /// Desc:action名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ActionName {get;set;}

           /// <summary>
           /// Desc:图标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Icon {get;set;}

           /// <summary>
           /// Desc:样式名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ClassName {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Sort {get;set;}

           /// <summary>
           /// Desc:0启用;1:禁用
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateDate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UpdateUserId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? UpdateDate {get;set;}

    }
}
