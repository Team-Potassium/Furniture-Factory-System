using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureFactory.Data.Xml.Importers
{
    public abstract class XmlFileImporter
    {
        protected string XmlFilePath;

        public abstract void ImportXmlData(string filepath);

    }
}
