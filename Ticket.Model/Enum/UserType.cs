using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 用户类别
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 售票员
        /// </summary>
        [Description("售票员")]
        SaleUser = 1,

        /// <summary>
        /// 检票员
        /// </summary>
        [Description("检票员")]
        CheckUser = 2,

        /// <summary>
        /// 景区管理员
        /// </summary>
        [Description("景区管理员")]
        AdminUser = 3
    }
}
