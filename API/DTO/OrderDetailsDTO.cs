using API.BLL.Entities.Order;

namespace API.DTO
{
    public class OrderDetailsDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public AdreesDTO ShipedAdress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string Status { get; set; } 
        public decimal Total { get; set; }
    }
}
