using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Entities.Order
{
    
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrderd itemorderd, decimal price, int quantity)
        {
            Price = price;
            Quantity = quantity;
        }

        public OrderItem(int id, ProductItemOrderd itemOrderd, decimal price, int quantity)
        {
            Id = id;
            ItemOrderd = itemOrderd;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public ProductItemOrderd ItemOrderd { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
