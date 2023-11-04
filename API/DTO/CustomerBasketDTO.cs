using API.BLL.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItem> items { get; set; } 
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? clientsecret { get; set; }
        public string? paymentcontentid { get; set; }
    }
}
