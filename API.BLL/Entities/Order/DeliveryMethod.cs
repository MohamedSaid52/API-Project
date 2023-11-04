﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Entities.Order
{
    public class DeliveryMethod : BaseEntity
    {
      public int id { get; set; }
      public  string ShortName { get; set; }
      public string Description { get; set; }
      public string DeliveryTime { get; set; }
      public decimal Price { get; set; }
    }
}
