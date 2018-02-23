using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _clientName;
        private string _clientId;

        public Client(string clientName, int clientId = 0)
        {
            _clientName = clientName;
            _clientId = clientId;
        }

        public int GetClientId()
        {
            return _clientId;
        }

        public string GetClientName()
        {
            return _clientName;
        }


    }
}
