namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 所有产品(门票)查询
    /// </summary>
    public class ProductQueryRequest
    {
        public HeadRequest Head { get; set; }
        public Product Body { get; set; }
    }

    public class Product
    {
        /// <summary>
        /// 分页的形式，0：不分页 1：分页 2:获取单个产品
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 分销商产品 ID
        /// </summary>
        public int ProductId { get; set; }
    }
}
