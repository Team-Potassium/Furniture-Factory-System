namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using Data.Manager.Exporters;
    using Data.Manager.Importers;
    using FileSystemUtils.FileLoaders;
    using FurnitureFactory.Data;
    using FurnitureFactory.Logic.DataImporters;
    using FurnitureFactory.Logic.Exporters;
    using Data.MongoDb;
    using Data.MySql;
    using Data.Reports;
    using Logic;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            var db = new FurnitureFactoryDbContext();
            Logic.IUserInterfaceHandlerIO io = new ConsoleUserInterfaceIO();

            db.Database.Delete();
            db.Database.Create();

            var mongodata = new MongoDbData(DatabaseName, io);
            mongodata.Import(db);
            // Load excel from zip - Task1
            LoadSalesReports();

            new MaterialsXmlImporter().Import();
            new ProductionDetailsXmlImporter().Import();
            new RoomsXmlMongoImporter().Import(io);

            PdfExporter pdfExporter = new PdfExporter(db);
            pdfExporter.GeneratePdf();

            var furnituresForBedroom = db.Products
                .Where(x => x.RoomId == 1)
                .Select(x => x.Series.Name)
                .ToList();

            //// Task 4.1
            var jsonReporter = new JsonProductsReporter(db);
            jsonReporter.GetJsonReport().Load();

            //// Task 4.2
            //var mySqlImporter = new SalesReportsMySqlImporter(io);
            //mySqlImporter.Save();

            //// Task 3. Generate Xml Report in Xml-Exports folder
            var productXmlReport = new ProductsXmlFileExporter(db);
            productXmlReport.GetXmlReport();

            var ordersXmlReport = new OrdersXmlFileExporter(db);
            ordersXmlReport.GetXmlReport();
        }

        private static void LoadSalesReports()
        {
            string sourceArchiveFilePath = @"..\..\..\Sales-Reports.zip";

            var salesReportLoader = new SalesReportsImporter();

            var excelFileLoader = new ExcelFileLoader();
            excelFileLoader.AddDataLoader(salesReportLoader);

            var zipArchiveLoader = new ZipArchiveLoader();
            zipArchiveLoader.AddFileLoader(excelFileLoader);

            zipArchiveLoader.Load(sourceArchiveFilePath);
        }
    }
}
