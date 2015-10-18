namespace FurnitureFactory.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public int Id { get; set; }

        public int? FurnitureTypeId { get; set; }

        [ForeignKey("FurnitureTypeId")]
        public virtual FurnitureType FurnitureType { get; set; }

        public int? RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

        public int? SeriesId { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }

        public decimal ProductionExpense { get; set; }

        // TODO: Change to appropriate type if have time
        public int ProductionTime { get; set; }

        public double Weight { get; set; }

        [Required]
        [MaxLength(10)]
        public string CatalogNumber { get; set; }
    }
}
