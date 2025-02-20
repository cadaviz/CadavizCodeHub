using AutoMapper;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.WebApi.Requests;

namespace CadavizCodeHub.WebApi.Mappers
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