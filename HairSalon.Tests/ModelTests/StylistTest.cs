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
    public void GetClients_RetrievesAllClientsWithStylistId_ClientList()
    {
      Stylist testStylist = new Stylist("Jane", 1);
      testStylist.Save();

      Client firstClient = new Client("Adam", 1, testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Paul", 2, testStylist.GetId());
      secondClient.Save();

      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      CollectionAssert.AreEqual(testClientList, resultClientList);
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
      Stylist testStylist = new Stylist("Jane");
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

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //arrange
      Stylist testStylist = new Stylist("Jane", 1);
      testStylist.Save();

      //act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void Delete_DeleteStylistFromDatabase_Void()
    {
      //arrange
      Stylist testStylist1 = new Stylist("Gina");
      testStylist1.Save();
      List<Stylist> originalList = Stylist.GetAll();
      Stylist testStylist2 = new Stylist("Ruby");
      testStylist2.Save();

      //act
      testStylist2.Delete();
      List<Stylist> newList = Stylist.GetAll();

      //assert
      CollectionAssert.AreEqual(originalList, newList);
    }

    [TestMethod]
    public void Delete_DeleteStylistANDClientsFromDB_Void()
    {
      //arrange
      Stylist testStylist1 = new Stylist("Gina", 1);
      testStylist1.Save();
      Client testClient1 = new Client("Adam", 1, 1);
      Client testClient2 = new Client("Paul", 2, 1);
      testClient1.Save();
      testClient2.Save();

      Stylist testStylist2 = new Stylist("Ruby", 2);
      testStylist2.Save();
      Client testClient3 = new Client("Greg", 3, 2);
      Client testClient4 = new Client("Dave", 4, 2);
      testClient3.Save();
      testClient4.Save();

      int numExistingClientsControl = 2;

      //act
      testStylist1.Delete();
      int result = Client.GetClients().Count;

      //assert
      Assert.AreEqual(numExistingClientsControl, result);
    }
  }
}
