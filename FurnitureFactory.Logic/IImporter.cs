namespace FurnitureFactory.Logic
{
    using System.Data.Entity;

    public interface IImporter
    {
        DbContext Db { get; set; }
    }
}
