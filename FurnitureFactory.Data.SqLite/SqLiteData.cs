namespace FurnitureFactory.Data.SqLite
{
    using System.Collections;
    using System.Collections.Generic;

    public class  SqLiteData
    {
        private SqLiteDbContext context;

        public SqLiteData()
        {
            this.context = new SqLiteDbContext();
          //  SqLiteSeeder.Seed(this.context);
        }

        public IEnumerable<Client> GetAll()
        {
           return this.context.Clients;
        }
    }
}
