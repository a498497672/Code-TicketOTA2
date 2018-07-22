using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Response
{
    public class BaseResponse
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string Sign { get; set; }
    }
}
