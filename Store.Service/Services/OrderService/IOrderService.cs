using Store.Data.Entites;
using Store.Service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderResultDto> CreateOrderasync(OrderDto input);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForUserAsync(string BuyerEmail);
        Task<OrderResultDto> GetOrderByIdasync(Guid id,string BuyerEmail);
        Task<IReadOnlyList<DeleviryMethod>> GetAllDeleviryMethodAsync();


    }
}
