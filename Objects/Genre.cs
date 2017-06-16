using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Band_Tracker;

namespace Band_Tracker.Objects
{
  public class Genre
  {
    private int _id;
    private string _name;

    public Genre(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherGenre)
    {
      if (!(otherGenre is Genre))
      {
        return false;
      }
      else
      {
        Genre newGenre = (Genre) otherGenre;
        bool idEquality = this.GetId() == newGenre.GetId();
        bool nameEquality = this.GetName() == newGenre.GetName();
        return (idEquality && nameEquality);
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
    public void SetId(int Id)
    {
      _id = Id;
    }
    public void SetName(string Name)
    {
      _name = Name;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO genres (name) OUTPUT INSERTED.id VALUES (@GenreName)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@GenreName";
      nameParam.Value = this.GetName();

      cmd.Parameters.Add(nameParam);

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
      SqlCommand cmd = new SqlCommand("DELETE FROM genres;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Genre> GetAll()
    {
      List<Genre> allGenres = new List<Genre>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM genres;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int genreId = rdr.GetInt32(0);
        string genreName = rdr.GetString(1);
        Genre newGenre = new Genre(genreName, genreId);
        allGenres.Add(newGenre);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allGenres;
    }
  }
}
