using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TicketWindow")]
    public partial class Tbl_TicketWindow
    {
           public Tbl_TicketWindow(){


           }
           /// <summary>
           /// Desc:售票窗口Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int TicketWindowId {get;set;}

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
           /// Desc:窗口名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string WindowName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Ip {get;set;}

           /// <summary>
           /// Desc:MAC地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MacAddress {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Summary {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderId {get;set;}

           /// <summary>
           /// Desc:数据状态(以二进制的方式存储,第1位:是否发送)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? DataStatus {get;set;}

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
