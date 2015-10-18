namespace FurnitureFactory.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class Room
    {
        private ICollection<Product> products;

        public Room()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
