namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Data.MongoDb;
    using FurnitureFactory.Data;
    using FurnitureFactory.Data.MySql;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            //// var db = new SalesDbContext();

            var db = new FurnitureFactoryDbContext();

            var databaseName = "furnitures";

            db.Database.Delete();
            db.Database.Create();
            db.SaveChanges();

            ConsoleUserInterfaceIO io = new ConsoleUserInterfaceIO();
            var mongodata = new MongoDbData(databaseName, io);
            mongodata.Import(db);

            var furnituresForBedroom = db.Products
                .Where(x => x.RoomId == 1)
                .Select(x => x.Series.Name)
                .ToList();

            // Output must be: ALVIS \n  396    Tests, huh? :D
            io.SetOutput(furnituresForBedroom.First());
            io.SetOutput(db.Products.Count());
        }
    }
}
