using AutoMapper;
using FengjingSDK461.Model.Response;
using Ticket.SqlSugar.Models;

namespace Ticket.Model.AutoMapper
{
    public class TicketProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Tbl_Ticket, ProductInfo>()
                .ForMember(a => a.ProductId, opt => opt.MapFrom(a => a.TicketId))
                .ForMember(a => a.ProductName, opt => opt.MapFrom(a => a.TicketName));

            //CreateMap<GW_Region_Hot, CityHotViewModel>().
            //    ForMember(s => s.CityName, opt => opt.MapFrom(s => s.ShortName));
        }
    }
}
