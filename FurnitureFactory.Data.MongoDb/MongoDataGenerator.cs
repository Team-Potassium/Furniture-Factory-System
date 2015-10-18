namespace FurnitureFactory.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FurnitureFactory.MongoModels;

    public class MongoDataGenerator
    {
        private static Random randomGenerator = new Random();

        public MongoDataGenerator(MongoDataRaw rawData)
        {
            this.RawData = rawData;
        }

        public MongoDataRaw RawData { get; private set; }

        public List<Series> GetSeries()
        {
            return this.RawData.GetSeriesNames()
                .Select(s => new Series(s.ToUpper()))
                .ToList();
        }

        public List<FurnitureType> GetTypes()
        {
            return this.RawData.GetAllTypeNames()
                .Select(t => new FurnitureType(t))
                .ToList();
        }

        public List<Room> GetRooms()
        {
            return this.RawData.GetRoomNames()
                .Select(r => new Room(r))
                .ToList();
        }

        public List<Product> GetAllProducts(List<Room> rooms, List<FurnitureType> types, List<Series> series)
        {
            var products = new List<Product>();

            this.RawData.GetBedroomTypeNames()
               .Select(type => this.GetNewProductsSeries(rooms, types, series, "Bedroom", type))
               .ToList()
               .ForEach(x => products.AddRange(x));

            this.RawData.GetLivingRoomTypeNames()
                .Select(type => this.GetNewProductsSeries(rooms, types, series, "Living room", type))
                .ToList()
                .ForEach(x => products.AddRange(x));

            return products;
        }

        private List<Product> GetNewProductsSeries(List<Room> rooms, List<FurnitureType> types, List<Series> series, string roomName, string typeName)
        {
            // TODO: i < rand.Next(), Select random series, Select random Count of the collection
            var room = rooms.First(x => x.Name == roomName);
            var type = types.First(x => x.Name == typeName);

            return series
                .Select(s => new Product(room, type, s, randomGenerator.Next(30, 400), randomGenerator.Next(3, 20), randomGenerator.Next(20, 100)))
                .ToList();
        }
    }
}
