namespace FurnitureFactory.Data.MongoDb
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Logic;
    using MongoDB.Driver;

    public class MongoDbDataImporter : IImporter
    {
        private static MongoDbDataImporter instance = null;
        private int nextFreeCatalogNumber = 1000;

        private MongoDbDataImporter(DbContext db, MongoDatabase source)
        {
            this.Db = db;
            this.MongoDataManager = MongoDataManager.GetInstance(source);
        }

        public DbContext Db { get; set; }

        public MongoDataManager MongoDataManager { get; set; }

        public static MongoDbDataImporter GetInstance(DbContext db, MongoDatabase source)
        {
            if (instance == null)
            {
                instance = new MongoDbDataImporter(db, source);
            }

            return instance;
        }

        public void ImportSeries()
        {
            var mongoSeries = this.MongoDataManager.GetCollectionAsList<MongoModels.Series>("series");

            foreach (var series in mongoSeries)
            {
                var seriesEntity = new Models.Series
                {
                    Name = series.Name
                };
                this.Db.Set<Models.Series>().AddOrUpdate(x => x.Name, seriesEntity);
            }

            this.Db.SaveChanges();
        }

        public void ImportRooms()
        {
            var mongoRooms = this.MongoDataManager.GetCollectionAsList<MongoModels.Room>("rooms");

            foreach (var room in mongoRooms)
            {
                var roomEntity = new Models.Room
                {
                    Name = room.Name
                };

                this.Db.Set<Models.Room>().AddOrUpdate(x => x.Name, roomEntity);
            }

            this.Db.SaveChanges();
        }

        public void ImportFurnitureTypes()
        {
            var types = this.MongoDataManager.GetCollectionAsList<MongoModels.FurnitureType>("types");

            foreach (var type in types)
            {
                var typeEntity = new Models.FurnitureType
                {
                    Name = type.Name
                };

                this.Db.Set<Models.FurnitureType>().AddOrUpdate(x => x.Name, typeEntity);
            }

            this.Db.SaveChanges();
        }

        public void ImportProducts()
        {
            var products = this.MongoDataManager.GetCollectionAsList<MongoModels.Product>("products");

            foreach (var product in products)
            {
                var type = this.Db.Set<Models.FurnitureType>()
                    .First(x => x.Name == product.FurnitureType.Name);

                var room = this.Db.Set<Models.Room>()
                    .First(x => x.Name == product.Room.Name);

                var series = this.Db.Set<Models.Series>()
                    .First(x => x.Name == product.Series.Name);

                var productEntity = new Models.Product
                {
                    FurnitureType = type,
                    Room = room,
                    Series = series,
                    ProductionExpense = product.Price,
                    ProductionTime = product.ManufacturingLeadTime,
                    Weight = product.Weight,
                    CatalogNumber = product.Series.Name.Substring(0, 1) + this.nextFreeCatalogNumber
                };

                this.nextFreeCatalogNumber++;

                this.Db.Set<Models.Product>().AddOrUpdate(x => x.CatalogNumber, productEntity);
            }

            this.Db.SaveChanges();
        }
    }
}
