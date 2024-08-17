using Store.Data.Entites.OrderEntities;
using Store.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }                    
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto shippingAaddress { get; set; }
        public string DeleviryMethodName { get; set; }
        public OrderPaymentStatues OrderPayment { get; set; } 
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntendId { get; set; }
        public string? BasketId { get; set; }

    }
}
