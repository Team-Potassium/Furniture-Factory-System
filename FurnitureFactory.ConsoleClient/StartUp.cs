namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using Data.MongoDb;
    using FurnitureFactory.Data;
    using FurnitureFactory.Data.MySql;

    public class StartUp
    {
        public static void Main()
        {
            //// var db = new SalesDbContext();

            var db = new FurnitureFactoryDbContext();

            var databaseName = "furnitures";

            db.Database.Delete();
            db.Database.Create();
            db.SaveChanges();

            var io = new UserInterfaceIO();
            var mongodata = new MongoDbData(databaseName, io);
            mongodata.Import(db);

            io.SetOutput(db.Products.Count());
        }
    }
}
