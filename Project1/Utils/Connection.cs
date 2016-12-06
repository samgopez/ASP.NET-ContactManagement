using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1.Utils
{
    public class Connection
    {
        public static MySqlConnection GetConnection()
        {
            MySqlConnection sqlConnection = new MySqlConnection();
            sqlConnection.ConnectionString = @"Server=localhost; Database=contact_db; Uid=root; Pwd=password";

            return sqlConnection;
        }
    }
}