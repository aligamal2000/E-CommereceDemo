﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Orders
{
    public class OrderItem:ModelBase<int>

    {
        public ProductItemOrdered Product { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }  

    }
}
