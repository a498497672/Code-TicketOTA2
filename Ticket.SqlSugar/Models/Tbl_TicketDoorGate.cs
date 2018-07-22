using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TicketDoorGate")]
    public partial class Tbl_TicketDoorGate
    {
           public Tbl_TicketDoorGate(){


           }
           /// <summary>
           /// Desc:闸机关联ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int TicketDoorGateId {get;set;}

           /// <summary>
           /// Desc:门票ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

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
           /// Desc:闸机ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int DoorGateId {get;set;}

           /// <summary>
           /// Desc:1：景区自己的，2：OTA
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketSource {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DoorGateNO {get;set;}

    }
}
