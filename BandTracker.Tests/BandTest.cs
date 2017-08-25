using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests
  {

    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
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

  }
}
