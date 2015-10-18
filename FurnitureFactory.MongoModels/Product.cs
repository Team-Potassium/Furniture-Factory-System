namespace FurnitureFactory.MongoModels
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product : IMongoModel
    {
        public Product(Room room, FurnitureType type, Series series, decimal price, int manufacturingTime, double weight)
        {
            this.Id = ObjectId.GenerateNewId().ToString();
            this.Room = room;
            this.FurnitureType = type;
            this.Series = series;
            this.Price = price;
            this.ManufacturingLeadTime = manufacturingTime;
            this.Weight = weight;
        }

        [BsonElement("_id")]
        public string Id { get; set; }

        public Room Room { get; set; }

        public FurnitureType FurnitureType { get; set; }

        public Series Series { get; set; }

        public decimal Price { get; set; }

        public int ManufacturingLeadTime { get; set; }

        public double Weight { get; set; }
    }
}
