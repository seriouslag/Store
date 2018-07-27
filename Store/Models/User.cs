using System.Collections.Generic;

namespace Store.Models
{
    public class User : BaseObject
    {
        public string Name { get; set; }
        public int StripeId { get; set; }

        public User(string name = "")
        {
            Name = name;
        }

        public User(int stripeId, string name = "")
        {
            StripeId = stripeId;
            Name = name;
        }

        public ICollection<Order> Orders;
    }
}