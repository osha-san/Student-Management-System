using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using StudentManagementSystem.DAL;

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = null;
            MySqlDataReader reader = null;

            try
            {
                conn = Database.GetConnection();
                conn.Open();
                Console.WriteLine("✓ Database connected successfully!");
                Console.WriteLine($"Connection State: {conn.State}");
                Console.WriteLine($"Database: {conn.Database}");

                string query = "SELECT * FROM users WHERE username=@username AND user_password=@password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                Console.WriteLine($"Executing query: {query}");
                Console.WriteLine($"Username: {txtUsername.Text}");

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    conn.Close();
                    
                    this.Hide();
                   
                    new MainForm().Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
                Console.WriteLine($"Error Number: {ex.Number}");
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


    }
}
