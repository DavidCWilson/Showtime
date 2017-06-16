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
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
     Venue.DeleteAll();
     Band.DeleteAll();
     Genre.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     int result = Venue.GetAll().Count;
     Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualityOfVenueObjects()
    {
     Venue firstVenue = new Venue("The Apollo", "New York");
     Venue secondVenue = new Venue("The Apollo", "New York");
     Assert.Equal(firstVenue, secondVenue);
    }

    [Fact]
    public void Test_SavesToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("The Crystal Ballroom", "Portland");
      testVenue.Save();

      //Act
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_IdAssignationWorksAsPlanned()
    {
      //Arrange
      Venue testVenue = new Venue("Red Rocks Ampitheatre", "Morrison");
      testVenue.Save();

      //Act
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindsVenueInDatabaseWorks()
    {
      //Arrange
      Venue testVenue = new Venue("Lovecraft", "Portland");
      testVenue.Save();

      //Act
      Venue result = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, result);
    }

    [Fact]
    public void Test_ReturnsAllBandsAddedToVenuesList()
    {

      Venue testVenue = new Venue("Swift", "Portland");
      testVenue.Save();
      Band testBand1 = new Band("MF DOOM");
      testBand1.Save();
      Band testBand2 = new Band("BusDriver, Milo, Anderson .Paak");
      testBand2.Save();

      testVenue.AddBandToShowsJoinTable(testBand1);
      testVenue.AddBandToShowsJoinTable(testBand2);

      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1, testBand2};
      // CONSOLE LOGGING LIST ITEMS (GETTING IDs)
      // Console.WriteLine("savedBands list id = {0}, {1}", savedBands[0].GetName(), savedBands[1].GetName());
      // Console.WriteLine("testList list id = {0}, {1}", testList[0].GetName(), testList[1].GetName());

      Assert.Equal(testList, savedBands);
    }
  }
}
