using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist : BaseEntity
    {
        public List<Specialty> Specialties { get; set; }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>();
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM Stylist;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                Stylist stylist = new Stylist();
                stylist.SetValuesFromReader(rdr);
                allStylists.Add(stylist);
            }

            rdr.Close();

            foreach (var stylist in allStylists)
            {
                stylist.PopulateSpecialties(conn);
            }


            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }

            return allStylists;
        }

        private void PopulateSpecialties(MySqlConnection conn)
        {
            List<Specialty> specialties = new List<Specialty>();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText =
                @"SELECT s.id, s.name FROM Specialty s, Specialty_Stylist ss  where ss.stylist_id = @stylistId and s.id = ss.specialty_id";
            MySqlParameter stylistIdParam = new MySqlParameter();
            stylistIdParam.ParameterName = "@stylistId";
            stylistIdParam.Value = Id;
            cmd.Parameters.Add(stylistIdParam);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                Specialty specialty = new Specialty();
                specialty.SetValuesFromReader(rdr);
                specialties.Add(specialty);
            }

            Specialties = specialties;
            rdr.Close();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO Stylist (name) VALUES (@StylistName);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@StylistName";
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

        public static void Delete(int id)
        {
            Delete("Stylist", id);
        }

        public static void Update(int id, string name)
        {
            Update("Stylist", id, name);
        }

    }
}