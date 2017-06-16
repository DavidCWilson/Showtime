using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using Band_Tracker;
using Band_Tracker.Objects;

namespace Band_TrackerTests
{
  [Collection("Band_TrackerTests")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
     Band.DeleteAll();
     Venue.DeleteAll();
     Genre.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     int result = Band.GetAll().Count;
     Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualityOfBandObjects()
    {
     Band firstBand = new Band("My Tentacle Romance");
     Band secondBand = new Band("My Tentacle Romance");
     Assert.Equal(firstBand, secondBand);
    }

    [Fact]
    public void Save_SavesToDatabase()
    {
      //Arrange
      Band testBand = new Band("Biddie Hobbie");
      testBand.Save();

      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_IdAssignationWorksAsPlanned()
    {
      //Arrange
      Band testBand = new Band("Blinkubus");
      testBand.Save();

      //Act
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
  }
}
