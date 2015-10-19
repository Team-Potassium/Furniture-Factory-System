namespace FileSystemUtils.FileLoaders
{
    /// <summary>
    /// Composite pattern interface for IFileLoader.
    /// </summary>
    public interface IArchiveLoader
    {
        string FileExtension { get; }

        void Load(string filePath);

        void AddFileLoader(IFileLoader fileLoader);
    }
}