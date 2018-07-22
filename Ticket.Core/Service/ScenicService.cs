using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 检票
    /// </summary>
    public class ScenicService
    {
        private readonly ScenicRepository _scenicRepository;

        public ScenicService(ScenicRepository scenicRepository)
        {
            _scenicRepository = scenicRepository;
        }

        public List<Tbl_Scenic> GetList(List<int> scenicIds)
        {
            return _scenicRepository.GetAllList(o => scenicIds.Contains(o.ScenicId));
        }

        public Tbl_Scenic Get(int scenicId)
        {
            return _scenicRepository.FirstOrDefault(a => a.ScenicId == scenicId);
        }

        /// <summary>
        /// 获取 景区是否启用的短信功能，且有可用短信额度
        /// </summary>
        /// <returns></returns>
        public Tbl_Scenic GetSurplusScenic(int scenicId)
        {
            return _scenicRepository.FirstOrDefault(o => o.ScenicId == scenicId && (o.DataStatus & 1) == 0 && (o.DataStatus & 2) == 0 && o.SmsCount > 0);
        }

        /// <summary>
        /// 更改景区短信额度
        /// </summary>
        /// <param name="scenicId"></param>
        public void UpdateSmsCount(int scenicId)
        {
            var scenic = _scenicRepository.FirstOrDefault(o => o.ScenicId == scenicId);
            if (scenic != null)
            {
                scenic.SmsCount--;
                _scenicRepository.Update(scenic);
            }
        }
    }
}
