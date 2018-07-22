using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Lib
{
    public class TongChengConfig
    {
        //=======【基本信息设置】=====================================
        /* 同程信息配置
        * UserId：OTA分配给供应商的账户（必须配置）
        * Version：版本号（必须配置）
        * UserKey：密钥（必须配置）
        * Website：请求地址（必须配置）
        */
        /// <summary>
        /// 同程用户标识
        /// </summary>
        public static readonly string UserId = ConfigurationManager.AppSettings["TongCheng:UserId"];
        public static readonly string Version = ConfigurationManager.AppSettings["TongCheng:Version"];
        public static readonly string UserKey = ConfigurationManager.AppSettings["TongCheng:UserKey"];
        public static readonly string Website = ConfigurationManager.AppSettings["TongCheng:Website"];

        /// <summary>
        /// 供应商分配给携程用户标识
        /// </summary>
        public static readonly string MyAccountId = ConfigurationManager.AppSettings["TongChengCtrip:UserId"];
    }
}
