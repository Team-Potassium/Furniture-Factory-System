using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench
{
    using System.Xml.Linq;

    class XmlResourceGenerator
    {
        private static Random random;
        static void Main(string[] args)
        {
            int numberOfMaterials = 13;
            random = new Random();
            var xdox = new XDocument();
            XDocument xDoc = new XDocument(new XElement("details", xdox.Root));


            for (int i = 1; i < 396; i++)
            {
                var info = new XElement("info");
                var item = new
                {
                    ProductId = i,
                    MaterialId = random.Next(1, 12),
                    Quantity = random.Next(1, 50)
                };
                info.Add(new XElement("productid", item.ProductId));
                info.Add(new XElement("materialid", item.MaterialId));
                info.Add(new XElement("quantity", item.Quantity));
                xDoc.Root.Add(info);
            }

            xDoc.Save("../../../Resources/production-details.xml");
        }
    }
}
