using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Request
{
    /// <summary>
    /// 产品
    /// </summary>
    public class ProductRequest
    {
        /// <summary>
        /// 查询页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 查询数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNo { get; set; }
        /// <summary>
        /// 景区编号
        /// </summary>
        public string SceneryNo { get; set; }
    }
}
