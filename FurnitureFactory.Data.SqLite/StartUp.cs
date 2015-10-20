namespace FurnitureFactory.Data.SqLite
{
    using System.Diagnostics;

    public class StartUp
    {
        public static void Main()
        {
            new ClientsInfoSqLiteExporter().Export();

        }
    }
}
