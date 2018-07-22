
namespace Ticket.Model.Enum
{
    /// <summary>
    /// 门票CheckWay状态
    /// </summary>
    public enum TicketCheckWayType
    {
        /// <summary>
        /// 全部通过
        /// </summary> 
        AllPass = 1,
        /// <summary>
        /// 全部不通过
        /// </summary> 
        AllNotPass = 2,
        /// <summary>
        /// 3：指定闸机（此时和闸机关联表联合）
        /// </summary>
        SpecifyTurnstile = 3
    }
}
