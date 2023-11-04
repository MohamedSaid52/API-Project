using API.BLL.Entities;
using API.BLL.Entities.Order;
using API.BLL.Interfaces;
using API.BLL.Interfaces___Copy;
using API.BLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class OrederService : IOrederService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketReposatory basketReposatory;
        private readonly IPaymentService paymentService;

        public OrederService(IUnitOfWork unitOfWork,IBasketReposatory basketReposatory,IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.basketReposatory = basketReposatory;
            this.paymentService = paymentService;
        }
        public async Task<Order> CreaterderAsync(string buyerEmail, int deliveryMethodId, string basketId, Adress ShiingAdress)
        {
            //get basket from the repo
            var basket = await basketReposatory.GetBasketAsync(basketId);
            //get items from the product repo
            var items=new List<OrderItem>();
            foreach (var item in basket.items)
            {
                var productitem = await unitOfWork.GenricReposatory<Product>().GetByIdAsync(item.Id);
                var itemorderd = new ProductItemOrderd(productitem.Id, productitem.Name, productitem.PictureUrl);
                var orderItem = new OrderItem(itemorderd, productitem.Price, item.Quantity);
                items.Add(orderItem);
            }
            //get delivery method from repo
            var delivaerymethod=await unitOfWork.GenricReposatory<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            //calculatesubtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            //check if order exist=>TODO
            var spec = new OrderedwithPaymentIntentSpecifcation(basket.paymentcontentid);
            var existingorder=await unitOfWork.GenricReposatory<Order>().GetEntityWithSpecifcation(spec);
            if (existingorder != null)
            {
                unitOfWork.GenricReposatory<Order>().Delete(existingorder);
                await paymentService.CrateOrUpdatePaymentIntent(basket.paymentcontentid);

            }
            //Create order
            var order =new Order(buyerEmail,ShiingAdress,delivaerymethod,items,subtotal,basket.paymentcontentid);
            unitOfWork.GenricReposatory<Order>().Add(order);
            //save to db
            var result = await unitOfWork.complete();
            if (result <= 0) return null;
            await basketReposatory.DeleteBasketAsync(basketId);
            return order;
        }

        public Task CreaterderAsync(Claim email, string deliveryMethodId, string basketId, Adress address)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
         => await unitOfWork.GenricReposatory<DeliveryMethod>().GetAllAsync();

        public async Task<Order> GetOrderById(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemAndOrderingSpecification(id,buyerEmail);
            return await unitOfWork.GenricReposatory<Order>().GetEntityWithSpecifcation(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemAndOrderingSpecification( buyerEmail);
            return await unitOfWork.GenricReposatory<Order>().listwithspecificationasync(spec);
        }
    }
}
