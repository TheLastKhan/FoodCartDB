using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodCart
{
    public partial class Form2 : Form
    {
        private NpgsqlConnection connection;
        private Form mainForm;

        public Form2(Form mainForm, NpgsqlConnection existingConnection)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = existingConnection;
        }

        private void Form2_Load(object sender, EventArgs e) { }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            if (!mainForm.Visible)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Main Form
        {
            mainForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) // Add User
        {
            string fullname = textBox1.Text.Trim();
            string user_email = textBox2.Text.Trim();
            string user_phone_number = textBox3.Text.Trim();
            string password = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(user_email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string query = "INSERT INTO users (fullname, user_email, user_phone_number, password) VALUES (@fullname, @user_email, @user_phone_number, @password)";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fullname", fullname);
                    command.Parameters.AddWithValue("@user_email", user_email);
                    command.Parameters.AddWithValue("@user_phone_number", user_phone_number);
                    command.Parameters.AddWithValue("@password", password);

                    command.ExecuteNonQuery();
                    MessageBox.Show("User added successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Delete User
        {
            string user_email = textBox2.Text.Trim();
            string password = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(user_email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter your E-mail and password!");
                return;
            }

            string query = "DELETE FROM users WHERE user_email = @user_email AND password = @password";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", user_email);
                    command.Parameters.AddWithValue("@password", password);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("User not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) // Update User
        {
            string fullname = textBox1.Text.Trim();
            string user_email = textBox2.Text.Trim();
            string user_phone_number = textBox3.Text.Trim();
            string password = textBox4.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(user_email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            string query = "UPDATE users SET fullname = @fullname, user_phone_number = @user_phone_number WHERE user_email = @user_email AND password = @password";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fullname", fullname);
                    command.Parameters.AddWithValue("@user_phone_number", user_phone_number);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@user_email", user_email);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information has been successfully updated!");
                    }
                    else
                    {
                        MessageBox.Show("User not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) // List Users
        {
            string query = "SELECT * FROM users";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
