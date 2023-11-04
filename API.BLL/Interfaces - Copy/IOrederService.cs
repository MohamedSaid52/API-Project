using API.BLL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Interfaces
{
    public interface IOrederService
    {
        Task<Order> CreaterderAsync(string buyerEmail, int deliveryMethodId, string basketId, Adress ShiingAdress);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> GetOrderById(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
        Task CreaterderAsync(Claim email, string deliveryMethodId, string basketId, Adress address);
    }
}
