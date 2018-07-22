using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 售票机状态
    /// </summary>
    public enum TvmDataStatus
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 0,

        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,
    }
}
