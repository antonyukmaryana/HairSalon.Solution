using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
      private int _id;
      private string _info;
      private int _stylistId;

      public Client(string info, int stylistId, int id = 0)
      {
        _id = id;
        _stylistId = stylistId;
        _info = info;

      }

      public int GetId()
     {
       return _id;
     }

     public void SetId(int newId)
     {
       _id = newId;
     }

     public string GetInfo()
     {
       return _info;
     }

     public void SetInfo(string info)
     {
       _info = info;
     }


     public static List<Client> GetAll(int stylistId)
     {
       List<Client> allClients = new List<Client>{};
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * FROM Client where stylist_id = @stylistId;";
       MySqlParameter stylistIdParam = new MySqlParameter();
       stylistIdParam.ParameterName = "@stylistId";
       stylistIdParam.Value = stylistId;
       cmd.Parameters.Add(stylistIdParam);
       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

       while (rdr.Read())
       {
         Client client= new Client (rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(0));
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
       cmd.CommandText = @"INSERT INTO Client (info, stylist_id) VALUES (@info, @stylistId);";
       MySqlParameter infoParam = new MySqlParameter();
       infoParam.ParameterName = "@info";
       infoParam.Value = this._info;
       cmd.Parameters.Add(infoParam);
       MySqlParameter stylistIdParam = new MySqlParameter();
       stylistIdParam.ParameterName = "@stylistId";
       stylistIdParam.Value = this._stylistId;
       cmd.Parameters.Add(stylistIdParam);

       cmd.ExecuteNonQuery();
       _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }
    }
  }
