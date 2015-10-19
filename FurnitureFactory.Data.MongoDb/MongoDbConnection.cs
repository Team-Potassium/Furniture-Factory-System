namespace FurnitureFactory.Data.MongoDb
{
    using System;
    using Logic;
    using MongoDB.Driver;

    public class MongoDbConnection
    {
        private const string ConnectionStringLocal = "mongodb://localhost:27017/furniture";
        private const string ConnectionString = @"mongodb://{0}:{1}@ds033754.mongolab.com:33754/furnitures";
        private const string EnvVarTest = "Testing";
        private const string EnvVarProduction = "Production";
        private readonly string[] envVar = { EnvVarTest, EnvVarProduction };

        public MongoDatabase Database { get; internal set; }

        internal MongoClient Client { get; private set; }

        public void Connect(string databaseName, IUserInterfaceHandlerIO io)
        {
            var env = this.ConfigEnvironment(io);

            if (env == EnvVarTest)
            {
                // TODO: Extract connection string and server in something handling Environment
                this.Client = new MongoClient(ConnectionStringLocal);
                var server = this.Client.GetServer();
                this.Database = server.GetDatabase(databaseName);
            }
            else if (env == EnvVarProduction)
            {
                io.SetOutput("Username: ");
                var username = io.GetInput().Trim();
                var password = io.GetPassword().Trim();
                this.Client = new MongoClient(string.Format(ConnectionString, username, password, databaseName));
                var server = this.Client.GetServer();
                this.Database = server.GetDatabase(databaseName);
            }
            else
            {
                throw new ArgumentException("Invalid environment.");
            }
        }

        public void Drop(IUserInterfaceHandlerIO io)
        {
            io.SetOutput("Drop database?");
            io.SetOutput("[Y]es / [N]o     :");
            var input = io.GetInput().Trim().ToUpper();

            if (input == "Y")
            {
                this.Database.Drop();
            }
        }

        private string ConfigEnvironment(IUserInterfaceHandlerIO io)
        {
            io.SetOutput("ENVIRONMENT: ");
            io.SetOutput("[0] -> Testing");
            io.SetOutput("[1] -> Production");
            var environment = int.Parse(io.GetInput());
            return this.envVar[environment];
        }
    }
}
