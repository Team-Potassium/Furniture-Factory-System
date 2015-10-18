namespace FurnitureFactory.ConsoleClient
{
    using Data.MongoDb;
    using FurnitureFactory.Data;

    public class StartUp
    {
        private const string DatabaseName = "furnitures";

        public static void Main()
        {
            var context = new FurnitureFactoryDbContext();
            context.Database.Delete();
            context.Database.Create();
            context.SaveChanges();

            ConsoleUserInterfaceIO io = new ConsoleUserInterfaceIO();
            var mongodata = new MongoDbData(DatabaseName, io);
            mongodata.Import(context);
        }
    }
}
