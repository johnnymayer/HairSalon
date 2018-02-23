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
    }
}
