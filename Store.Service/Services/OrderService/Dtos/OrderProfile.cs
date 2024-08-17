using AutoMapper;
using Store.Data.Entites.IdentitEntites;
using Store.Data.Entites.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        { 
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<AddressDto, ShippingAaddress>().ReverseMap();
            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeleviryMethodName, options => options.MapFrom(src => src.DeleviryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, options => options.MapFrom(src => src.DeleviryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductItemId, options => options.MapFrom(src => src.ItemOrderd.ProductItemId))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.ItemOrderd.ProductName))
               .ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.ItemOrderd.PictureUrl))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<OrderItemResolver>()).ReverseMap();







        }
    }
}
