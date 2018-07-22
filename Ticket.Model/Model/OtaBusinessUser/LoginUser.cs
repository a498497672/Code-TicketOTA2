using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.OtaBusinessUser
{
    public class LoginUser
    {
        public int Id { get; set; }
        public int OtaBusinessId { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
    }
}
