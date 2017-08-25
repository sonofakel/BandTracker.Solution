using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class  Venue
  {
    private int _id;
    private string _name;

    public Venue(string name, int id = 0)
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

    public override bool Equals(Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;

        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());


        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Venue> GetAll()
    {
     List<Venue> venueList = new List<Venue> {};

     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM venues;";

     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     while(rdr.Read())
     {
       int venueId = rdr.GetInt32(0);
       string name = rdr.GetString(1);
       Venue newVenue = new Venue(name, venueId);
       venueList.Add(newVenue);
     }
     conn.Close();
     return venueList;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);


      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    public static Venue Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @venueId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@venueId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int venueId = 0;
      string venueName = "";


      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);

      }
      Venue foundVenue = new Venue(venueName, venueId);
      conn.Close();
      return foundVenue;
    }

    public void Update(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET name = @newName WHERE id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      conn.Close();
      _name = newName;
    }


    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues;";
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
