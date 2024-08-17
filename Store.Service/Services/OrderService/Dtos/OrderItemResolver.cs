using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entites;
using Store.Data.Entites.OrderEntities;
using Store.Service.Services.ProductServices.Dtos;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderItemResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrderd.PictureUrl))
            {
                return $"{configuration["BaseUrl"]}{source.ItemOrderd.PictureUrl}";
            }
            return null;
        }
    }
}