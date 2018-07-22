using System;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 年票入园记录
    /// </summary>
    public class YearTicketAdmissionRecordService
    {
        private readonly YearTicketAdmissionRecordRepository _yearTicketAdmissionRecordRepository;
        private readonly YearTicketUserRepository _yearTicketUserRepository;

        public YearTicketAdmissionRecordService(
            YearTicketAdmissionRecordRepository yearTicketAdmissionRecordRepository,
            YearTicketUserRepository yearTicketUserRepository)
        {
            _yearTicketAdmissionRecordRepository = yearTicketAdmissionRecordRepository;
            _yearTicketUserRepository = yearTicketUserRepository;
        }

        /// <summary>
        /// 添加年票入园记录
        /// </summary>
        /// <param name="cradNo">年卡号</param>
        public void AddYearTicketAdmissionRecord(string cradNo)
        {
            var entity = _yearTicketUserRepository.FirstOrDefault(a => a.CradNo == cradNo);
            if (entity != null)
            {
                _yearTicketAdmissionRecordRepository.Add(new Tbl_YearTicket_AdmissionRecord
                {
                    CradNo = entity.CradNo,
                    IdCard = entity.IdCard,
                    UserId = entity.UserId,
                    UserName = entity.UserName,
                    ValidityDateEnd = entity.ValidityDateEnd,
                    AdmissionTime = DateTime.Now
                });
            }
        }
    }
}
