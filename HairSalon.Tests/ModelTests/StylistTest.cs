using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=johnny_mayer_test;";
        }

        [TestMethod]
        public void GetClients_RetrievesAllClientsWithStylistId_ClientList()
        {
            Stylist testStylist = new Stylist("Jane", 1);
            testStylist.Save();

            Client firstClient = new Client("Adam", 1, testStylist.GetStylistId());
            Client secondClient = new Client("Paul", 2, testStylist.GetStylistId());

            List<Client> testClientList = new List<Client> {firstClient, secondClient};
            List<Client> resultClientList = testStylist.GetClients();

            CollectionAssert.AreEqual(testClientList, resultClientList);
        }
    }
}
