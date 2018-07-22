using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 门票配置
    /// </summary>
    public class TicketRuleService
    {
        private readonly TicketRuleRepository _ticketRuleRepository;

        public TicketRuleService(TicketRuleRepository ticketRuleRepository)
        {
            _ticketRuleRepository = ticketRuleRepository;
        }

        /// <summary>
        /// 获取门票配置
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public Tbl_TicketRule Get(int id)
        {
            return _ticketRuleRepository.FirstOrDefault(a => a.Id == id);
        }

        public List<Tbl_TicketRule> GetList(List<int> ids)
        {
            return _ticketRuleRepository.GetAllList(a => ids.Contains(a.Id));
        }
    }
}
