
namespace FurnitureFactory.Data.Xml.Exporters
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class ProductsXmlFileExporter : XmlFileExporter
    {
        private const string XmlProductsReport = "/products";
        private const string NoProductsFound = "No products in database!";

        public ProductsXmlFileExporter(FurnitureFactoryDbContext db) 
            : base(db)
        {
        }

        public override void GetXmlReport()
        {
            var productsByWeigth = this.db.Products
                  .Select(pr =>
                  new
                  {
                      pr.Id,
                      pr.ProductionExpense,
                      pr.ProductionTime,
                      pr.Weight,
                      pr.CatalogNumber
                  })
                  .GroupBy(pr => pr.Weight)
                  .ToList();

            if (productsByWeigth.Any())
            {
                var doc = new XDocument(
                new XElement(
                    "products",
                    productsByWeigth.Select(gr => new XElement(
                        "product",
                        new XAttribute("weight", gr.Key),
                        gr.Select(pr => new XElement(
                                "production-info",
                                new XAttribute("product-id", pr.CatalogNumber),
                                new XAttribute("expense", pr.ProductionExpense),
                                new XAttribute("time", pr.ProductionTime)))))));

                this.CreateDirectoryIfNotExist();
                doc.Save(XmlFilePath + XmlProductsReport + XmlExtension);
                Console.WriteLine(XmlReportResult, "products");
            }
            else
            {
                Console.WriteLine(NoProductsFound);
            }
        }
    }
}
