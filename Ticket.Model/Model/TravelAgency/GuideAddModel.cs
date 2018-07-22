using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.TravelAgency
{
    public class GuideAddModel
    {
        public int EnterpriseId { get; set; }
        public int ScenicId { get; set; }
        public int OTABusinessId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string IdCard { get; set; }
        public int GuideType { get; set; }
    }
}
