namespace FurnitureFactory.Data.SqLite
{
    using System.Data.Entity;

    public class SqLiteDbContext : DbContext
    {
        public SqLiteDbContext()
            : base("SqLiteConnectionString")
        {
            this.Database.CreateIfNotExists();
        }

        public IDbSet<Client> Clients { get; set; }
    }
}
