using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialty : BaseEntity
    {
        public List<Stylist> Stylists;
        
        public static List<Specialty> GetAll()
        {
            List<Specialty> specialties = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Specialty;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                Specialty specialty = new Specialty();
                specialty.SetValuesFromReader(rdr);
                specialties.Add(specialty);
            }
            rdr.Close();
            foreach (var specialty in specialties)
            {
                specialty.PopulateStylists(conn);
            }
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }

            return specialties;
        }
        
        private void PopulateStylists(MySqlConnection conn)
        {
            List<Stylist> stylists = new List<Stylist>();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText =
                @"SELECT s.id, s.name FROM Stylist s, Specialty_Stylist ss  where ss.specialty_id = @speciltyId and s.id = ss.stylist_id";
            MySqlParameter stylistIdParam = new MySqlParameter();
            stylistIdParam.ParameterName = "@speciltyId";
            stylistIdParam.Value = Id;
            cmd.Parameters.Add(stylistIdParam);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                Stylist stylist = new Stylist();
                stylist.SetValuesFromReader(rdr);
                stylists.Add(stylist);
            }

            Stylists = stylists;
            rdr.Close();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO Specialty (name) VALUES (@name);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = Name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}