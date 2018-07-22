namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 单个产品(门票)查询
    /// </summary>
    public class ProductInfoQueryRequest : RequestBase
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductId { get; set; }
    }
}
