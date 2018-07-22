using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_EnterpriseUser")]
    public partial class Tbl_EnterpriseUser
    {
           public Tbl_EnterpriseUser(){


           }
           /// <summary>
           /// Desc:企业员工Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int EnterpriseUserId {get;set;}

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
           /// Desc:用户名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// Desc:用户密码(MD5加密)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PassWord {get;set;}

           /// <summary>
           /// Desc:真实姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RealName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:最近登录时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LastLoginTime {get;set;}

           /// <summary>
           /// Desc:员工类型(1：售票员，2：检票员3：景区管理员)
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UserType {get;set;}

           /// <summary>
           /// Desc:角色ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RoleId {get;set;}

           /// <summary>
           /// Desc:卖票类型 1：
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SaleTicketType {get;set;}

           /// <summary>
           /// Desc:密码安全级别1-6(越大越安全)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PassWordLevel {get;set;}

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

           /// <summary>
           /// Desc:是否是超级管理员（是:1 ;否:0;）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool IsSupperAdmin {get;set;}

    }
}
