﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto_s.BasketDto
{
    public class BasketItemDto
    {
        public int id { get; set; }

        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        [Range(1, double.MaxValue)]
        public string Price { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
