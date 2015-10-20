namespace FurnitureFactory.Data.SqLite
{
    using System.ComponentModel.DataAnnotations;
    using Data.SqLite;

    public class ClientsInfoSqLiteExporter
    {
        private SqLiteData sqliteData;

        public ClientsInfoSqLiteExporter()
        {
            this.sqliteData = new SqLiteData();
        }

        public void Export()
        {
            var clientsInfo = this.sqliteData.GetAll();

        }
    }
}
