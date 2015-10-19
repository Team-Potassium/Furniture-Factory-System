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

        public MySqlLocalRepository()
            : this(new SalesDbContext())
        {
        }

        public void Add<T>(T entity)
            where T : class
        {
            this.context.Set<Client>().Add(entity as Client);
        }

        public void AddRange<T>(IEnumerable<T> collection)
        {
            this.context.Set<Client>().AddRange(collection as IQueryable<Client>);
        }

        // Because why not 
        //public IEnumerable<T> GetEntities<T>()
        //{
        //    return (dynamic)this.context.Set<Series>();
        //}

        public IQueryable<Client> GetEntities()
        {
            return this.context.Set<Client>();
        } 

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
