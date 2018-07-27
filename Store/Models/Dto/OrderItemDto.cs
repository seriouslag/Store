using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models.Dto
{
    public class OrderItemDto
    {
        public int ProductOptionId { get; set; }
        public int Quantity { get; set; }
    }
}