namespace FurnitureFactory.Data.MongoDb
{
    using System.Collections.Generic;
    using Logic;
    using MongoDB.Driver;
    using MongoModels;

    public class MongoDataSeeder
    {
        public MongoDataSeeder(string databaseName, MongoDatabase database, IUserInterfaceHandlerIO io)
        {
            this.DataSource = new MongoDataRaw();
            this.DataGenerator = new MongoDataGenerator(this.DataSource);
            this.DataManager = MongoDataManager.GetInstance(database);
            this.Collections = new Dictionary<string, IEnumerable<IMongoModel>>();
        }

        public MongoDataRaw DataSource { get; set; }

        public MongoDataGenerator DataGenerator { get; set; }

        public IDictionary<string, IEnumerable<IMongoModel>> Collections { get; set; }

        public MongoDataManager DataManager { get; set; }

        public void Seed()
        {
            this.GenerateMongoCollections();
            this.InsertMongoCollections();
        }

        public void GenerateMongoCollections()
        {
            List<Room> rooms = this.DataGenerator.GetRooms();
            List<FurnitureType> types = this.DataGenerator.GetTypes();
            List<Series> series = this.DataGenerator.GetSeries();
            List<Product> products = this.DataGenerator.GetAllProducts(rooms, types, series);

            this.Collections.Add(new KeyValuePair<string, IEnumerable<IMongoModel>>("rooms", rooms));
            this.Collections.Add(new KeyValuePair<string, IEnumerable<IMongoModel>>("types", types));
            this.Collections.Add(new KeyValuePair<string, IEnumerable<IMongoModel>>("series", series));
            this.Collections.Add(new KeyValuePair<string, IEnumerable<IMongoModel>>("products", products));
        }

        public void InsertMongoCollections()
        {
            foreach (var collection in this.Collections)
            {
                this.DataManager
                    .SeedCollection<IMongoModel>(collection.Key, collection.Value);
            }
        }

        // TODO: Not here.
        public MongoCollection<T> GetMongoCollection<T>(string collectionName)
        {
            return this.DataManager.GetCollection<T>(collectionName);
        }
    }
}
