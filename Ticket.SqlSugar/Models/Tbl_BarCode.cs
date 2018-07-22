using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_BarCode")]
    public partial class Tbl_BarCode
    {
           public Tbl_BarCode(){


           }
           /// <summary>
           /// Desc:条形码主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int BarCodeId {get;set;}

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
           /// Desc:条形码起始
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BarCodeStart {get;set;}

           /// <summary>
           /// Desc:条形码结束
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BarCodeEnd {get;set;}

           /// <summary>
           /// Desc:操作日期
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime OpDate {get;set;}

           /// <summary>
           /// Desc:门票ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? TicketId {get;set;}

           /// <summary>
           /// Desc:二进制，第一位：是否已激活
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
