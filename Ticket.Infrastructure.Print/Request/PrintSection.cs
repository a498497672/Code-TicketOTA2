using System.Configuration;

namespace Ticket.Infrastructure.Print.Request
{
    /// <summary>
    /// PrintSection块，在web.config中提供PrintSection块定义
    /// </summary>
    public class PrintSection : ConfigurationSection
    {
        /// <summary>
        /// key
        /// </summary>
        [ConfigurationProperty("key", DefaultValue = "Print001")]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        [ConfigurationProperty("partner", DefaultValue = "21108")]
        public string Partner
        {
            get { return (string)this["partner"]; }
            set { this["partner"] = value; }
        }
        /// <summary>
        /// API 密钥
        /// </summary>
        [ConfigurationProperty("apiKey", DefaultValue = "2f12779e593499b981beb7fe9644bb934b9dada7")]
        public string ApiKey
        {
            get { return (string)this["apiKey"]; }
            set { this["apiKey"] = value; }
        }

        /// <summary>
        /// 机器码 终端编号
        /// </summary>
        [ConfigurationProperty("machineCode", DefaultValue = "4004547827")]
        public string MachineCode
        {
            get { return (string)this["machineCode"]; }
            set { this["machineCode"] = value; }
        }

        /// <summary>
        /// 终端密钥
        /// </summary>
        [ConfigurationProperty("machineKey", DefaultValue = "6a7r3enumfbu")]
        public string MachineKey
        {
            get { return (string)this["machineKey"]; }
            set { this["machineKey"] = value; }
        }
    }
}
