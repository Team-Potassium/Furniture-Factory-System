namespace FileSystemUtils.FileLoaders
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Composite pattern interface for IFileLoader.
    /// </summary>
    public interface IArchiveLoader
    {
        void Load(string filePath);

        string FileExtension { get; }

        void AddFileLoader(IFileLoader fileLoader);

    }
}
