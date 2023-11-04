namespace API.DTO
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string DeliveryMethodId { get; set; }
        public AdreesDTO ShipToAddress { get; set; }
    }
}
