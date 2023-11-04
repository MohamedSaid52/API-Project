using API.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Interfaces
{
    public interface IBasketReposatory
    {
        Task<CustomerBasket> GetBasketAsync(string basketid);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customer);
        Task<bool> DeleteBasketAsync(string basketid);
    }
}
