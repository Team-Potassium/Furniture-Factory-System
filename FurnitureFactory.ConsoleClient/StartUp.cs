namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Data.MongoDb;
    using FurnitureFactory.Data;
    using FurnitureFactory.Data.Json;
    using FurnitureFactory.Data.MySql;
    using FurnitureFactory.Logic.Exporters;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            //// var db = new SalesDbContext();

            var db = new FurnitureFactoryDbContext();
            ConsoleUserInterfaceIO io = new ConsoleUserInterfaceIO();

            //db.Database.Delete();
            //db.Database.Create();

            //var mongodata = new MongoDbData(DatabaseName, io);
            //mongodata.Import(db);

            //PdfExporter pdfExporter = new PdfExporter(db);
            //pdfExporter.GeneratePdf();

            var furnituresForBedroom = db.Products
                .Where(x => x.RoomId == 1)
                .Select(x => x.Series.Name)
                .ToList();

            //// Output must be: ALVIS \n  396    Tests, huh? :D

            io.SetOutput(furnituresForBedroom.FirstOrDefault());
            io.SetOutput(db.Products.Count());

            var jsonReporter = new JsonProductsReporter(db);
            jsonReporter.GetJsonReport();

        }
    }
}
