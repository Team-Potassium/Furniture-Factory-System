namespace FurnitureFactory.Data.Xml.Exporters
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public abstract class XmlFileExporter
    {
        protected const string XmlFilePath = "../../../Xml-Exports";
        protected const string XmlExtension = ".xml";
        protected const string XmlReportResult = "Xml report for {0} was saved successful!";

        protected FurnitureFactoryDbContext db;

        public XmlFileExporter(FurnitureFactoryDbContext db)
        {
            this.db = db;
        }

        public abstract void GetXmlReport();

        protected void CreateDirectoryIfNotExist()
        {
            if (!Directory.Exists(XmlFilePath))
            {
                Directory.CreateDirectory(XmlFilePath);
            }
        }
    }
}
