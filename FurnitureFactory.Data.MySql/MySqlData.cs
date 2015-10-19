namespace FurnitureFactory.Data.MySql
{
    using System.Collections.Generic;
    using Models;
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Config.Sql;

    public class MySqlData
    {
        private MySqlLocalRepository repository;
        private const string ConnectionString = "server=localhost;database=furnitures;uid=root;pwd={0};";
        private readonly SalesDbContext context;

        public MySqlData(string password)
        {
            this.context = new SalesDbContext(string.Format(ConnectionString, password));
            this.repository = new MySqlLocalRepository(this.context);
        }

        private void VerifyDatabase()
        {
            var schemaHandler = context.GetSchemaHandler();
            this.EnsureDB(schemaHandler);
        }

        private void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script;

            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

        public void Import(IEnumerable<SalesTotalCostReport> entities)
        {
            this.repository.AddRange(entities);
        }

        public IEnumerable<SalesTotalCostReport> Export()
        {
            return this.repository.GetEntities();
        }
    }
}
