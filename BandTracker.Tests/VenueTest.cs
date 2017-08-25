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

  }
}
