namespace FurnitureFactory.Logic.DataImporters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FileSystemUtils.Contracts;
    using FurnitureFactory.Data;
    using FurnitureFactory.Models;

    public class SalesReportsImporter : IDataImporter
    {
        private int clientId = -1;
        private FurnitureFactoryDbContext db = new FurnitureFactoryDbContext();
        private int databaseMonitoredOrders = 0;

        /// <summary>
        /// The method imports data to the sql server database. It checks whether such customer exists
        /// and adds it to customers DbSet if necessary otherwise gets its id to use for orders. 
        /// So you can`t add the same customer twice to Customers DbSet, say if you load two different xls reports from the same customer.
        /// But it does not check for duplication of orders.
        /// So the same orders can be added to Orders DbSet twice if we load the same xls report file twice from a particular Customer.
        /// </summary>
        /// <param name="data"></param>
        public void ImportData(IList<object> data)
        {
            // means we start a new file (assuming first row is always empty
            if (data[0] == null) 
            {
                // we reset the products client for the new file
                this.clientId = -1; 
                return;
            }
            else if (this.clientId == -1)
            {
                var client = new Client()
                {
                    Name = data[0].ToString(),
                };

                if (!this.db.Clients.Any(x => x.Name == client.Name))
                {
                    this.db.Clients.Add(client);
                    this.db.SaveChanges();
                }

                // we set the current client Id for the current sales reports file`s products
                this.clientId = this.db.Clients.First(x => x.Name == client.Name).Id;
                return;
            }
            else if (data[0].ToString().Contains("ProductID"))
            {
                return;
            }

            var order = new Order()
            {
                ProductId = int.Parse(data[0].ToString()),
                ClientId = this.clientId,
                Quantity = int.Parse(data[1].ToString()),
                Price = decimal.Parse(data[3].ToString()),
                DeliveryDate = DateTime.Now
            };

            this.db.Orders.Add(order);
            this.databaseMonitoredOrders++;

            // optimize dbContext for large data import
            if (this.databaseMonitoredOrders >= 10)
            {
                this.db.SaveChanges();
                this.db = new FurnitureFactoryDbContext();
                this.databaseMonitoredOrders = 0;
            }
        }

        public void FinalizeImporting()
        {
            this.db.SaveChanges();
            this.db = new FurnitureFactoryDbContext();
        }
    }
}