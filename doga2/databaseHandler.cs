using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace doga2
{
    public class databaseHandler
    {
        MySqlConnection connection;
        string tablename = "pek";
        public databaseHandler()
        {
            string username = "root";
            string password = "";
            string host = "localhost";
            string dbName = "pekaru";
            string connectionString = $"user={username}; password={password}; host={host}; database={dbName}";
            connection = new MySqlConnection(connectionString);
        }
        public void readAll()
        {
            try
            {
                connection.Open();
                string query = $"SELECT * FROM {tablename}";
                MySqlCommand command = new MySqlCommand(query,connection);
                MySqlDataReader read = command.ExecuteReader();
                while (read.Read())
                {
                    int id = read.GetInt32(read.GetOrdinal("id"));
                    string name = read.GetString(read.GetOrdinal("name"));
                    int db = read.GetInt32(read.GetOrdinal("db"));
                    int price = read.GetInt32(read.GetOrdinal("price"));
                    pekaru onePek = new pekaru();
                    onePek.id = id;
                    onePek.name = name;
                    onePek.db = db;
                    onePek.price = price;
                    pekaru.pekaruk.Add(onePek);
                }
                read.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:");
                
            }
        }
        public void writePek(pekaru onePek)
        {
            try
            {
                connection.Open();
                string query = $"INSERT INTO {tablename} (name,db,price) VALUES('{onePek.name}', '{onePek.db}', '{onePek.price}')";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:");
            }
        }
        public void deletePek(pekaru onePek)
        {
            try
            {
                connection.Open();
                string query = $"DELETE FROM {tablename} WHERE id = {onePek.id}";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                pekaru.pekaruk.Remove(onePek);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error:");
                
            }
        }
    }
}
