using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.SqlSugar
{
    /// <summary>
    /// 静态配置类
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 数据库连接字符串(私有字段)
        /// </summary>
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["YTS_TicketDBContext"].ConnectionString;
        //private static readonly string _connectionString = "server=120.76.195.73;uid=sa;pwd=#3edc$4rfv;database=NiuYuZui_TicketDB";
        /// <summary>
        /// 数据库连接字符串(公有属性)
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
        }
    }
}
