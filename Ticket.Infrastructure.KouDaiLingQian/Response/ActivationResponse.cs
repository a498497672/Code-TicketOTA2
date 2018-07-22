using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Response
{
    public class ActivationResponse : BaseResponse
    {
        public string DateStr { get; set; }
        public string Data { get; set; }
    }

    public class ActivationDataResponse
    {
        public string ActivationTime { get; set; }
        public string Key { get; set; }
        public string MerchantNo { get; set; }
    }
}
