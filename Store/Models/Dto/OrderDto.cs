using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models.Dto
{
    public class OrderDto
    {
        private ICollection<OrderItemDto> OrderItems { get; set; }

    }
}