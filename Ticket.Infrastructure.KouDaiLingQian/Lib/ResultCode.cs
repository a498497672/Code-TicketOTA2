using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Lib
{
    public class ResultCode
    {
        /// <summary>
        /// 成功（如果贵方系统中订单已经生成或者已经取消就按正常返回）
        /// </summary>
        public static string Success = "00";

        /// <summary>
        /// 失败
        /// </summary>
        public static string Error = "01";

        /// <summary>
        /// 请求参数不合法
        /// </summary>
        public static string DataError = "02";

        /// <summary>
        /// 系统错误
        /// </summary>
        public static string ManageError = "99";

        /// <summary>
        /// 等待用户支付中
        /// </summary>
        public static string UserPaying = "userPaying";
        /// <summary>
        /// 系统错误
        /// </summary>
        public static string SystemError = "systemError";
    }
}
