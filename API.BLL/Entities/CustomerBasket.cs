using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket( string id)
        {
            id = id;
        }
        public CustomerBasket()
        {
            
        }
        public string Id { get; set; }
        public List<BasketItem> items { get; set; }=new List<BasketItem>();
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? clientsecret { get; set; }
        public string? paymentcontentid { get; set; }
    }
}
