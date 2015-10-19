namespace FurnitureFactory.ConsoleClient
{
    using System.Linq;
    using Data.MongoDb;
    using FileSystemUtils.FileLoaders;
    using FurnitureFactory.Data;
    using FurnitureFactory.Logic.DataLoaders;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            //var context = new FurnitureFactoryDbContext();
            //context.Database.Delete();
            //context.Database.Create();
            //context.SaveChanges();

            //ConsoleUserInterfaceIO io = new ConsoleUserInterfaceIO();
            //var mongodata = new MongoDbData(DatabaseName, io);
            //mongodata.Import(context);
            //io.SetOutput(context.Products.Count());

            // Load excel from zip - Task1
            LoadSalesReports();
        }

        private static void LoadSalesReports()
        {
            string sourceArchiveFilePath = @"..\..\..\Sales-Reports.zip";

            var salesReportLoader = new SalesReportsDataLoader();

            var excelFileLoader = new ExcelFileLoader();
            excelFileLoader.AddDataLoader(salesReportLoader);

            var zipArchiveLoader = new ZipArchiveLoader();
            zipArchiveLoader.AddFileLoader(excelFileLoader);

            zipArchiveLoader.Load(sourceArchiveFilePath);
        }
    }
}
