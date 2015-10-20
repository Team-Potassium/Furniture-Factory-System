using System;
using FileSystemUtils.FileLoaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystemUtils.Tests.FileLoaders
{
    [TestClass]
    public class ExcelFileLoaderTests
    {
        [TestMethod]
        public void GetFileDateShouldReturnCorrectDateOnCorrectFilePath()
        {
            var filePath = @"..\filename-10-Jun-2015.xls";
            var excelFileLoader = new ExcelFileLoader();

            var extractedDate = excelFileLoader.GetFileDate(filePath);

            var expectedDate = new DateTime(2015,6,10);
            Assert.AreEqual(expectedDate, extractedDate);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetFileDateShouldThrowOnWrongDateFormat()
        {
            var filePath = @"..\filename-55-Jun-2015.xls";
            var excelFileLoader = new ExcelFileLoader();

            var extractedDate = excelFileLoader.GetFileDate(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GetFileDateShouldThrowOnWrongFileNameFormat()
        {
            var filePath = @"..\filename-55-Jun-2015-wrong-wrong.xls";
            var excelFileLoader = new ExcelFileLoader();

            var extractedDate = excelFileLoader.GetFileDate(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetFileDateShouldThrowOnWrongFilePath()
        {
            var filePath = @"..\someFolder\fsdf";
            var excelFileLoader = new ExcelFileLoader();

            var extractedDate = excelFileLoader.GetFileDate(filePath);
        }
    }
}
