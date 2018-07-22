using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Power_RoleSource")]
    public partial class Tbl_Power_RoleSource
    {
           public Tbl_Power_RoleSource(){


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
           /// Desc:角色Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RoleId {get;set;}

           /// <summary>
           /// Desc:资源Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SourceId {get;set;}

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

    }
}
