using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using HairSalon.Models;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private int _id;
    private int _stylistId;

    public Client (string name, int Id = 0, int stylistId = 0)
    {
      _name = name;
      _id = Id;
      _stylistId = stylistId;
    }

    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newName)
    {
      _name = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetId(int newId)
    {
        _id = newId;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public void SetStylistId(int stylistId)
    {
      _stylistId = stylistId;
    }

    public static List<Client> GetClients()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientId, stylistId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `clients` (`name`, `stylist_id`) VALUES (@Name, @StylistId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@Name";
      name.Value = this._name;

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = this._stylistId;

      cmd.Parameters.Add(name);
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
        List<Client> allClients = Client.GetClients();
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * from `clients` WHERE id = @thisId;";

        MySqlParameter thisId = new MySqlParameter();
        thisId.ParameterName = "@thisId";
        thisId.Value = id;
        cmd.Parameters.Add(thisId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int clientId = 0;
        string clientName = "";
        int clientStylistId = 0;

        while (rdr.Read())
        {
            clientId = rdr.GetInt32(0);
            clientName = rdr.GetString(1);
            clientStylistId = rdr.GetInt32(2);
        }

        Client foundClient = new Client(clientName, clientId, clientStylistId);

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }

        return foundClient;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool stylistEquality = (this.GetStylistId() == newClient.GetStylistId());
        return (idEquality && nameEquality && stylistEquality);
      }
    }

  }
}
