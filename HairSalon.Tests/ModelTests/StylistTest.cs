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
        public void GetStylistName_ReturnsStylistName_String()
        {
            //arrange
            string controlName = "Jane";
            Stylist newStylist = new Stylist("Jane");

            //act
            string result = newStylist.GetStylistName();

            //assert
            Assert.AreEqual(result, controlName);
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, act
            int result = Stylist.GetAllStylists().Count;

            //assert
            Assert.AreEqual(result, 0);
        }
    }
}
