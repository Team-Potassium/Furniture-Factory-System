namespace FileSystemUtils.FileLoaders
{
    using FileSystemUtils.Contracts;

    /// <summary>
    /// Describes classes that load data from file of certain type.
    /// </summary>
    public interface IFileLoader
    {
        string FileExtension { get; }

        void Load(string filePath);

        void AddDataLoader(IDataImporter dataLoader);
    }
}