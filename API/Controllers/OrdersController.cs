using API.BLL.Entities.Order;
using API.BLL.Interfaces;
using API.DTO;
using API.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrederService orederService;
        private readonly IMapper mapper;

        public OrdersController(IOrederService orederService,IMapper mapper)
        {
            this.orederService = orederService;
            this.mapper = mapper;
        }

        [HttpPost("Create Order")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = User?.FindFirst(ClaimTypes.Email);
            var address = mapper.Map<Adress>(orderDto.ShipToAddress);
            var order = await orederService.CreaterderAsync(email,orderDto.DeliveryMethodId,orderDto.BasketId, address);
            if (order is null)
                return BadRequest(new APIResponse(400,"Problem Creating Order"));
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrdersForUser()
        {
            var email = User?.FindFirst(ClaimTypes.Email);
            var orders = await orederService.GetOrderForUserAsync(email);
            return Ok(mapper.Map<IReadOnlyList<OrderDetailsDTO>>(orders));
        }
        [HttpGet("{(id)}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrdersByIdForUser(int id)
        {
            var email = User?.FindFirst(ClaimTypes.Email);
            var order=await orederService.GetOrderById(id,email);
            if (order is null) 
                return NotFound(new APIResponse(404));
            return Ok(mapper.Map<OrderDetailsDTO>(order));
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
          => Ok(await orederService.GetDeliveryMethodAsync());
    }
}
