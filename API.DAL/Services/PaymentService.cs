using API.BLL.Entities;
using API.BLL.Entities.Order;
using API.BLL.Interfaces;
using API.BLL.Interfaces___Copy;
using API.BLL.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = API.BLL.Entities.Product;

namespace API.DAL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketReposatory basketReposatory;
        private readonly IConfiguration configuration;

        public PaymentService(IUnitOfWork unitOfWork,IBasketReposatory basketReposatory,IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.basketReposatory = basketReposatory;
            this.configuration = configuration;
        }
        public async Task<CustomerBasket> CrateOrUpdatePaymentIntent(string basketid)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:Secretkey"];
            var basket=basketReposatory.GetBasketAsync(basketid);
            if (basket is null)
                return null;
            var shipingprice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliverymethod = await unitOfWork.GenricReposatory<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.value);
                shipingprice = deliverymethod.Price;
            }
            foreach (var item in basket.Items)
            {
                var productitem = await unitOfWork.GenricReposatory<Product>().GetByIdAsync(item.Id);
                if (item.Price != productitem.Price)
                    item.Price = productitem.Price;
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentintentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount=basket.items.sum(i=>i.Quantity*(i.Price*100))+((long)shipingprice*100),
                    Currency="USD",
                    PaymentMethod=new List<string>>{"Card"},
                };
                intent = await service.CreateAsync(options);
                basket.PaymentintentId = intent.Id;
                    basket.ClientSecret=intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = basket.items.sum(i => i.Quantity * (i.Price * 100)) + ((long)shipingprice * 100),
                    
                    //Currency = "USD",
                    //PaymentMethod = new List<string>>{ "Card" },
                };
                await service.UpdateAsync(basket.PaymentintentId,options);
            }
            await basketReposatory.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentintendid)
        {
            var spec = new OrdersWithItemAndOrderingSpecification(paymentintendid);
            var order=await unitOfWork.GenricReposatory<Order>().GetEntityWithSpecifcation(spec);
            if (order is null)
                return null;
            order.Status = OrderStatus.paymentfailed;
            unitOfWork.GenricReposatory<Order>.Update(order);
            await unitOfWork.complete();
            return order;
        }

        public Task<Order> UpdateOrderPaymentSucceeded(string paymentintendid)
        {
            var spec = new OrdersWithItemAndOrderingSpecification(paymentintendid);
            var order = await unitOfWork.GenricReposatory<Order>().GetEntityWithSpecifcation(spec);
            if (order is null)
                return null;
            order.Status = OrderStatus.paymentrecived;
            unitOfWork.GenricReposatory<Order>.Update(order);
            await unitOfWork.complete();
            return order;
        }
    }
}
