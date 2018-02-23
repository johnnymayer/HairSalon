using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using HairSalon.Models;

namespace HairSalon.Models
{
    public class Client
    {
        private string _clientName;
        private int _clientId;
        private int _stylistId;

        public Client(string clientName, int clientId = 0, int stylistId = 0)
        {
            _clientName = clientName;
            _clientId = clientId;
            _stylistId = stylistId;
        }

        public int GetClientId()
        {
            return _clientId;
        }

        public string GetClientName()
        {
            return _clientName;
        }

        public static List<Client> GetAll()
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
                Client newClient = new Client(clientName, clientId);
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
            cmd.CommandText = @"INSERT INTO `clients` (`clientName`, `stylistId`) VALUES (@ClientName, @StylistId);";

            MySqlParameter clientName = new MySqlParameter();
            clientName.ParameterName = "@ClientName";
            clientName.Value = this._clientName;

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@StylistId";
            stylistId.Value = this._stylistId;

            cmd.Parameters.Add(clientName);
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _stylistId = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
    }
}
