using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace Store.Models
{
    public class Order : BaseObject
    {
        public ICollection<OrderItem> OrderItems { get; set; }

        public Order(OrderItem orderItem)
        {
            OrderItems = new List<OrderItem>
            {
                orderItem
            };
        }

        public Order(ICollection<OrderItem> orderItems)
        {
            OrderItems = orderItems;
        }

        public User User { get; set; }
        public int UserId { get; set; }

        public void UpdateOrder(OrderItem orderItem)
        {
            OrderItems.Clear();
            OrderItems.Add(orderItem);
            UpdateModifiedDate();
        }

        public void UpdateOrder(ICollection<OrderItem> orderItems)
        {
            OrderItems.Clear();
            orderItems.CopyItemsTo(OrderItems);
            UpdateModifiedDate();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            if (!IsOrderItemInOrder(orderItem)) return;

            RemoveOrderItemFromOrder(orderItem);
            if (orderItem.Quantity > 0)
            {
                OrderItems.Add(orderItem);
            }
            UpdateModifiedDate();
        }

        public void UpdateOrderItems(ICollection<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                UpdateOrderItem(orderItem);
            }
        }

        public bool IsOrderItemInOrder(OrderItem orderItem)
        {
            return OrderItems.Any(oi => oi.ProductOption.Sku == orderItem.ProductOption.Sku);
        }

        public bool IsOrderItemSkuInOrder(string sku)
        {
            return OrderItems.Any(oi => string.Equals(oi.ProductOption.Sku.ToUpper(), sku.ToUpper()));
        }

        public void RemoveOrderItemFromOrder(OrderItem orderItem)
        {
            orderItem.UpdateQuanity(0);
            UpdateOrderItem(orderItem);
        }

        public void AddOrderItemToOrder(OrderItem orderItem)
        {
            UpdateOrderItem(orderItem);
        }

        public void AddOrderItemsToOrder(ICollection<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                AddOrderItemToOrder(orderItem);
            }
        }

        public void UpdateOrderItemQuantityBySku(string sku, int quantity)
        {
            var orderItem = GetOrderItemBySku(sku);

            if (orderItem == null) return;

            if (quantity <= 0)
            {
                RemoveOrderItemFromOrder(orderItem);
                return;
            }

            orderItem.UpdateQuanity(quantity);
            UpdateOrderItem(orderItem);
        }

        public OrderItem GetOrderItemBySku(string sku) => OrderItems.First(oi => string.Equals(oi.ProductOption.Sku.ToUpper(), sku.ToUpper()));

    }
}