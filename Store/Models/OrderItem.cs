using System;
using Store.Models.Exceptions;

namespace Store.Models
{
    public class OrderItem : BaseObject
    {
        public ProductOption ProductOption { get; }
        public int Quantity { get; private set; }
        public bool IsValid => IsValidOrderItemQuanity(Quantity);

        public OrderItem(ProductOption productOption, int quantity)
        {
            ProductOption = productOption;
            UpdateQuanity(quantity);
        }

        public Order Order { get; set; }
        public int OrderId { get; set; }

        public void UpdateQuanity(int quantity) => Quantity = IsValidOrderItemQuanity(quantity) ? quantity : throw new OrderItemQuanityLessThanZeroException();

        public bool IsValidOrderItemQuanity(int quantity) => quantity > 0;
    }
}