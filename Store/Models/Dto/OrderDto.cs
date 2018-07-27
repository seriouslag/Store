﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}