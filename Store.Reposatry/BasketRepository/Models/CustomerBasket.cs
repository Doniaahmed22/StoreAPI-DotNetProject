﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.BasketRepository.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; } 
        public List<BasketItem> BasketItems { get; set; }= new List<BasketItem>();

        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
