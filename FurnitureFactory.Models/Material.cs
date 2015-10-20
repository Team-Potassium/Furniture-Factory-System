namespace FurnitureFactory.Models
{
    public class Material
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PricePerUnit { get; set; }

        public string MeasurementUnit { get; set; }
    }
}
