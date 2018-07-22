using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Service;
using Ticket.Model.Model.Ticket;
using Ticket.Model.Result;

namespace Ticket.SaleTicketPlatform.Application
{
    public class TicketFacadeService
    {
        private readonly TicketTypeService _ticketTypeService;
        private readonly EnterpriseUserService _enterpriseUserService;
        private readonly TicketService _ticketService;

        public TicketFacadeService(
            TicketTypeService ticketTypeService,
            EnterpriseUserService enterpriseUserService,
            TicketService ticketService)
        {
            _ticketTypeService = ticketTypeService;
            _enterpriseUserService = enterpriseUserService;
            _ticketService = ticketService;
        }

        public TResult<List<TicketTypeViewModel>> GetTicketType()
        {
            var result = new TResult<List<TicketTypeViewModel>>();
            var userInfo = _enterpriseUserService.LoginForSession();
            var data = _ticketTypeService.GetList(userInfo.ScenicId,userInfo.UserId);
            return result.SuccessResult(data);
        }

        public TPageResult<TblTicketViewModel> GetList(TblTicketQueryModel model)
        {
            var userInfo = _enterpriseUserService.LoginForSession();
            model.ScenicId = userInfo.ScenicId;
            model.UserId = userInfo.UserId;
            return _ticketService.GetList(model);
        }
    }
}
