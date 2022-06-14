using AutoMapper;
using DotNetSecurity.DTO;
using DotNetSecurity.Models;

namespace DotNetSecurity.Mapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ForMember(
                src=>src.OrderDetails  , opt=> opt.MapFrom( src=>src.Details)
                
                ).ReverseMap();


            CreateMap<Order,OrderCreateDto >().ForMember(
                src => src.Details, opt => opt.MapFrom(src => src.OrderDetails)
                ).ReverseMap();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailDto>();
        }
    }
}
