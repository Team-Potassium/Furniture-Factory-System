namespace FurnitureFactory.Data.Manager.Importers
{
    using System;
    using System.Xml.Linq;
    using Models;

    public class MaterialsXmlImporter
    {
        private SqlLocalRepository<Material> materialsDb;
        private string documentPath = "../../../Resources/XML/materials.xml";

        public MaterialsXmlImporter()
        {
            this.materialsDb = new SqlLocalRepository<Material>();
        }

        public void Import()
        {
            XDocument xmlDoc = XDocument.Load(documentPath);
            var descendants = xmlDoc.Descendants("material");
            Console.WriteLine("Seeding materials from XML to SQL");
            foreach (var node in descendants)
            {
                var item = new Material();
                item.Name = node.Element("name").Value;
                item.PricePerUnit = decimal.Parse(node.Element("price-per-unit").Value);
                item.MeasurementUnit = node.Element("measure-unit").Value;

                this.materialsDb.Add(item);
                Console.Write(".");
            }

            this.materialsDb.SaveChanges();
        }

    }
}
