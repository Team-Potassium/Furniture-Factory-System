namespace FurnitureFactory.Data.MySql
{
    using System.Collections.Generic;
    using Models;

    public class MySqlData
    {
        private MySqlLocalRepository repository;

        public MySqlData(MySqlLocalRepository repository)
        {
            this.repository = repository;
        }

        public MySqlData()
            : this(new MySqlLocalRepository())
        {
        }

        public void Import(IEnumerable<SalesTotalCostReport> entities)
        {
            this.repository.AddRange(entities);
        }

        public IEnumerable<SalesTotalCostReport> Export()
        {
            return this.repository.GetEntities();
        }
    }
}
