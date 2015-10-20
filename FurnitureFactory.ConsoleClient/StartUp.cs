namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using FileSystemUtils.FileLoaders;
    using FurnitureFactory.Data;
    using FurnitureFactory.Data.Json;
    using FurnitureFactory.Data.Xml.Exporters;
    using FurnitureFactory.Data.Xml.Importers;
    using FurnitureFactory.Logic.DataImporters;
    using FurnitureFactory.Logic.Exporters;
    using Data.MongoDb;
    using Data.MySql;
    using Logic;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            var db = new FurnitureFactoryDbContext();
            ConsoleUserInterfaceIO io = new ConsoleUserInterfaceIO();

            //db.Database.Delete();
            //db.Database.Create();

            //var mongodata = new MongoDbData(DatabaseName, io);
            //mongodata.Import(db);
            //// Load excel from zip - Task1
            //LoadSalesReports();

            //new MaterialsXmlImporter().Import();
            //new ProductionDetailsXmlImporter().Import();

            PdfExporter pdfExporter = new PdfExporter(db);
            pdfExporter.GeneratePdf();

            var furnituresForBedroom = db.Products
                .Where(x => x.RoomId == 1)
                .Select(x => x.Series.Name)
                .ToList();

            // Output must be: ALVIS \n  396    Tests, huh? :D

            //io.SetOutput(furnituresForBedroom.FirstOrDefault());
            //io.SetOutput(db.Products.Count());

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

            //// import XML file
            //var importProducts = new ProductsXmlFileImporter();
            //importProducts.ImportXmlData("../../../Xml-Data.xml");



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
