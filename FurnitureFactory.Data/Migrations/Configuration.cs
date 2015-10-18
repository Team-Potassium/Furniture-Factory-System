namespace FurnitureFactory.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FurnitureFactoryDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "FurnitureFactory.Data.FurnitureFactoryDbContext";
         }

        protected override void Seed(FurnitureFactoryDbContext context)
        {
        }
    }
}
