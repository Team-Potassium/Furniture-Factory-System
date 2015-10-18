﻿namespace FurnitureFactory.Data.MongoDb
{
    using System.Data.Entity;
    using Logic;

    // TODO: Singleton
    public class MongoDbData
    {
        public MongoDbData(string dbName, UserInterfaceHandlerIO io)
        {
            this.Connection = new MongoDbConnection();
            this.Connection.Connect(dbName, io);
            this.Connection.Drop(io);
            this.Seeder = new MongoDataSeeder(dbName, this.Connection.Database, io);
            this.Seeder.Seed();
        }

        public MongoDbConnection Connection { get; set; }

        public MongoDataSeeder Seeder { get; set; }

        public MongoDbDataImporter Importer { get; set; }

        public void Import(DbContext db)
        {
            this.Importer = MongoDbDataImporter.GetInstance(db, this.Connection.Database);
            this.Importer.ImportSeries();
            this.Importer.ImportRooms();
            this.Importer.ImportFurnitureTypes();
            this.Importer.ImportProducts();
        }
    }
}
