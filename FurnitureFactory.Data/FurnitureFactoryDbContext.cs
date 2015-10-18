namespace FurnitureFactory.Data
{
    using System.Data.Entity;
    using FurnitureFactory.Models;

    public class FurnitureFactoryDbContext : DbContext
    {
        public FurnitureFactoryDbContext()
            : base("FurnitureFactoryConnection")
        {
            // Just good to know: It's not possible to use DropCreate and migrations together. 
            Database.SetInitializer(new DropCreateDatabaseAlways<FurnitureFactoryDbContext>());
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Room> Rooms { get; set; }

        public virtual IDbSet<FurnitureType> FurnitureTypes { get; set; }

        public virtual IDbSet<Series> Series { get; set; }

        public virtual IDbSet<Material> Materials { get; set; }

        public virtual IDbSet<Client> Clients { get; set; }

        public virtual IDbSet<Order> Orders { get; set; }

        public virtual IDbSet<ProductsMaterialsQuantity> ProductsMaterialsQuantities { get; set; }
    }
}
