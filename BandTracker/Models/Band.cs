using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public override bool Equals(Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;

        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());


        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Band> GetAll()
    {
     List<Band> bandList = new List<Band> {};

     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM bands;";

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int bandId = rdr.GetInt32(0);
       string name = rdr.GetString(1);

       Band newBand = new Band(name, bandId);
       bandList.Add(newBand);
     }
     conn.Close();
     return bandList;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    public static Band Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands WHERE id = @bandId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@bandId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int bandId = 0;
      string bandName = "";

      while(rdr.Read())
      {
        bandId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);

      }
      Band foundBand = new Band(bandName, bandId);
      conn.Close();
      return foundBand;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void AddVenueToJoinTable(Venue newVenue)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);";

      MySqlParameter venue_id_param = new MySqlParameter();
      venue_id_param.ParameterName = "@VenueId";
      venue_id_param.Value = newVenue.GetId();
      cmd.Parameters.Add(venue_id_param);

      MySqlParameter band_id_param = new MySqlParameter();
      band_id_param.ParameterName = "@BandId";
      band_id_param.Value = _id;
      cmd.Parameters.Add(band_id_param);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Venue> GetVenues()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT venues.*
          FROM bands
          JOIN bands_venues ON (bands.id = bands_venues.band_id)
          JOIN venues ON (venues.id = bands_venues.venue_id)
          WHERE bands.id = @BandId;";

        MySqlParameter bandIdParameter = new MySqlParameter();
        bandIdParameter.ParameterName = "@BandId";
        bandIdParameter.Value = _id;
        cmd.Parameters.Add(bandIdParameter);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Venue> categories = new List<Venue>{};

        while(rdr.Read())
        {
          int venueId = rdr.GetInt32(0);
          string venueName = rdr.GetString(1);

          Venue newVenue = new Venue(venueName, venueId);
          categories.Add(newVenue);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return categories;
    }

  }
}
