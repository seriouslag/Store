using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models.Dto
{
    public class ChargeDtos
    {
        public int Amount { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}