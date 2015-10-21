namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using Data.Manager.Exporters;
    using Data.Manager.Importers;
    using FileSystemUtils.FileLoaders;
    using FurnitureFactory.Data;
    using Data.MongoDb;
    using Data.MySql;
    using Utils;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";
        private const string SourceSalesReportsArchiveFilePath = @"..\..\..\Resources\Sales-Reports.zip";

        public static void Main()
        {
            var db = new FurnitureFactoryDbContext();
            Utils.IUserInterfaceHandlerIO io = new ConsoleUserInterfaceIO();

            var mongodata = new MongoDbData(DatabaseName, io);
            mongodata.Import(db);

            // Task 1. Load excel from zip
            LoadSalesReports(SourceSalesReportsArchiveFilePath);

            new MaterialsXmlImporter().Import();
            new ProductionDetailsXmlImporter().Import();
            new RoomsXmlMongoImporter().Import(io);

            PdfExporter pdfExporter = new PdfExporter(db);
            pdfExporter.GeneratePdf();

            // Task 4.1
            var jsonReporter = new JsonProductsReporter(db);
            jsonReporter.GetJsonReport().Load();

            // Task 4.2
            //var mySqlImporter = new SalesReportsMySqlImporter(io);
            //mySqlImporter.Save();

            //// Task 3. Generate Xml Report in Xml-Exports folder
            var productXmlReport = new ProductsXmlFileExporter(db);
            productXmlReport.GetXmlReport();

            var ordersXmlReport = new OrdersXmlFileExporter(db);
            ordersXmlReport.GetXmlReport();

        }

        public static void LoadSalesReports(string sourceArchiveFilePath)
        {
            var salesReportImporter = new SalesReportsImporter();

            var excelFileLoader = new ExcelFileLoader();
            excelFileLoader.AddDataImporter(salesReportImporter);

            var zipArchiveLoader = new ZipArchiveLoader();
            zipArchiveLoader.AddFileLoader(excelFileLoader);

            zipArchiveLoader.Load(sourceArchiveFilePath);
        }
    }
}
