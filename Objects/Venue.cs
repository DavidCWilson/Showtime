using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Band_Tracker;

namespace Band_Tracker.Objects
{
  public class Venue
  {
    private int _id;
    private string _name;
    private string _city;

    public Venue(string Name, string City, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _city = City;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = this.GetId() == newVenue.GetId();
        bool nameEquality = this.GetName() == newVenue.GetName();
        bool cityEquality = this.GetCity() == newVenue.GetCity();
        return (idEquality && nameEquality && cityEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetCity()
    {
      return _city;
    }
    public void SetId(int Id)
    {
      _id = Id;
    }
    public void SetName(string Name)
    {
      _name = Name;
    }
    public void SetCity(string City)
    {
      _city = City;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name, city) OUTPUT INSERTED.id VALUES (@VenueName, @VenueCity)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@VenueName";
      nameParam.Value = this.GetName();

      SqlParameter cityParam = new SqlParameter();
      cityParam.ParameterName = "@VenueCity";
      cityParam.Value = this.GetCity();

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(cityParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        string venueCity = rdr.GetString(2);
        Venue newVenue = new Venue(venueName, venueCity, venueId);
        allVenues.Add(newVenue);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allVenues;
    }
  }
}
