namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using Data.MongoDb;
    using FurnitureFactory.Data;
    using FurnitureFactory.Logic.Exporters;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            var context = new FurnitureFactoryDbContext();

            PdfExporter pdfExporter= new PdfExporter(context);
            pdfExporter.GeneratePdf();

            context.Database.Delete();
            context.Database.Create();
            context.SaveChanges();

            ConsoleUserInterfaceIO io = new ConsoleUserInterfaceIO();
            var mongodata = new MongoDbData(DatabaseName, io);
            mongodata.Import(context);
            io.SetOutput(context.Products.Count());
        }
    }
}
