using System;
using System.Linq;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Model.TravelAgency;
using Ticket.Model.Result;
using Ticket.Utility.Extensions;

namespace Ticket.Core.Service
{
    public class TravelAgencyGuideService
    {
        private readonly TravelAgencyGuideRepository _travelAgencyGuideRepository;
        public TravelAgencyGuideService(TravelAgencyGuideRepository travelAgencyGuideRepository)
        {
            _travelAgencyGuideRepository = travelAgencyGuideRepository;
        }

        public GuideViewModel Get(int id)
        {
            var entity = _travelAgencyGuideRepository.FirstOrDefault(a => a.Id == id);
            return new GuideViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Mobile = entity.Mobile,
                IdCard = entity.IdCard,
                GuideType = entity.GuideType
            };
        }

        public TPageResult<GuideViewModel> GetList(GuideQueryModel model)
        {
            var where = PredicateBuilder.True<Tbl_TravelAgencyGuides>();
            where = PredicateBuilder.And(@where, x => x.OTABusinessId == model.OTABusinessId);
            if (!string.IsNullOrEmpty(model.Name))
            {
                where = PredicateBuilder.And(@where, x => x.Name.Contains(model.Name));
            }
            if (!string.IsNullOrEmpty(model.Mobile))
            {
                where = PredicateBuilder.And(@where, x => x.Mobile == model.Mobile);
            }
            if (!string.IsNullOrEmpty(model.IdCard))
            {
                where = PredicateBuilder.And(@where, x => x.IdCard == model.IdCard);
            }
            var total = 0;
            var list = _travelAgencyGuideRepository.GetPageList(model.Limit, model.Page, out total, where, a => a.CreateTime);
            var result = new TPageResult<GuideViewModel>();
            var data = list.Select(a => new GuideViewModel
            {
                Id = a.Id,
                IdCard = a.IdCard,
                Mobile = a.Mobile,
                Name = a.Name
            }).ToList();
            return result.SuccessResult(data, total);
        }

        public TResult Add(GuideAddModel model)
        {
            var result = new TResult();
            _travelAgencyGuideRepository.Add(new Tbl_TravelAgencyGuides
            {
                EnterpriseId = model.EnterpriseId,
                ScenicId = model.ScenicId,
                OTABusinessId = model.OTABusinessId,
                Name = model.Name,
                Mobile = model.Mobile,
                IdCard = model.IdCard,
                GuideType = model.GuideType,
                CreateTime = DateTime.Now
            });
            return result.SuccessResult();
        }

        public TResult Update(GuideUpdateModel model)
        {
            var result = new TResult();
            var guide = _travelAgencyGuideRepository.FirstOrDefault(a => a.Id == model.Id);
            if (guide == null)
            {
                return result.FailureResult();
            }
            guide.Name = model.Name;
            guide.Mobile = model.Mobile;
            guide.IdCard = model.IdCard;
            guide.GuideType = model.GuideType;
            _travelAgencyGuideRepository.Update(guide);
            return result.SuccessResult();
        }

        public TResult Delete(int id)
        {
            var result = new TResult();
            var guide = _travelAgencyGuideRepository.FirstOrDefault(a => a.Id == id);
            if (guide == null)
            {
                return result.FailureResult();
            }
            _travelAgencyGuideRepository.Delete(guide);
            return result.SuccessResult();
        }
    }
}
