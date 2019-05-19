using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class BaseEntity
    {
        private int _id;
        private string _name;

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public void SetValuesFromReader(MySqlDataReader rdr)
        {
            Id = rdr.GetInt32(0);
            Name = rdr.GetString(1);
        }

        public static void Update(string tableName, int id, string name)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE " + tableName + @" set name = @name where id = @id";

            MySqlParameter idParam = new MySqlParameter();
            idParam.ParameterName = "@id";
            idParam.Value = id;
            cmd.Parameters.Add(idParam);

            MySqlParameter nameParam = new MySqlParameter();
            nameParam.ParameterName = "@name";
            nameParam.Value = name;
            cmd.Parameters.Add(nameParam);


            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Delete(string tableName, int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM " + tableName + @" where id = @id";
            MySqlParameter idParam = new MySqlParameter();
            idParam.ParameterName = "@id";
            idParam.Value = id;
            cmd.Parameters.Add(idParam);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Populate(BaseEntity entity, string tableName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText =
                @"SELECT name FROM " + tableName + @" WHERE id = @id";
            MySqlParameter idParam = new MySqlParameter();
            idParam.ParameterName = "@id";
            idParam.Value = entity.Id;
            cmd.Parameters.Add(idParam);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                entity.Name = rdr.GetString(0);
            }

            rdr.Close();
        }
    }
}