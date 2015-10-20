//namespace FurnitureFactory.Data.Json
//{
//    using System;
//    using System.IO;
//    using System.Linq;
//    using FurnitureFactory.Data;
//    using MySql.Models;
//    using Newtonsoft.Json;

//    public class JsonProductsReporter
//    {
//        // Can be passed through the constructor
//        private const string JsonFilePath = "../../../Json-Reports";
//        private const string FileExtension = ".json";
//        private const string NoProductsFound = "No products in database!";

//        private readonly FurnitureFactoryDbContext db;
//        private JsonRepository localRepository = JsonRepository.GetInstance();

//        public JsonProductsReporter(FurnitureFactoryDbContext db)
//        {
//            this.db = db;

//        }

//        public void Load()
//        {
//            if (!Directory.Exists(JsonFilePath))
//            {
//                Directory.CreateDirectory(JsonFilePath);
//            }

//            var data = this.localRepository.GetAllReports();

//            foreach (var product in data)
//            {
//                string json = JsonConvert.SerializeObject(product.Value, Formatting.Indented);

//                using (var writer = new StreamWriter(JsonFilePath + "/" + product.Key + FileExtension))
//                {
//                    writer.Write(json);
//                    Console.Write(".");
//                }
//            }
//        }

//        public JsonProductsReporter GetJsonReport()
//        {
//            var reports = db.Clients
//                .Select(x => new SalesTotalCostReport
//                {
//                    Name = x.Name,
//                    TotalCost = db.Orders.Where(o => o.Client.Id == x.Id).Select(price => price.Price).Sum(t => t)
//                })
//                .ToList();

//            reports.ForEach(x => this.localRepository.AddReport(
//                    x.Name,
//                    JsonConvert.SerializeObject(x, Formatting.Indented)));
//            return this;
//        }
//    }
//}
