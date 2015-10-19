namespace FurnitureFactory.Data.Xml.Exporters
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class OrdersXmlFileExporter : XmlFileExporter
    {
        private const string XmlOrdersReport = "/orders";
        private const string NoOrdersFound = "No orders in database!";

        public OrdersXmlFileExporter(FurnitureFactoryDbContext db) 
            : base(db)
        {
        }

        public override void GetXmlReport()
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
                Console.WriteLine(string.Format(XmlReportResult, "orders"));
            }
            else
            {
                Console.WriteLine(NoOrdersFound);
            }
        }
    }
}
