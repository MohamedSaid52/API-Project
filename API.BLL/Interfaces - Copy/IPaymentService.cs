using API.BLL.Entities;
using API.BLL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Interfaces___Copy
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CrateOrUpdatePaymentIntent(string basketid);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentintendid);
        Task<Order> UpdateOrderPaymentFailed(string paymentintendid);
    }
}
