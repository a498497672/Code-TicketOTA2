using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Ticket.SqlSugar.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tbl_EnterpriseUserTicket")]
    public partial class Tbl_EnterpriseUserTicket
    {
           public Tbl_EnterpriseUserTicket(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:售票员表（Tbl_EnterpriseUser）  id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int EnterpriseUserId {get;set;}

           /// <summary>
           /// Desc:门票表（Tbl_Ticket） Id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int TicketId {get;set;}

    }
}
