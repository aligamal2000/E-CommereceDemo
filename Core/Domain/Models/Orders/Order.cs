using System;
using System.Collections.Generic;
using Domain.Models.Products;

namespace Domain.Models.Orders
{
    public class Order : ModelBase<Guid>
    {
        // Parameterless constructor for EF Core
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        // Optional constructor for manual object creation
        public Order(string userEmail, decimal subtotal, OrderAddress shipToAddress, DeliveryMethod deliveryMethod)
        {
            UserEmail = userEmail;
            Subtotal = subtotal;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = new List<OrderItem>(); // Initialize as needed
            OrderStatus = OrderStatus.Pending;
            OrderDate = DateTimeOffset.Now;
        }

        public string UserEmail { get; set; } = default!;
        public decimal Subtotal { get; set; }
        public OrderAddress ShipToAddress { get; set; } = default!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public decimal GetTotal()
        {
            if (DeliveryMethod == null)
                throw new InvalidOperationException("DeliveryMethod is not set.");

            return Subtotal + DeliveryMethod.Price;
        }
    }
}
