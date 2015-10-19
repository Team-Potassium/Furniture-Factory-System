namespace FurnitureFactory.Data.Xml.Exporters
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlFileExporter
    {
        private const string XmlFilePath = "../../../Xml-Exports";
        private const string XmlProductsReport = "/products";
        private const string XmlOrdersReport = "/orders";
        private const string XmlExtension = ".xml";
        private const string NoProductsFound = "No products in database!";
        private const string NoOrdersFound = "No orders in database!";

        private FurnitureFactoryDbContext db;

        public XmlFileExporter(FurnitureFactoryDbContext db)
        {
            this.db = db;
        }

        public void GetXmlReportForProducts()
        {
            var productsByWeigth = this.db.Products
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
                                new XAttribute("product-id", pr.Id),
                                new XAttribute("expense", pr.ProductionExpense),
                                new XAttribute("time", pr.ProductionTime)))))));

                this.CreateDirectoryIfNotExist();
                doc.Save(XmlFilePath + XmlProductsReport + XmlExtension);
                Console.WriteLine("Xml report for products was saved successful!");
            }
            else
            {
                Console.WriteLine(NoProductsFound);
            }
        }

        public void GetXmlReportForOrders()
        {
            var ordersByClient = this.db.Orders
                .Select(o =>
                    new
                    {
                        Client = o.Client.Name,
                        CatalogNumber = o.Product.CatalogNumber,
                        Price = o.Price,
                        DeliveryDate = o.DeliveryDate
                    })
                    .GroupBy(o => o.Client)
                    .ToList();

            if (ordersByClient.Any())
            {
                var doc = new XDocument(
                new XElement(
                    "orders",
                    ordersByClient.Select(gr => new XElement(
                        "order",
                        new XAttribute("client", gr.Key),
                        gr.Select(o => new XElement(
                            "product",
                            new XAttribute("catalog-number", o.CatalogNumber),
                            new XAttribute("price", o.Price),
                            new XAttribute("delivery-date", o.DeliveryDate)))))));

                this.CreateDirectoryIfNotExist();
                doc.Save(XmlFilePath + XmlOrdersReport + XmlExtension);
                Console.WriteLine("Xml report for orders was saved successful!");
            }
            else
            {
                Console.WriteLine(NoOrdersFound);
            }
        }

        private void CreateDirectoryIfNotExist()
        {
            if (!Directory.Exists(XmlFilePath))
            {
                Directory.CreateDirectory(XmlFilePath);
            }
        }
    }
}
