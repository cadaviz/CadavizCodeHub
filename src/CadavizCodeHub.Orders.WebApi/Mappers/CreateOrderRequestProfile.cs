using AutoMapper;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.WebApi.Requests;

namespace CadavizCodeHub.Orders.WebApi.Mappers
{
    internal class CreateOrderRequestProfile : Profile
    {
        public CreateOrderRequestProfile()
        {
            CreateMap<CreateOrderRequest, Order>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateOrderRequestItem, Item>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<CreateOrderRequestProduct, Product>();
        }
    }
}