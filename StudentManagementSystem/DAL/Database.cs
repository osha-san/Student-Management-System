using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StudentManagementSystem.DAL
{
    internal class Database
    {
        private static string connectionString = "server=localhost;database=student_management_system;uid=root;pwd=October22@2003";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }

}
