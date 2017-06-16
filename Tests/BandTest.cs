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

    [Fact]
    public void Test_FindsBandInDatabaseWorks()
    {
      //Arrange
      Band testBand = new Band("ChthoniC");
      testBand.Save();

      //Act
      Band result = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, result);
    }

    [Fact]
    public void Test_ReturnsAllVenuesAddedToBandsList()
    {

      Band testBand = new Band("The Number 12 Looks Like You");
      testBand.Save();
      Venue testVenue1 = new Venue("BlackWater Bar", "Portland");
      testVenue1.Save();
      Venue testVenue2 = new Venue("The Know", "Portland");
      testVenue2.Save();

      testVenue1.AddBandToShowsJoinTable(testBand);
      testVenue2.AddBandToShowsJoinTable(testBand);

      List<Venue> savedVenues = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1, testVenue2};

      Assert.Equal(testList, savedVenues);
    }

    [Fact]
    public void Test_TheAbilityToAdd_SingleVanueToBand()
    {

      Band testBand = new Band("The Number 12 Looks Like You");
      testBand.Save();
      Venue testVenue1 = new Venue("BlackWater Bar", "Portland");
      testVenue1.Save();
      Venue testVenue2 = new Venue("The Know", "Portland");
      testVenue2.Save();

      testBand.AddVenueToShowsJoinTable(testVenue1);
      testBand.AddVenueToShowsJoinTable(testVenue2);

      List<Venue> savedVenues = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1, testVenue2};

      Assert.Equal(testList, savedVenues);
    }
  }
}
