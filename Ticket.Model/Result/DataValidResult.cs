using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Result
{
    public class DataValidResult<TItem>
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<TItem> List { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public TItem Item { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 验证结果代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 描述(验证结果说明)
        /// </summary>
        public string Message { get; set; }
    }

    public class DataValidResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 验证结果代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 描述(验证结果说明)
        /// </summary>
        public string Message { get; set; }
    }
}
