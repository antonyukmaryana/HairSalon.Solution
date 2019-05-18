using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
      private string _name;
      private int _id;
      private string _description;


      public Stylist(string name, string description, int id = 0)
      {
        _id = id;
        _name = name;
        _description = description;

      }

      public int GetId()
     {
       return _id;
     }

     public void SetId(int newId)
     {
       _id = newId;
     }

     public string GetName()
     {
       return _name;
     }

     public void SetName(string newName)
     {
       _name = newName;
     }

     public string GetDescription()
     {
       return _description;
     }

     public void SetDescription(string newDescription)
     {
       _description = newDescription;
     }
     public static List<Stylist> GetAll()
     {
       List<Stylist> allStylists = new List<Stylist>{};
       MySqlConnection conn = DB.Connection();
       conn.Open();
       MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"SELECT * FROM Stylist;";
       MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

       while (rdr.Read())
       {
         int stylistId = rdr.GetInt32(0);
         string stylistName = rdr.GetString(1);
         string stylistDescription = rdr.GetString(2);

         Stylist stylist= new Stylist (stylistName, stylistDescription, stylistId);
         allStylists.Add(stylist);
       }

       conn.Close();

       if (conn != null)
       {
         conn.Dispose();
       }

       return allStylists;

     }

     public void Save()
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO Stylist (name, description) VALUES (@StylistName, @StylistDescription);";
       MySqlParameter name = new MySqlParameter();
       name.ParameterName = "@StylistName";
       name.Value = this._name;
       cmd.Parameters.Add(name);
       MySqlParameter description = new MySqlParameter();
       description.ParameterName = "@StylistDescription";
       description.Value = this._description;
       cmd.Parameters.Add(description);

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
