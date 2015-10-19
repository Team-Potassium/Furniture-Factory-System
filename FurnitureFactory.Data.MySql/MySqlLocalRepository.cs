namespace FurnitureFactory.Data.MySql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class MySqlLocalRepository
    {
        private SalesDbContext context;

        public MySqlLocalRepository(SalesDbContext context)
        {
            this.context = context;
        }

      public void Add(SalesTotalCostReport entity)
        {
            this.context.Add(entity);
        }

        public void AddRange(IEnumerable<SalesTotalCostReport> collection)
        {
            this.context.Add(collection);
        }

        // Because why not 
        //public IEnumerable<T> GetEntities<T>()
        //{
        //    return (dynamic)this.context.Set<Series>();
        //}

        public IQueryable<SalesTotalCostReport> GetEntities()
        {
            return this.context.Sales;
        }

        public void DeleteAll()
        {
            this.context.Delete(this.context.Sales);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
