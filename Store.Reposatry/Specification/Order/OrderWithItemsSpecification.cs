using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Specification.Order
{
    public class OrderWithItemsSpecification : BaseSpecification<Data.Entites.OrderEntities.Order>
    {
        public OrderWithItemsSpecification(string buyerEmail)
            :base(order => order.BuyerEmail == buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeleviryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderWithItemsSpecification(Guid id,string buyerEmail)
         : base(order => order.BuyerEmail == buyerEmail   && order.Id== id )
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeleviryMethod);
         
        }

    }
}
