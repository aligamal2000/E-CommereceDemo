﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.OrderDto
{
    public class DelvieryMethodDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; }=null!;
        public string Description { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;  
        public decimal Price { get; set; }

    }
}
