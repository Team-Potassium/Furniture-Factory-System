﻿namespace FurnitureFactory.Data.Manager.Importers
{
    public abstract class XmlFileImporter
    {
        protected string XmlFilePath;

        public abstract void ImportXmlData(string filepath);

    }
}

