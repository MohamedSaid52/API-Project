using API.BLL.Entities;
using API.BLL.Interfaces;
using API.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   
    public class BasketController : BaseAPIController
    {
        private readonly IBasketReposatory basketReposatory;
        private readonly IMapper mapper;

        public BasketController(IBasketReposatory basketReposatory,IMapper mapper)
        {
            this.basketReposatory = basketReposatory;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket=await basketReposatory.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var customerbasket = mapper.Map<CustomerBasket>(basket);
            var updatedbasket=await basketReposatory.UpdateBasketAsync(customerbasket);
            return Ok(updatedbasket);
        }
        [HttpDelete]
        public async Task DeleteBasketById(string id)
        {
            await basketReposatory.DeleteBasketAsync(id);
        }
    }
}
