﻿﻿namespace FurnitureFactory.Data.Manager.Exporters
{
    using System.IO;

    public abstract class XmlFileExporter
    {
        protected const string XmlFilePath = "../../../Reports/Xml-Exports";
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
