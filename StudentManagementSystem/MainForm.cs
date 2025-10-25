using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using StudentManagementSystem;
using StudentManagementSystem.DAL;

namespace StudentManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Configure DataGridView
            dataGridStudents.SelectionChanged += DataGridStudents_SelectionChanged;
        }
        private void DataGridStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridStudents.SelectedRows.Count > 0 )
            {
                DataGridViewRow row = dataGridStudents.SelectedRows[0];
                txtFirstName.Text = row.Cells["first_name"].Value?.ToString() ?? "" ;
                txtLastName.Text = row.Cells["last_name"].Value?.ToString() ?? "";
                txtAge.Text = row.Cells["age"].Value?.ToString() ?? "";
                txtCourse.Text = row.Cells["course"].Value?.ToString() ?? "";

            }
        }

        private bool ValidateFields()
        {
            // Check First Name
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the First Name.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            // Check Last Name
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter the Last Name.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            // Check Age
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Please enter the Age.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            // Validate Age is a number
            int age;
            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Age must be a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            // Validate Age range
            if (age < 1 || age > 150)
            {
                MessageBox.Show("Age must be between 1 and 150.", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            // Check Course
            if (string.IsNullOrWhiteSpace(txtCourse.Text))
            {
                MessageBox.Show("Please enter the Course.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourse.Focus();
                return false;
            }

            return true;
        }
        // Clear all textboxes
        private void ClearFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            txtCourse.Clear();
            txtFirstName.Focus();
        }


        private void LoadStudents()
        {
            try
            {
                var conn = Database.GetConnection();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM students", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridStudents.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Load Error: " + ex.Message);
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate fields first
                if (!ValidateFields())
                {
                    return; // Stop if validation fails
                }

                var conn = Database.GetConnection();
                conn.Open();
                string query = "INSERT INTO students (first_name, last_name, age, course) VALUES (@first_name, @last_name, @age, @course)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");

                conn.Close();
                MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadStudents();

             }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Add Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(" Update btn clicked");

                //check if row is selected

                if (dataGridStudents.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a student row to update.", "No Selection ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateFields())
                {
                    return; // Stop if validation fails
                }


                int id = Convert.ToInt32(dataGridStudents.SelectedRows[0].Cells["student_id"].Value);
                Console.WriteLine($"Selected ID: {id}");

                var conn = Database.GetConnection();
                conn.Open();

                string query = "UPDATE students SET first_name=@first_name, last_name=@last_name, age=@age, course=@course WHERE student_id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine($"Rows affected: {rowsAffected}");
               
                conn.Close();
                MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Update Error: " + ex.Message);
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("=== DELETE BUTTON CLICKED ===");

                // Check if a row is selected
                if (dataGridStudents.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a student row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dataGridStudents.SelectedRows[0].Cells["student_id"].Value);
                Console.WriteLine($"Selected ID to delete: {id}");

                var confirmResult = MessageBox.Show($"Are you sure you want to delete student ID {id}?",
                                          "Confirm Delete",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                    {
                        var conn = Database.GetConnection();
                        conn.Open();
                        string query = "DELETE FROM students WHERE student_id=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows affected: {rowsAffected}");

         
                        conn.Close();
                        MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      
                        ClearFields();
                        LoadStudents();
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Delete Error: " + ex.Message);
            }
        }
    }
}
