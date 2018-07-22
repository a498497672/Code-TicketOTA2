using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 操作状态
    /// </summary>
    public enum ActionStatus
    {
        /// <summary>
        /// 显示
        /// </summary>
        [Description("显示")]
        Show = 0,

        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add = 1,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Edit = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3,

        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login = 4,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Audit = 5,

        /// <summary>
        /// 重置
        /// </summary>
        [Description("重置")]
        Reset = 6,

        /// <summary>
        /// 设置打印
        /// </summary>
        [Description("设置打印")]
        SetPrint = 7,

        /// <summary>
        /// 导出
        /// </summary>
        [Description("导出")]
        Export = 8,

        /// <summary>
        /// 生成
        /// </summary>
        [Description("生成")]
        Generate = 9,

        /// <summary>
        /// 礼包发货
        /// </summary>
        [Description("礼包发货")]
        SendGoods = 10,

        /// <summary>
        /// 退票
        /// </summary>
        [Description("退票")]
        RefundTicket = 11,

        /// <summary>
        /// 购票
        /// </summary>
        [Description("购票")]
        SaleTicket = 12,

        /// <summary>
        /// 退出登录
        /// </summary>
        [Description("退出登录")]
        LoginOut = 13,

        /// <summary>
        /// 修改密码
        /// </summary>
        [Description("修改密码")]
        EditPass = 14,

        /// <summary>
        /// 激活票
        /// </summary>
        [Description("激活门票")]
        ActivateT = 15,

        /// <summary>
        /// 取消激活
        /// </summary>
        [Description("取消激活")]
        NoActivateT = 16,

        /// <summary>
        /// 退票
        /// </summary>
        [Description("批量退票")]
        BatchRefundTicket = 17,

        /// <summary>
        /// 新加年票会员
        /// </summary>
        [Description("新加年票会员")]
        AddYearTicketUser = 18,

        /// <summary>
        /// 年票续期
        /// </summary>
        [Description("年票续期")]
        DelayToDo = 19,

        /// <summary>
        /// 年票挂失
        /// </summary>
        [Description("年票挂失")]
        ReportLossToDo = 20,

        /// <summary>
        /// 指纹采集
        /// </summary>
        [Description("指纹采集")]
        CollectFingerPrint = 21
    }
}
