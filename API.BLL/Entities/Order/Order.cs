using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Entities.Order
{
    public class Order:BaseEntity
    {
        private readonly string paymentIntentId;

        public Order()
        {
            
        }
        public Order(string buyerEmail, Adress shipedAdress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> Orderitems,
            decimal subTotal,string PaymentIntentId )
        {
            BuyerEmail = buyerEmail;
            ShipedAdress = shipedAdress;
            DeliveryMethod = deliveryMethod;
            OrderItems = OrderItems;
            SubTotal = subTotal;
            paymentIntentId = PaymentIntentId;
        }

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public Adress ShipedAdress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set;}
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.pending;
        public string paymentcontentid { get; set; }
        public decimal GetTotal()
         => SubTotal + DeliveryMethod.Price;
    }
}
