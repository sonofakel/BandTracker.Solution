using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Venue.DeleteAll();
    }

    [TestMethod]
    public void GetAll_VenuesEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameName_True()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("Gorge");
      Venue secondVenue = new Venue("Gorge");

      //Assert
      Assert.AreEqual(firstVenue, secondVenue);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToVenue_Id()
    {
      //Arrange
      Venue testVenue = new Venue("Gorge");
      testVenue.Save();

      //Act
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabase_Venue()
    {
      //Arrange
      Venue testVenue = new Venue("Gorge");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.AreEqual(testVenue, foundVenue);
    }

    [TestMethod]
    public void Update_UpdatesVenueNameInDatabase_Venue()
    {
      Venue testVenue = new Venue("Gorge");
      Venue testVenue2 = new Venue("ChaCha");
      testVenue.Save();
      testVenue2.Save();

      string newName = "Crocodile";
      testVenue.Update(newName);

      string expected = newName;
      string actual = testVenue.GetName();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Delete_DeletesVenueFromDatabase_VenueList()
    {
      //Arrange
      string name1 = "Gorge";
      Venue testVenue1 = new Venue(name1);
      testVenue1.Save();

      string name2 = "Crocodile";
      Venue testVenue2 = new Venue(name2);
      testVenue2.Save();

      //Act
      testVenue1.Delete();
      List<Venue> resultVenues = Venue.GetAll();
      List<Venue> testVenueList = new List<Venue> {testVenue2};

      //Assert
      CollectionAssert.AreEqual(testVenueList, resultVenues);
    }


  }
}
