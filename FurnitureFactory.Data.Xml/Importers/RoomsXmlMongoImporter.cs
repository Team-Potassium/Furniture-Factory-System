namespace FurnitureFactory.Data.Manager.Importers
{
    using System;
    using System.Collections.Generic;
    using MongoDb;
    using System.Xml.Linq;
    using Logic;
    using MongoModels;

    public class RoomsXmlMongoImporter
    {
        private const string documentPath = "../../../Resources/new-rooms.xml";
        private MongoDbConnection mongoContext;
        private ICollection<MongoModels.Room> rooms;
        public RoomsXmlMongoImporter()
        {
            this.mongoContext = new MongoDbConnection();
           this.rooms = new List<MongoModels.Room>();
        }

        public void Import(IUserInterfaceHandlerIO io)
        {
            this.mongoContext.Connect("furnitures",  io);
            XDocument xmlDoc = XDocument.Load(documentPath);
            var descendants = xmlDoc.Descendants("room");

            Console.WriteLine("Seeding materials quantity from XML to SQL");

            foreach (var node in descendants)
            {
                var item = new MongoModels.Room(node.Element("name").Value.ToString());
                this.rooms.Add(item);
                Console.Write(".");
            }
          
            var rooms = this.mongoContext.Database.GetCollection<MongoModels.Room>("rooms");
            rooms.InsertBatch(this.rooms);

        }
    }
}
