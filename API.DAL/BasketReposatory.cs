using API.BLL.Entities;
using API.BLL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.DAL
{
    public class BasketReposatory : IBasketReposatory
    {
        private readonly IDatabase _database;
        public BasketReposatory(IConnectionMultiplexer redis)
        {
            _database =redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
           => await _database.KeyDeleteAsync(basketid);

        public async Task<CustomerBasket> GetBasketAsync(string basketid)
        {
            var data=await _database.StringGetAsync(basketid);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customer)
        {
            var created = await _database.StringSetAsync(customer.Id, JsonSerializer.Serialize<CustomerBasket>(customer),TimeSpan.FromDays(30));
            if (!created)
                return null;
            return await GetBasketAsync(customer.Id);
        }
    }
}
