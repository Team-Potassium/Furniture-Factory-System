namespace FileSystemUtils.FileLoaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using FileSystemUtils.FileLoaders;

    /// <summary>
    /// Extracts all .xls files from zip archive to destination path.
    /// </summary>
    public class ZipArchiveLoader : IFileLoader
    {
        private readonly string fileExtension = "zip";

        private ICollection<IFileLoader> fileLoaders;

        public ZipArchiveLoader()
        {
            this.fileLoaders = new List<IFileLoader>();
        }

        public string FileExtension
        {
            get { return this.fileExtension; }
        }

        public void Load(string sourceFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException();
            }

            var targetPath = @"..\temp";
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            // this will keep the directory structure and extract all files at once. 
            // I`m not using this method for performance reasons.

            //ZipFile.ExtractToDirectory(sourceFilePath, targetPath);

            // this will extract each .xls file in temp dir, then load its data, then delete the extracted file, then continue to the next.
            using (ZipArchive archive = ZipFile.OpenRead(sourceFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    foreach (var fileLoader in this.fileLoaders)
                    {
                        if (entry.FullName.EndsWith(fileLoader.FileExtension, StringComparison.OrdinalIgnoreCase))
                        {
                            var targetFilePath = Path.Combine(targetPath, entry.Name);

                            entry.ExtractToFile(targetFilePath, true);

                            fileLoader.Load(targetFilePath);

                            File.Delete(targetFilePath);
                        }
                    }
                }
            }
        }

        public void AddFileLoader(IFileLoader fileLoader)
        {
            if (fileLoader == null)
            {
                throw new ArgumentNullException();
            }

            this.fileLoaders.Add(fileLoader);
        }
    }
}