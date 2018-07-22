using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.TaskEngine.Application.Model
{
    public class ResultData
    {
        /// <summary>
        /// true 调用成功 false 调用失败
        /// </summary>
        public bool IsTrue { get; set; }
        /// <summary>
        /// 结果代码
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 结果代码详情
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// json字符串
        /// </summary>
        public string ResultJson { get; set; }

    }
}
