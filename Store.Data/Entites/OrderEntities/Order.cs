using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entites.OrderEntities
{
    public enum OrderPaymentStatues
    {
        Pending,
        Recevied,
        Failed
    }
    public class Order : BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ShippingAaddress shippingAaddress { get; set; }
        public DeleviryMethod DeleviryMethod { get; set; }
        public int? DeleviryMethodId { get; set; }
        public OrderPaymentStatues OrderPayment { get; set; } = OrderPaymentStatues.Pending;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string? PaymentIntendId { get; set; }
        public decimal GetTotal()
            => SubTotal + DeleviryMethod.Price;
        public string? BasketId { get; set; }


    }
}
