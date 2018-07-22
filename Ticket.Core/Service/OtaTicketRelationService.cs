using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Repository;

namespace Ticket.Core.Service
{
    public class OtaTicketRelationService
    {
        private readonly OtaTicketRelationRepository _otaTicketRelationRepository;
        public OtaTicketRelationService(OtaTicketRelationRepository otaTicketRelationRepository)
        {
            _otaTicketRelationRepository = otaTicketRelationRepository;
        }

        /// <summary>
        /// 根据分销商id获取所分配的门票id
        /// </summary>
        /// <param name="OtaBusinessId">分销商id</param>
        /// <returns></returns>
        public List<int> GetTicketIds(int otaBusinessId)
        {
            return _otaTicketRelationRepository.GetAll().Where(a => a.OTABusinessId == otaBusinessId).Select(a => a.TicketId).ToList();
        }

        public List<int> GetTicketIds(int otaBusinessId,List<int> productIds)
        {
            return _otaTicketRelationRepository.GetAll().Where(a => a.OTABusinessId == otaBusinessId&& productIds.Contains(a.TicketId)).Select(a => a.TicketId).ToList();
        }

        /// <summary>
        /// 验证分销商是否有这个产品存在
        /// </summary>
        /// <param name="otaBusinessId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool CheckIsTicketId(int otaBusinessId, int productId)
        {
            var entity=_otaTicketRelationRepository.FirstOrDefault(a => a.OTABusinessId == otaBusinessId && a.TicketId == productId);
            return entity == null ? false : true;
        }
    }
}
