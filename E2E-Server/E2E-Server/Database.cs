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
        private string database_name = "sensorverify";
        private string username = "victor";
        private string password = "250413";
        private string table = "train_table";
        private string validation_table = "validation_table";

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

        public MySqlConnection LoadSqlServer(String database, String username, String password)
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

    }
}
