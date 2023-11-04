﻿using API.BLL.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Specifications
{
    public class OrdersWithItemAndOrderingSpecification : BaseSpecifications<Order>
    {
        public OrdersWithItemAndOrderingSpecification(string email) : base(o=>o.BuyerEmail==email)
        {
            AddInclude(o=>o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescinding(o=>o.OrderDate);

        }

        public OrdersWithItemAndOrderingSpecification(int id,string email) : base(o =>o.Id==id&& o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            

        }
    }
}
