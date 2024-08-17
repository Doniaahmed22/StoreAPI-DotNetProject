using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entites;
using Store.Data.Entites.OrderEntities;
using Store.Reposatry.Interfaces;
using Store.Reposatry.Specification.Order;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entites.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;
        private readonly IBasketService basketService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentService(IConfiguration configuration, IBasketService basketService , IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this.configuration = configuration;
            this.basketService = basketService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = configuration["Sript:Secretkey"];
            if (basket == null )
            {
               throw new Exception("basket is null");
            }


            var deliveryMethod = await unitOfWork.Repositpry<DeleviryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            var shippingPrice = deliveryMethod.Price;

            foreach (var item in basket.BasketItems)
            {
                var product = await unitOfWork.Repositpry<Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                {
                    item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntendId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card"}
                };

                paymentIntent= await service.CreateAsync(options);

                basket.PaymentIntendId = paymentIntent.Id;
                basket.ClientSecret= paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100)
              
                };
                await service.UpdateAsync(basket.PaymentIntendId, options);
            }

            await basketService.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["Sript:Secretkey"];
            var basket= await basketService.GetBasketAsync(basketId);
            if (basket == null)
            {
                throw new Exception("basket is null");
            }


            var deliveryMethod = await unitOfWork.Repositpry<DeleviryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            var shippingPrice = deliveryMethod.Price;

            foreach (var item in basket.BasketItems)
            {
                var product = await unitOfWork.Repositpry<Product, int>().GetByIdAsync(item.ProductId);
                if (item.Price != product.Price)
                {
                    item.Price = product.Price;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntendId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntendId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(shippingPrice * 100)

                };
                await service.UpdateAsync(basket.PaymentIntendId, options);
            }

            await basketService.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecofication(paymentIntentId);
            var order = await unitOfWork.Repositpry<Order,Guid>().GetWithSpecificationByIdAsync(specs);
            if (order is null)
            {
                throw new Exception("order does not exist");
            }
            order.OrderPayment= OrderPaymentStatues.Failed;
            unitOfWork.Repositpry<Order,Guid>().Updat(order);
            await unitOfWork.CopleteAsync();
            var mappedOrder = mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecofication(paymentIntentId);
            var order = await unitOfWork.Repositpry<Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order is null)
            {
                throw new Exception("order does not exist");
            }
            order.OrderPayment = OrderPaymentStatues.Recevied;
            unitOfWork.Repositpry<Order, Guid>().Updat(order);
            await unitOfWork.CopleteAsync();
            await basketService.DeleteBasketAsync(order.BasketId);
            var mappedOrder = mapper.Map<OrderResultDto>(order);
            

            return mappedOrder;
        }
    }
}
