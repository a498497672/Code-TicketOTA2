using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    public class OtaBusinessService
    {
        private readonly OtaBusinessRepository _otaBusinessRepository;

        public OtaBusinessService(OtaBusinessRepository otaBusinessRepository)
        {
            _otaBusinessRepository = otaBusinessRepository;
        }

        /// <summary>
        /// 分销商信息
        /// </summary>
        /// <param name="identityKey">分销商身份标识</param>
        /// <returns></returns>
        public Tbl_OTABusiness Get(int id)
        {
            return _otaBusinessRepository.Single(a => a.Id == id);
        }

        /// <summary>
        /// 分销商信息
        /// </summary>
        /// <param name="identityKey">分销商身份标识</param>
        /// <returns></returns>
        public Tbl_OTABusiness Get(string identityKey)
        {
            return _otaBusinessRepository.FirstOrDefault(a => a.IdentityKey == identityKey && a.DataStatus == 1);
        }

        /// <summary>
        /// 分销商信息
        /// </summary>
        /// <param name="codes">分销商编码集合</param>
        /// <param name="type">商家类型(1:OTA;2:旅行社)</param>
        /// <returns></returns>
        public List<Tbl_OTABusiness> GetList(List<string> codes, int type)
        {
            return _otaBusinessRepository.GetAllList(a => codes.Contains(a.Code) && a.BusinessType == type);
        }
    }
}
