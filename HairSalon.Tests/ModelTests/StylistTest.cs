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
        Stylist.DeleteAll();
        Stylist.DeleteAll();
        }

        public StylistTests()
        {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=johnny_mayer_test";
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSameStylist_True()
        {
        Stylist firstStylist = new Stylist("Jane");
        Stylist secondStylist = new Stylist("Jane");

        firstStylist.Save();
        secondStylist.Save();

        Assert.AreEqual(true, firstStylist.GetName().Equals(secondStylist.GetName()));
        }

        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
        Stylist testStylist = new Stylist("Jane");;
        testStylist.Save();

        Stylist foundStylist = Stylist.Find(testStylist.GetId());

        Assert.AreEqual(testStylist.GetId(), foundStylist.GetId());
        }
    }
}
