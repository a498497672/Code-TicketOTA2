using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.TravelAgency
{
    /// <summary>
    /// 导游列表查询
    /// </summary>
    public class GuideQueryModel: PageBase
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string IdCard { get; set; }
        public int OTABusinessId { get; set; }
    }
}
