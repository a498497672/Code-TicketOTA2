using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Infrastructure.Print.Request;

namespace Ticket.Infrastructure.Print.Lib
{
    /// <summary>
    /// 打印配置信息加载
    /// </summary>
    public class PrintManager
    {
        /// <summary>
        /// 注意事项文字描述
        /// </summary>
        public static string Considerations = ConfigurationManager.AppSettings["Considerations"];

        public static PrintConfigData Get(string printKey)
        {
            var printConfigData = new PrintConfigData();
            var list = GetSection();
            var data = list.FirstOrDefault(a => a.Key == printKey);
            if (data != null)
            {
                printConfigData.Partner = data.Partner;
                printConfigData.ApiKey = data.ApiKey;
                printConfigData.MachineCode = data.MachineCode;
                printConfigData.MachineKey = data.MachineKey;
                return printConfigData;
            }
            return null;
        }

        /// <summary>
        /// 配置信息实体
        /// </summary>
        public static IList<PrintSection> Instance
        {
            get
            {
                return GetSection();
            }
        }

        private static IList<PrintSection> GetSection()
        {
            var dddd = ConfigurationManager.GetSection("printSection");
            var dic = ConfigurationManager.GetSection("printSection") as Dictionary<string, PrintSection>;
            return dic.Values.ToList();
        }
    }
}
