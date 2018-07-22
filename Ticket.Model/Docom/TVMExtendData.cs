using DocomSDK.TVM;

namespace Ticket.Model.Docom
{
    public class TVMExtendData
    {
        /// <summary>
        /// 设备对象
        /// </summary>
        public TVMDevice Device { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
}
