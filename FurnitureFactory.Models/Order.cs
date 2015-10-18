namespace FurnitureFactory.Models
{
    using System;

    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool PaymentStatus { get; set; }
    }
}
