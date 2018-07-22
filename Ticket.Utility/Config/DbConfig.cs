using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Ticket.Utility.Config
{
    public static class DbConfig
    {
        public static string TicketConnectionString = ConfigurationManager.ConnectionStrings["YTS_TicketDBContext"].ConnectionString;
    }
}
