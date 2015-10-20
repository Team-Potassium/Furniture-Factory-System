namespace FurnitureFactory.Data.Xml.Importers
{
    using System;
    using System.Xml.Linq;
    using Models;

    public class ProductionDetailsXmlImporter
    {
        private SqlLocalRepository<ProductsMaterialsQuantity> materialQuantityDb;
        private string documentPath = "../../../Resources/productionDetails.xml";

        public ProductionDetailsXmlImporter()
        {
            this.materialQuantityDb = new SqlLocalRepository<ProductsMaterialsQuantity>();
        }

        public void Import()
        {
            XDocument xmlDoc = XDocument.Load(documentPath);
            var descendants = xmlDoc.Descendants("info");

            Console.WriteLine("Seeding materials quantity from XML to SQL");

            foreach (var node in descendants)
            {
                var item = new ProductsMaterialsQuantity();

                item.ProductId = int.Parse(node.Element("productid").Value);
                item.MaterialId = int.Parse(node.Element("materialid").Value);
                item.Quantity = int.Parse(node.Element("quantity").Value);

                this.materialQuantityDb.Add(item);
                Console.Write(".");
            }

            this.materialQuantityDb.SaveChanges();
        }

    }
}
