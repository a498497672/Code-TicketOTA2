using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_TvmTicket")]
    public partial class Tbl_TvmTicket
    {
           public Tbl_TvmTicket(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:售取票机表（Tbl_Tvm）  id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TvmId {get;set;}

           /// <summary>
           /// Desc:门票表（Tbl_Ticket） Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

    }
}
