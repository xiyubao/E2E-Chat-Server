using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace E2E_Server
{
    public partial class MainWindow : Window
    {
        private MySqlConnection sqlconnection = null;
        private string serverip = "111.231.57.188";
        private string database_name = "E2E";
        private string username = "victor";
        private string password = "250413";
        private string table = "main";

        private void Connect_Database(object sender, RoutedEventArgs e)
        {
            if (sqlconnection == null)
            {
                sqlconnection = LoadSqlServer(database_name, username, password);
                if (sqlconnection != null)
                {
                    Light(true, Para.Light_Type.Data);
                    button2.Content = "Close DB";
                    Log("connect to database successfully");

                   //UpdateID();

                }
                else
                {
                    Log("can not connect to database");
                }
            }
            else
            {
                Light(false, Para.Light_Type.Data);
                button2.Content = "Connect DB";
            }
        }

        public MySqlConnection LoadSqlServer(string database, string username, string password)
        {

            String connectionString = "server=" + serverip + ";user id=" + username + ";pwd=" + password + ";database=" + database;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connectionString);
                conn.Open();
            }
            catch
            {
                conn = null;
                Console.WriteLine("error in MysqlConnection");
            }
            return conn;
        }

        public bool DatabaseLogin(string username, string password)
        {
            string str = "select * from " + table + " where (true)";
            //sql数据库

            MySqlCommand command = new MySqlCommand(str, sqlconnection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int index = 0;
                string user = reader[index++].ToString();
                string pass = reader[index++].ToString();
                if (user.Equals(username) && pass.Equals(password))
                    return true;
            }

            return false;
        }

        public bool DatabaseRegister(string username, string password)
        {
            if (DatabaseLogin(username, password))
            {
                Log(Line());
                return false;
            }
            string str = "insert into " + table + " values(" + username+ "," + password + ")";

            if (sqlconnection == null)
                return false;

            MySqlCommand command = new MySqlCommand(str, sqlconnection);
            command.CommandText = str;
            command.ExecuteNonQuery();

            return true;
        }

    }
}
