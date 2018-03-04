using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }

        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=johnny_mayer_test";
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSameClient_True()
        {
            Client firstClient = new Client("Jim");
            Client secondClient = new Client("Jim");

            firstClient.Save();
            secondClient.Save();

            Assert.AreEqual(true, firstClient.GetName().Equals(secondClient.GetName()));
        }

        [TestMethod]
        public void Find_FindsClientInDatabase_Client()
        {
            Client testClient = new Client("Jim");
            testClient.Save();

            Client foundClient = Client.Find(testClient.GetId());

            Assert.AreEqual(testClient, foundClient);
        }

        [TestMethod]
        public void Delete_DeleteAllClientsInDatabase_void()
        {
            Client newClient = new Client("Jim");

            Client.DeleteAll();
            int result = Client.GetAll().Count;

            Assert.AreEqual(0, result);
        }

    }
}
