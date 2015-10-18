namespace FurnitureFactory.Data.MySql
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using FurnitureFactory.Models;
    using global::MySql.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SalesDbContext : DbContext
    {
        public SalesDbContext()
            : base("SalesConnection")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
    }
}