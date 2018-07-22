using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Power_Role")]
    public partial class Tbl_Power_Role
    {
           public Tbl_Power_Role(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:企业Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:景区Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:所属站点:1:云票务风景网内部系统;2:云票务售票系统,3:云票务景区管理系统
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Site {get;set;}

           /// <summary>
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RoleName {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Description {get;set;}

           /// <summary>
           /// Desc:状态0禁用;1：启用
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
