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


    public static Genre Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM genres WHERE id = @GenreId", conn);
      SqlParameter genreIdParameter = new SqlParameter();
      genreIdParameter.ParameterName = "@GenreId";
      genreIdParameter.Value = id.ToString();
      cmd.Parameters.Add(genreIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundGenreId = 0;
      string foundGenreName = null;

      while(rdr.Read())
      {
        foundGenreId = rdr.GetInt32(0);
        foundGenreName = rdr.GetString(1);
      }
      Genre foundGenre = new Genre(foundGenreName, foundGenreId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundGenre;
    }

    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM genres JOIN bands_genres ON (genres.id = bands_genres.genres_id) JOIN bands ON (bands_genres.bands_id = bands.id) WHERE genres.id = @GenreId;", conn);
      SqlParameter genreIdParameter = new SqlParameter();
      genreIdParameter.ParameterName = "@GenreId";
      genreIdParameter.Value = this.GetId().ToString();

      cmd.Parameters.Add(genreIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Band> bands = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        bands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return bands;
    }

    public void AddBandToBands_GenresJoinTable(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_genres (bands_id, genres_id) OUTPUT INSERTED.bands_id VALUES (@BandId, @GenreId);", conn);

      SqlParameter genreIdParameter = new SqlParameter();
      genreIdParameter.ParameterName = "@GenreId";
      genreIdParameter.Value = this.GetId();

      SqlParameter bandsIdParameter = new SqlParameter();
      bandsIdParameter.ParameterName = "@BandId";
      bandsIdParameter.Value = newBand.GetId();

      cmd.Parameters.Add(genreIdParameter);
      cmd.Parameters.Add(bandsIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        newBand.SetId(rdr.GetInt32(0));
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

    public void UpdateName(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE genres SET name = @NewName OUTPUT INSERTED.name WHERE id = @GenreId;", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@NewName";
      nameParameter.Value = newName;

      SqlParameter genreIdParameter = new SqlParameter();
      genreIdParameter.ParameterName = "@GenreId";
      genreIdParameter.Value = this.GetId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(genreIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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
  }
}
