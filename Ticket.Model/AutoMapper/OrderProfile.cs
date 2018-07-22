using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.AutoMapper
{
    public class OrderProfile : Profile
    {
        protected override void Configure()
        {
            //CreateMap<GW_Region, CityViewModel>().
            //    ForMember(s => s.Name, opt => opt.MapFrom(s => s.ShortName));

            //CreateMap<GW_Region_Hot, CityHotViewModel>().
            //    ForMember(s => s.CityName, opt => opt.MapFrom(s => s.ShortName));
        }
    }
}
