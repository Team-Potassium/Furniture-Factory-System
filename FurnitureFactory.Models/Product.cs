namespace FurnitureFactory.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int? FurnitureTypeId { get; set; }

        public int? RoomId { get; set; }

        public int? SeriesId { get; set; }

        public decimal ProductionExpense { get; set; }

        // TODO: Change to appropriate type if have time
        public int ProductionTime { get; set; }

        public double Weight { get; set; }

        public string CatalogNumber { get; set; }

        public virtual FurnitureType FurnitureType { get; set; }

        public virtual Room Room { get; set; }

        public virtual Series Series { get; set; }
    }
}
