using System.Collections.Generic;
using System;
using System.Transactions;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client : BaseEntity
    {
        private int _stylistId;


        public int StylistId
        {
            get => _stylistId;
            set => _stylistId = value;
        }


        public static List<Client> GetAll(int stylistId)
        {
            List<Client> allClients = new List<Client> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT id, name, stylist_id FROM Client where stylist_id = @stylistId;";
            MySqlParameter stylistIdParam = new MySqlParameter();
            stylistIdParam.ParameterName = "@stylistId";
            stylistIdParam.Value = stylistId;
            cmd.Parameters.Add(stylistIdParam);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                Client client = new Client();
                client.SetValuesFromReader(rdr);
                client.StylistId = rdr.GetInt32(2);
                allClients.Add(client);
            }

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }

            return allClients;
        }


        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO Client (name, stylist_id) VALUES (@name, @stylistId);";
            MySqlParameter infoParam = new MySqlParameter();
            infoParam.ParameterName = "@name";
            infoParam.Value = Name;
            cmd.Parameters.Add(infoParam);
            MySqlParameter stylistIdParam = new MySqlParameter();
            stylistIdParam.ParameterName = "@stylistId";
            stylistIdParam.Value = StylistId;
            cmd.Parameters.Add(stylistIdParam);

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
           Delete("Client", id);
        }
    }
}