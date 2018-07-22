using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Order_Config")]
    public partial class Tbl_Order_Config
    {
           public Tbl_Order_Config(){


           }
           /// <summary>
           /// Desc:退款流水号最大值(需要生成退款批次流水号时,取此值+1,如为当天第一笔退款则清0)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RefundSerialMax {get;set;}

           /// <summary>
           /// Desc:订单号时间戳(用于记录当前订单号随机数生成时间,单位分钟,如果为新的时间则覆盖)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? OrderNoTimeStamp {get;set;}

           /// <summary>
           /// Desc:订单自动关闭时间(单位:天)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? OrderCloseDays {get;set;}

           /// <summary>
           /// Desc:是否自动下架推荐内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? IsOffshelvesRecommend {get;set;}

           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int ListId {get;set;}

           /// <summary>
           /// Desc:门票订单自动确认天数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? I_OrderConfirmDays {get;set;}

    }
}
