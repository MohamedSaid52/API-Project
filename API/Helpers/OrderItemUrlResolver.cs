using API.BLL.Entities;
using API.BLL.Entities.Order;
using API.DTO;
using AutoMapper;

namespace API.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrderd.PictureUrl))
                return configuration["ApiUrl"] + source.ItemOrderd.PictureUrl;
            return null;
        }
    }
}
