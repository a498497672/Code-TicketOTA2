using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Repository;
using Ticket.Model.Model;

namespace Ticket.Core.Service
{
    public class YearTicketFingerPrintService
    {
        private readonly YearTicketFingerPrintRepository _yearTicketFingerPrintRepository;
        public YearTicketFingerPrintService(YearTicketFingerPrintRepository yearTicketFingerPrintRepository)
        {
            _yearTicketFingerPrintRepository = yearTicketFingerPrintRepository;
        }

        public List<YearTicketFingerPrintModel> GetFingers(int yearTicketUserId)
        {
            var list = _yearTicketFingerPrintRepository.GetAllList(a => a.YearTicketUserId == yearTicketUserId)
                .Select(a => new YearTicketFingerPrintModel
                {
                    FingerFeature = a.FingerFeature,
                    Img = a.Img,
                    YearTicketUserId = a.YearTicketUserId
                }).ToList();
            return list;
        }
    }
}
