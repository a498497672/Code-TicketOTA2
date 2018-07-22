using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_Ticket_Testing")]
    public partial class Tbl_Ticket_Testing
    {
           public Tbl_Ticket_Testing(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int TicketTestingId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int OrderDetailId {get;set;}

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
           public int EnterpriseId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ScenicId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketCategory {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TicketName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BarCode {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CertificateNO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QRcode {get;set;}

           /// <summary>
           /// Desc:第一位：是否激活 第二位：是否使用
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DataStatus {get;set;}

           /// <summary>
           /// Desc:闸机机身号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DoorGateNO {get;set;}

           /// <summary>
           /// Desc:刷卡类型，1：二位码，2：身份证，3：条形码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CardType {get;set;}

           /// <summary>
           /// Desc:身份证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IDCard {get;set;}

           /// <summary>
           /// Desc:读头号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ReadNO {get;set;}

           /// <summary>
           /// Desc:检票员ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CheckTicketUserId {get;set;}

           /// <summary>
           /// Desc:检票时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CheckDate {get;set;}

           /// <summary>
           /// Desc:通过人数
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public int? Quantity {get;set;}

           /// <summary>
           /// Desc:园门ID( 无园门闸机验票，云票务后台验票 值为0)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ScenicGateId {get;set;}

           /// <summary>
           /// Desc:检票方式（1 云票务后台检票，2 景区有园门闸机验票，3景区无园门闸机验票，4App端验票）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? CheckTicketWay {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int UsedQuantity {get;set;}

           /// <summary>
           /// Desc:
           /// Default:newid()
           /// Nullable:False
           /// </summary>           
           public Guid OrderDetailNumber {get;set;}

    }
}
