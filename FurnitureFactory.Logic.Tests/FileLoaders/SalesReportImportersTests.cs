using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using FakeDbSet;
using FurnitureFactory.Data;
using FurnitureFactory.Logic.DataImporters;
using FurnitureFactory.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FurnitureFactory.Logic.Tests.FileLoaders
{
    [TestClass]
    public class SalesReportsImportersTests
    {
        private Mock<FurnitureFactoryDbContext> dbMock = new Mock<FurnitureFactoryDbContext>();

        [TestInitialize]
        public void OnStart()
        {
            var mockClients = new InMemoryDbSet<Client>() { new Client() { Id = 1 }};
            dbMock.Setup(db => db.Clients).Returns(mockClients);
        }

        [TestMethod]
        public void SalesReportsImporterShouldImportCorrectDate()
        {
            var date = new DateTime(2015, 6, 10);
            dbMock.Setup(db => db.Orders.Add(It.Is<Order>(
                o => o.DeliveryDate == date))).Verifiable();

            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                "CLient Name"
            };

            // second record and on are the orders entries for the particular client
            var secondRecord = new List<Object>()
            {
                "1",    //  ProductId
                "1",    //  quantity
                "1",    //  unused value
                "1",    //  Price
                new DateTime(2015, 6, 10),   //  Date is inserted as last property
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
            salesReportImporter.ImportData(secondRecord);

            dbMock.Verify(db => db.Orders.Add(It.Is<Order>(
                o => o.DeliveryDate == date)));
        }

        [TestMethod]
        public void SalesReportsImporterShouldNotThrowOnValidFirstNullDataRecord()
        {
            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                null
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SalesReportsImporterShouldThrowOnInvalidFirstDataRecord()
        {
            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>();

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
        }

        [TestMethod]
        public void SalesReportsImporterShouldNotThrowOnValidFirstDataRecordWithClientName()
        {
            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                "CLient Name"
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void SalesReportsImporterShouldThrowOnInvalidSecondDataRecord()
        {
            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                "CLient Name"
            };

            var secondRecord = new List<Object>()
            {
                "Unexpected data",
                null,null,null
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
            salesReportImporter.ImportData(secondRecord);
        }

        [TestMethod]
        public void SalesReportsImporterShouldNotThrowOnValidSecondDataRecord()
        {
            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                "CLient Name"
            };

            var secondRecord = new List<Object>()
            {
                "ProductID",null,null,null
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
            salesReportImporter.ImportData(secondRecord);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SalesReportsImporterShouldThrowOnInvalidSecondDataRecordLength()
        {
            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                "CLient Name"
            };

            var secondRecord = new List<Object>()
            {
                1,1
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
            salesReportImporter.ImportData(secondRecord);
        }
    }
}