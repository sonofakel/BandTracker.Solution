using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {

    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    public void Dispose()
   {
     Band.DeleteAll();
     Venue.DeleteAll();
   }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Band()
    {
      //Arrange, Act
      Band firstBand = new Band("Nirvana");
      Band secondBand = new Band("Nirvana");

      //Assert
      Assert.AreEqual(firstBand, secondBand);
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_BandList()
    {
      //Arrange
      Band testBand = new Band("Nirvana");

      //Act
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Find_FindsBandInDatabase_Band()
    {
     //Arrange
     Band testBand = new Band("Nirvana");
     testBand.Save();

     //Act
     Band foundBand = Band.Find(testBand.GetId());

     //Assert
     Assert.AreEqual(testBand, foundBand);
    }

    [TestMethod]
    public void GetVenues_ReturnsAllVenueBands_VenueList()
    {
      //Arrange
      Band testBand = new Band("Nirvana");
      testBand.Save();

      Venue testVenue1 = new Venue("Gorge");
      testVenue1.Save();

      Venue testVenue2 = new Venue("Crocodile");
      testVenue2.Save();

      //Act
      testBand.AddVenueToJoinTable(testVenue1);
      testBand.AddVenueToJoinTable(testVenue2);
      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1, testVenue2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void AddVenue_AddsVenueToJoinTable_VenueList()
    {
      //Arrange
      Band testBand = new Band("Nirvana");
      testBand.Save();

      Venue testVenue1 = new Venue("Gorge");
      testVenue1.Save();
      Venue testVenue2 = new Venue("Crocodile");
      testVenue2.Save();

      //Act
      testBand.AddVenueToJoinTable(testVenue1);
      testBand.AddVenueToJoinTable(testVenue2);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue1, testVenue2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

  }
}
