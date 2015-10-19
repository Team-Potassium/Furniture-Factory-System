﻿namespace FurnitureFactory.Models
{
    public class ProductsMaterialsQuantity
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int MaterialId { get; set; }

        public virtual Material Material { get; set; }
    }
}
