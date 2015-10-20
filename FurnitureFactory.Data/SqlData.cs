namespace FurnitureFactory.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class SqlLocalRepository<T> where T : class
    {
        private FurnitureFactoryDbContext context;

        public SqlLocalRepository()
        {
            this.context = new FurnitureFactoryDbContext();
        }

        public void Add(T entity)
        {
            this.context.Set<T>().AddOrUpdate(entity);
        }

        public IQueryable<T> All()
        {
            return this.context.Set<T>().AsQueryable();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
