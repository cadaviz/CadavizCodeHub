using AutoMapper;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.WebApi.Responses;

namespace CadavizCodeHub.WebApi.Mappers
{
    internal class OrderResponseProfile : Profile
    {
        public OrderResponseProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<Item, OrderResponseItem>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<Product, OrderResponseProduct>();
        }
    }
}
