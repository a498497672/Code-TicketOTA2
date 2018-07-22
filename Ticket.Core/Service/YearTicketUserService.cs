using System;
using Ticket.Core.Repository;

namespace Ticket.Core.Service
{
    public class YearTicketUserService
    {
        private readonly YearTicketUserRepository _yearTicketUserRepository;
        public YearTicketUserService(YearTicketUserRepository yearTicketUserRepository)
        {
            _yearTicketUserRepository = yearTicketUserRepository;
        }

        /// <summary>
        /// 验证年卡是否有效
        /// </summary>
        /// <param name="cradNo">年卡号</param>
        /// <param name="msg">提示消息</param>
        /// <param name="yearTicketUserId">年卡id</param>
        /// <returns></returns>
        public bool CheckYearTicketUser(string cradNo, out string msg, out int yearTicketUserId)
        {
            yearTicketUserId = 0;
            var entity = _yearTicketUserRepository.FirstOrDefault(a => a.CradNo == cradNo);
            if (entity == null)
            {
                msg = "无效年卡！";
                return false;
            }
            //判断年卡的有效期
            if (entity.YearTicketValidityDateEnd < DateTime.Now)
            {
                msg = "年卡已过期，请续费！";
                return false;
            }
            msg = "验卡通过！";
            yearTicketUserId = entity.UserId;
            return true;
        }
    }
}
