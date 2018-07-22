using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 产品查询类型
    /// </summary>
    public enum ProductQureyType
    {
        /// <summary>
        /// 不分页
        /// </summary>
        [Description("不分页")]
        NoPage = 0,
        /// <summary>
        /// 分页
        /// </summary>
        [Description("分页")]
        Page = 1,
        /// <summary>
        /// 单个产品
        /// </summary>
        [Description("单个产品")]
        OneProduct = 2,
    }
}
