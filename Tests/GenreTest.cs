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
  public class GenreTest : IDisposable
  {
    public GenreTest()
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
     int result = Genre.GetAll().Count;
     Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualityOfGenreObjects()
    {
     Genre firstGenre = new Genre("Mathcore");
     Genre secondGenre = new Genre("Mathcore");
     Assert.Equal(firstGenre, secondGenre);
    }

    [Fact]
    public void Test_SavesToDatabase()
    {
      //Arrange
      Genre testGenre = new Genre("Thrash Metal");
      testGenre.Save();

      //Act
      List<Genre> result = Genre.GetAll();
      List<Genre> testList = new List<Genre>{testGenre};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_IdAssignationWorksAsPlanned()
    {
      //Arrange
      Genre testGenre = new Genre("Black Metal");
      testGenre.Save();

      //Act
      Genre savedGenre = Genre.GetAll()[0];

      int result = savedGenre.GetId();
      int testId = testGenre.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindsGenreInDatabaseWorks()
    {
      //Arrange
      Genre testGenre = new Genre("Speedcore");
      testGenre.Save();

      //Act
      Genre result = Genre.Find(testGenre.GetId());

      //Assert
      Assert.Equal(testGenre, result);
    }
  }
}
