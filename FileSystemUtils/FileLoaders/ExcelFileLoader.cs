namespace FileSystemUtils.FileLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using FileSystemUtils.Contracts;

    public class ExcelFileLoader : IFileLoader
    {
        private readonly string fileExtension = "xls";

        private List<IDataImporter> dataLoaders = new List<IDataImporter>();

        public string FileExtension
        {
            get { return this.fileExtension; }
        }

        public void Load(string filePath)
        {
            // IMPORTANT!!! The system needs to have been installed the following OLE.DB provider:
            // http://www.microsoft.com/en-us/download/confirmation.aspx?id=23734
            // this is necessary for reading excel, access and other data by the CLR

            // HDR - tells the provider that the source file has a header row
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 xml;HDR=NO;IMEX=1;'";

            OleDbConnection databaseConnection = new OleDbConnection(excelConnectionString);

            databaseConnection.Open();
            using (databaseConnection)
            {
                OleDbCommand readCmd = new OleDbCommand("SELECT * FROM [Sales$]", databaseConnection);
                var reader = readCmd.ExecuteReader();

                using (reader)
                {
                    foreach (var dataLoader in this.dataLoaders)
                    {
                        while (reader.Read())
                        {
                            IList<object> currentRowFields = new List<object>();
                            var columnCount = reader.FieldCount;
                            for (int i = 0; i < columnCount; i++)
                            {
                                var fieldContent = (reader[i].GetType() == typeof(DBNull)) ? null : reader[i];
                                currentRowFields.Add(fieldContent);
                            }

                            dataLoader.ImportData(currentRowFields);
                        }
                    }
                }

                foreach (var dataLoader in this.dataLoaders)
                {
                    dataLoader.FinalizeImporting();
                }
            }
        }

        public void AddDataLoader(IDataImporter dataLoader)
        {
            this.dataLoaders.Add(dataLoader);
        }
    }
}