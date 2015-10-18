namespace FurnitureFactory.Data.MongoDb
{
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Driver;

    public class MongoDataManager
    {
        private static MongoDataManager instance = null;

        private MongoDataManager(MongoDatabase database)
        {
            this.Database = database;
        }

        public MongoDatabase Database { get; set; }

        public static MongoDataManager GetInstance(MongoDatabase database)
        {
            if (instance == null)
            {
                instance = new MongoDataManager(database);
            }

            return instance;
        }

        public void SeedCollection<T>(string collectionName, IEnumerable<T> collectionItems)
        {
            MongoCollection<T> collection = this.Database.GetCollection<T>(collectionName);
            collection.InsertBatch(collectionItems);
        }

        public MongoCollection<T> GetCollection<T>(string collectionName)
        {
            return this.Database.GetCollection<T>(collectionName);
        }

        public IList<T> GetCollectionAsList<T>(string collectionName)
        {
            return this.Database
                 .GetCollection<T>(collectionName)
                 .FindAllAs<T>()
                 .ToList();
        }
    }
}
