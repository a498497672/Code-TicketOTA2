using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=120.76.195.73;uid=sa;pwd=#3edc$4rfv;database=NiuYuZui_TicketDB",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });

            //获取和设置当前目录(该进程从中启动的目录)的完全限定目录  
            string path2 = System.Environment.CurrentDirectory;
            var index = path2.IndexOf("SqlSugarCreate");
            var path = path2.Substring(0, index) + "Ticket.SqlSugar\\Models";

            //生成model
            db.DbFirst.IsCreateAttribute().CreateClassFile(path, "Ticket.SqlSugar.Models");
        }
    }
}
