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

        [TestMethod]
        public void SalesReportsImporterShouldImportCorrectly()
        {
            DateTime date = new DateTime(2015, 6, 10);

            dbMock.Setup(db => db.Orders.Add(It.Is<Order>(
                o => o.DeliveryDate == date)));

            var mockClients = new InMemoryDbSet<Client>() { new Client() { Id = 1 }};
            dbMock.Setup(db => db.Clients).Returns(mockClients);

            // first record is used to store the Client Name and Id for the file
            var firstRecord = new List<Object>()
            {
                "CLient Name"
            };

            // second record and on are the orders entries for the particular client
            var secondRecord = new List<Object>()
            {
                "0",    //  ProductId
                "1",    //  quantity
                "2",    //  unused value
                "3",    //  Price
                date,   //  Date is inserted as last property
            };

            var salesReportImporter = new SalesReportsImporter();
            salesReportImporter.Db = dbMock.Object;

            salesReportImporter.ImportData(firstRecord);
            salesReportImporter.ImportData(secondRecord);
        }
    }
}