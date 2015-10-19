namespace FurnitureFactory.Data.MySql
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using FurnitureFactory.Models;
    using global::MySql.Data.Entity;
    using Models;
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;

    public partial class SalesDbContext : OpenAccessContext
    {
        private static string connectionStringName = @"SalesConnection";

        private static BackendConfiguration backend =
            GetBackendConfiguration();

        private static MetadataSource metadataSource =
            new SalesModelConfiguration();

        public SalesDbContext(string connectionStringName)
            : base(connectionStringName, backend, metadataSource)
        { }

      
        public IQueryable<SalesTotalCostReport> Sales
        {
            get
            {
                return this.GetAll<SalesTotalCostReport>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MsSql";
            backend.ProviderName = "System.Data.SqlClient";

            return backend;
        }
    }
}