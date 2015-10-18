namespace FurnitureFactory.Data.MongoDb
{
    using System;
    using Logic;
    using MongoDB.Driver;

    public class MongoDbConnection
    {
        private const string ConnectionStringLocal = "mongodb://127.0.0.1";
        private const string ConnectionString = @"mongodb://{0}:{1}@ds033754.mongolab.com:33754/furnitures";
        private const string ENV_VAR_TEST = "Testing";
        private const string ENV_VAR_PRODUCTION = "Production";
        private readonly string[] ENV_VAR = { ENV_VAR_TEST, ENV_VAR_PRODUCTION };

        public MongoDatabase Database { get; internal set; }

        internal MongoClient Client { get; private set; }

        public void Connect(string dbName, UserInterfaceHandlerIO io)
        {
            var env = this.ConfigEnvironment(io);

            if (env == ENV_VAR_TEST)
            {
                // TODO: Extract connection string and server in something handling Environment
                this.Client = new MongoClient(ConnectionStringLocal);
                var server = this.Client.GetServer();
                this.Database = server.GetDatabase(dbName);
            }
            else if (env == ENV_VAR_PRODUCTION)
            {
                io.SetOutput("Username: ");
                var username = io.GetInput().Trim();
                io.SetOutput("Password: ");
                Console.ForegroundColor = ConsoleColor.Black;
                var password = io.GetInput().Trim();
                Console.ForegroundColor = ConsoleColor.White;
                this.Client = new MongoClient(string.Format(ConnectionString, username, password, dbName));

                var server = this.Client.GetServer();
                this.Database = server.GetDatabase(dbName);
            }
            else
            {
                throw new ArgumentException("Invalid environment.");
            }
        }

        public void Drop(UserInterfaceHandlerIO io)
        {
            io.SetOutput("Drop database?");
            io.SetOutput("[Y]es / [N]o     :");
            var input = io.GetInput();

            if (input == "Y")
            {
                this.Database.Drop();
            }
        }

        private string ConfigEnvironment(UserInterfaceHandlerIO io)
        {
            io.SetOutput("ENVIRONMENT: ");
            io.SetOutput("[0] -> Testing");
            io.SetOutput("[1] -> Production");
            var env = int.Parse(io.GetInput());
            return this.ENV_VAR[env];
        }
    }
}
