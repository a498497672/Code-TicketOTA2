using System.ComponentModel;

namespace Ticket.Model.Enum
{
    public enum ActivateType
    {
        /// <summary>
        /// 系统激活
        /// </summary>
        [Description("系统激活")]
        System = 0,
        /// <summary>
        /// 人为激活
        /// </summary>
        [Description("人为激活")]
        Person = 1,
    }
}
