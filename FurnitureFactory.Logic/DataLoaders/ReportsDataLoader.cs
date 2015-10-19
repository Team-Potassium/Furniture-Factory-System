namespace FurnitureFactory.Logic.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using FileSystemUtils;
    using FurnitureFactory.Data;
    using FurnitureFactory.Models;

    public class SalesReportsDataLoader : IDataLoader
    {
        private int? clientId = null;
        private FurnitureFactoryDbContext db = new FurnitureFactoryDbContext();

        public void LoadData(IList<object> data)
        {
            if (data[0] == null)
            {
                return;
            }
            else if (clientId == null)
            {
                var client = new Client()
                {
                    Name = data[0].ToString(),
                };

                if (!db.Clients.Any(x => x.Name == client.Name))
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                }

                this.clientId = client.Id;


                return;
            }

            // TODO: Add all sales for particular client, currently only the new clients are added if they don`t exist in the Clients table
        }
    }
}