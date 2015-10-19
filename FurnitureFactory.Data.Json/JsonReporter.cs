namespace FurnitureFactory.Data.Json
{
    using System;
    using System.IO;
    using System.Linq;

    using FurnitureFactory.Data;
    using Newtonsoft.Json;

    public class JsonProductsReporter
    {
        // Can be passed through the constructor
        private const string JsonFilePath = "../../../Json-Reports";
        private const string FileExtension = ".json";
        private const string NoProductsFound = "No products in database!";

        private readonly FurnitureFactoryDbContext db;

        public JsonProductsReporter(FurnitureFactoryDbContext db)
        {
            this.db = db;
        }

        public void GetJsonReport()
        {
            if (!Directory.Exists(JsonFilePath))
            {
                Directory.CreateDirectory(JsonFilePath);
            }

            var products = this.db.Products
               .Select(pr =>
               new
               {
                   pr.Id,
                   pr.ProductionExpense,
                   pr.ProductionTime,
                   pr.Weight,
                   pr.CatalogNumber,
                   FurnitureType = pr.FurnitureType.Name,
                   Room = pr.Room.Name,
                   Series = pr.Series.Name
               }).ToList();

            if (products.Any())
            {
                foreach (var product in products)
                {
                    var json = JsonConvert.SerializeObject(product, Formatting.Indented);

                    using (var writer = new StreamWriter(JsonFilePath + "/" + product.Id + FileExtension))
                    {
                        writer.Write(json);
                        Console.Write(".");
                    }
                }
            }
            else
            {
                Console.WriteLine(NoProductsFound);
            }
        }
    }
}
