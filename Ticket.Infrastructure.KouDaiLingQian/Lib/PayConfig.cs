using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Lib
{
    public class PayConfig
    {
        //public static readonly string Version = "1.0.0";
        //public static readonly string SystemCode = "99999";
        //public static readonly string PartnerKey = "7bff5871936e4f46978bd5a70c00f249";//35649FB15051E21D5569A28430273F8C80
        //public static readonly string MerchantNo = "48e9e269deb44cd89b35a32d483dfea0";
        //public static readonly string DefaultKey = "1111222233334444";
        //public static readonly string SnNo = "201807041512000";
        //public static readonly string WebSite = "http://139.199.46.76:8083/MerchantPayTest";

        public static readonly string Version = ConfigurationManager.AppSettings["koudailingqian:Version"];//"1.0.0";
        public static readonly string SystemCode = ConfigurationManager.AppSettings["koudailingqian:SystemCode"];//"20007";
        public static readonly string PartnerKey = ConfigurationManager.AppSettings["koudailingqian:PartnerKey"];//"7bff5871936e4f46978bd5a70c00f249";//支付key
        public static readonly string DefaultKey = ConfigurationManager.AppSettings["koudailingqian:DefaultKey"];//"a4146ca564a4485bb9df1d428e31baed";//初始key，用于激活
        public static readonly string SnNo = ConfigurationManager.AppSettings["koudailingqian:SnNo"];//"200072018121200001";
        public static readonly string WebSite = ConfigurationManager.AppSettings["koudailingqian:WebSite"];//"https://www.koudailingqian.com";

    }
}
