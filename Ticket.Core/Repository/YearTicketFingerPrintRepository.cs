using System.Collections.Generic;
using System.Linq;
using Ticket.SqlSugar.Models;
using Ticket.SqlSugar.Repository;

namespace Ticket.Core.Repository
{
    //年票会员指纹库
    public class YearTicketFingerPrintRepository : RepositoryBase<Tbl_YearTicket_FingerPrint>
    {

        public List<Tbl_YearTicket_FingerPrint> GetByUserId(int yearTicketUserId)
        {
            var list = GetAllList(a => a.YearTicketUserId == yearTicketUserId).ToList();
            return list;
        }
    }
}
