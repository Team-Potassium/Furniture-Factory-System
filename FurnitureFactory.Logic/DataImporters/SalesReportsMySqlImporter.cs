//namespace FurnitureFactory.Logic
//{
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.Linq;
//    using Data.Json;
//    using Data.MySql;
//    using Data.MySql.Models;
//    using Models;
//    using Newtonsoft.Json;

//    public class SalesReportsMySqlImporter
//    {
//        private MySqlData mySqlHandler;
//        private JsonRepository reportsRepository = JsonRepository.GetInstance();

//        public SalesReportsMySqlImporter(IUserInterfaceHandlerIO io)
//        {
//            var password = io.GetPassword();
//            this.mySqlHandler = new MySqlData(password);
//        }

//        public void Save()
//        {
//            IDictionary<string, object> reports = reportsRepository.GetAllReports();

//            // TODO: New model for reports
//            List<SalesTotalCostReport> deserializedReports = reports.Values
//                .Select(x => JsonConvert.DeserializeObject<SalesTotalCostReport>(x.ToString()))
//                .ToList();

//            mySqlHandler.Import(deserializedReports);
//        }

//    }
//}
