using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TicketConsume")]
    public partial class Tbl_TicketConsume
    {
           public Tbl_TicketConsume(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNo {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public Guid OrderDetailNumber {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketTestingId {get;set;}

           /// <summary>
           /// Desc:Ota的订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OtaOrderNo {get;set;}

           /// <summary>
           /// Desc:票的种类 1：印刷票，2：二维码打印票，3：二维码电子票
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketCategory {get;set;}

           /// <summary>
           /// Desc:条形码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BarCode {get;set;}

           /// <summary>
           /// Desc:二维码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QRcode {get;set;}

           /// <summary>
           /// Desc:订单来源   0:自己 1：OTA 2：小径平台
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderSource {get;set;}

           /// <summary>
           /// Desc:发送状态： 0 ：没发送 1：已发送
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool SendStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:发送次数，发送次数最多5次
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int SendCount {get;set;}

           /// <summary>
           /// Desc:产品id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

    }
}
