namespace Ticket.Model.Model
{
    public class PageBase
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int Limit { get; set; }
    }
}
