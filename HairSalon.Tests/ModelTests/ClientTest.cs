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
            string controlName = "Jane";
            Client newClient = new Client("Jane");

            //act
            string result = newClient.GetClientName();

            //assert
            Assert.AreEqual(result, controlName);
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, act
            int result = Client.GetAll().Count;

            //assert
            Assert.AreEqual(result, 0);
        }
    }
}
