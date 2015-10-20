using System;
using FileSystemUtils.FileLoaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemUtils.Tests.FileLoaders
{
    [TestClass]
    public class ExcelFileLoaderTests
    {
        [TestMethod]
        public void GetFileDateShouldReturnCorrectDate()
        {
            var filePath = @"..\filename-10-Jun-2015.xls";
            var excelFileLoader = new ExcelFileLoader();

            var extractedDate = excelFileLoader.GetFileDate(filePath);

            var expectedDate = new DateTime(2015,6,10);
            Assert.AreEqual(expectedDate, extractedDate);
        }
    }
}
