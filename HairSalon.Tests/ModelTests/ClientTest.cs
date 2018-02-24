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
            DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=8889; database=johnny_mayer_test;";
        }

        [TestMethod]
        public void GetClientName_ReturnsClientName_String()
        {
            //arrange
            string controlName = "Adam";
            Client newClient = new Client("Adam");

            //act
            string result = newClient.GetName();

            //assert
            Assert.AreEqual(result, controlName);
        }
        [TestMethod]
        public void Equals_ReturnsTrueIfClientNameIsTheSame_Client()
        {
            //Arrange, act
            Client firstClient = new Client("Adam");
            Client secondClient = new Client("Adam");

            //assert
            Assert.AreEqual(firstClient, secondClient);

        }

        [TestMethod]
        public void GetClients_DatabaseEmptyAtFirst_0()
        {
            //Arrange, act
            int result = Client.GetClients().Count;

            //assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {

            //arrange
            Client testClient = new Client("Adam");

            //act
            testClient.Save();
            List<Client> result = Client.GetClients();
            List<Client> testList = new List<Client>{testClient};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToClient_Id()
        {
            //Arrange
            Client testClient = new Client("Adam");

            //act
            testClient.Save();
            Client savedClient = Client.GetClients()[0];
            int result = savedClient.GetId();
            int testId = testClient.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsClientInDatabase_Client()
        {
            //arrange
            Client testClient = new Client("Adam");
            testClient.Save();

            //act
            Client foundClient = Client.Find(testClient.GetId());

            //Assert
            Assert.AreEqual(testClient, foundClient);
        }
    }
}
