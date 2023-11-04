﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "pending")]
        pending,
        [EnumMember(Value = "paymentrecived")]
        paymentrecived,
        [EnumMember(Value = "paymentfailed")]
        paymentfailed

    }
}
