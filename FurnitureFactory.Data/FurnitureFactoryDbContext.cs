namespace FurnitureFactory.Data
{
    using System.Data.Entity;
    using FurnitureFactory.Models;
    using Migrations;

    public class FurnitureFactoryDbContext : DbContext
    {
        public FurnitureFactoryDbContext()
            : base("FurnitureFactoryConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FurnitureFactoryDbContext, Configuration>());
        }

        public virtual IDbSet<Room> Rooms { get; set; }

        public virtual IDbSet<FurnitureType> FurnitureTypes { get; set; }

        public virtual IDbSet<Series> Series { get; set; }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Material> Materials { get; set; }

        public virtual IDbSet<Client> Clients { get; set; }

        public virtual IDbSet<Order> Orders { get; set; }

        public virtual IDbSet<ProductsMaterialsQuantity> ProductsMaterialsQuantities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOptional(x => x.FurnitureType)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
               .HasOptional(x => x.Series)
               .WithMany()
               .WillCascadeOnDelete(false);

             modelBuilder.Entity<Product>()
                .HasOptional(x => x.Room)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
