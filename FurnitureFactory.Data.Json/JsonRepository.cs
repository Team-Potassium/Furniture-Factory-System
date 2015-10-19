namespace FurnitureFactory.Data.Json
{
    using System.Collections.Generic;
    using System.Linq;

    public class JsonRepository
    {
        private static JsonRepository instance;
        private Dictionary<string,object> reports; 

        private JsonRepository()
        {
            this.reports = new Dictionary<string, object>();
        }

        public static JsonRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new JsonRepository();
            }

            return instance;
        }

        public void AddReport(string key, object report)
        {
            this.reports.Add(key, report);
        }

        //public void AddReports(List<object> reports)
        //{
        //    this.reports.AddRange(reports);
        //}

        public Dictionary<string,object> GetAllReports()
        {
            return this.reports;
        }

    }
}
