using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Service;
using Ticket.Model.Model.TravelAgency;
using Ticket.Model.Result;

namespace Ticket.DistributorPlatform.Application
{
    public class GuideFacadeService
    {
        private readonly TravelAgencyGuideService _travelAgencyGuideService;
        public GuideFacadeService(TravelAgencyGuideService travelAgencyGuideService)
        {
            _travelAgencyGuideService = travelAgencyGuideService;
        }

        public GuideViewModel Get(int id)
        {
            return _travelAgencyGuideService.Get(id);
        }

        public TPageResult<GuideViewModel> GetList(GuideQueryModel model)
        {
            return _travelAgencyGuideService.GetList(model);
        }

        public TResult Add(GuideAddModel model)
        {
            return _travelAgencyGuideService.Add(model);
        }


        public TResult Update(GuideUpdateModel model)
        {
            return _travelAgencyGuideService.Update(model);
        }

        public TResult Delete(int id)
        {
            return _travelAgencyGuideService.Delete(id);
        }
    }
}
