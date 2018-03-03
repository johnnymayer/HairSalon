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
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=johnny_mayer_test;";
    }


    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
      //arrange, act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylistId_ClientList()
    {
        Stylist testStylist = new Stylist("Jane", 1);
        testStylist.Save();

        Client firstClient = new Client("Jill", 1, testStylist.GetId());
        firstClient.Save();
        Client secondClient = new Client("John", 2, testStylist.GetId());
        secondClient.Save();

        List<Client> testClientList = new List<Client> {firstClient, secondClient};
        List<Client> resultClientList = testStylist.GetClients();

        CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Jane");
      Stylist secondStylist = new Stylist("Jane");

      //assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //arrange
      Stylist testStylist = new Stylist("Jane", 1);
      testStylist.Save();

      //act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //arrange
      Stylist testStylist = new Stylist("Jane");
      testStylist.Save();

      //act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //assert
      Assert.AreEqual(testId, result);
    }
  }
}
