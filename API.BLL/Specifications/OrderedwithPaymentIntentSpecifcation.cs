using API.BLL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Specifications
{
    public class OrderedwithPaymentIntentSpecifcation : BaseSpecifications<Order>
    {
        public OrderedwithPaymentIntentSpecifcation(string PaymentIntentId) 
            : base(o=>o.paymentcontentid==PaymentIntentId)
        {
        }
    }
}
