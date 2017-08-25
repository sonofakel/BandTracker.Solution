using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTests
  {
    public VenueTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
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


  }
}
