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

  }
}
