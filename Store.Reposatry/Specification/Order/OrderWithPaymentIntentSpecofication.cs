using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Specification.Order
{
    public class OrderWithPaymentIntentSpecofication : BaseSpecification<Data.Entites.OrderEntities.Order>
    {
        public OrderWithPaymentIntentSpecofication(string? paymentIntentId)
           : base(order => order.PaymentIntendId == paymentIntentId)
        {
           
        }
    }
}
