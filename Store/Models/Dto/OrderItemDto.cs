using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models.Dto
{
    public class OrderItemDto
    {
        public ProductOptionDto ProductOption { get; set; }
        public int Quantity { get; set; }
    }
}