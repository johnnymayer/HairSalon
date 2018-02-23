using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _stylistName;
    private int _stylistId;

    public Stylist(string stylistName, int stylistId = 0)
    {
      _stylistName = stylistName;
      _stylistId = stylistId;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public string GetStylistName()
    {
      return _stylistName;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int StylistId = rdr.GetInt32(1);
        string StylistName = rdr.GetString(0);
        Stylist newStylist = new Stylist(StylistName, StylistId);
        allStylists.Add(newStylist);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        return this.GetStylistId().Equals(newStylist.GetStylistId());
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `stylists` (`stylistName`, `stylistId`) VALUES (@stylistName, @stylistId);";

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@stylistName";
      stylistName.Value = this._stylistName;

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = this._stylistId;

      cmd.Parameters.Add(stylistName);
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _stylistId = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Client> GetClients()
    {
      List<Client> allStylistClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE `stylistId` = @stylistId;";

      MySqlParameter StylistId = new MySqlParameter();
      StylistId.ParameterName = "@stylistId";
      StylistId.Value = this._stylistId;
      cmd.Parameters.Add(StylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ClientId = rdr.GetInt32(0);
        string ClientName = rdr.GetString(1);
        Client newClient = new Client(ClientName, ClientId);
        allStylistClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allStylistClients;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // public void Delete()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";
    //
    //   var cmdClients = conn.CreateCommand() as MySqlCommand;
    //   cmdClients.CommandText = @"DELETE FROM clients WHERE stylist_id = @thisId;";
    //
    //   MySqlParameter thisId = new MySqlParameter();
    //   thisId.ParameterName = "@thisId";
    //   thisId.Value = _clientId;
    //   cmd.Parameters.Add(thisId);
    //   cmdClients.Parameters.Add(thisId);
    //
    //   cmdClients.ExecuteNonQuery();
    //   cmd.ExecuteNonQuery();
    //
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    // }

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `stylists` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int StylistId = 0;
      string StylistName = "";

      while (rdr.Read())
      {
        StylistId = rdr.GetInt32(1);
        StylistName = rdr.GetString(0);
      }

      Stylist foundStylist = new Stylist(StylistName, StylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundStylist;
    }
  }
}
