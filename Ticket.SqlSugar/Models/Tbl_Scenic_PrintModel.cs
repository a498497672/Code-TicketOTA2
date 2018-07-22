using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Scenic_PrintModel")]
    public partial class Tbl_Scenic_PrintModel
    {
           public Tbl_Scenic_PrintModel(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ListId {get;set;}

           /// <summary>
           /// Desc:景区id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:模板地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string ModelPath {get;set;}

           /// <summary>
           /// Desc:打印宽度
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? ModelWidth {get;set;}

           /// <summary>
           /// Desc:打印高度
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? ModelHeight {get;set;}

           /// <summary>
           /// Desc:打印方式：1：竖打，2：横打
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public int? PrintWay {get;set;}

           /// <summary>
           /// Desc:最后修改时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime UpdateTime {get;set;}

           /// <summary>
           /// Desc:修改者
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string UpdateBy {get;set;}

           /// <summary>
           /// Desc:二进制状态：第一位：是否启用
           /// Default:1
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

    }
}
