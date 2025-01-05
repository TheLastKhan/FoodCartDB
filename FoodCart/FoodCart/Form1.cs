using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;

namespace FoodCart
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            connection = new NpgsqlConnection("Server=localhost;Database=FoodCartDB;User Id=postgres;Password=password;");
        }

        private void Form1_Load(object sender, EventArgs e)        
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Handle form closing event
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose(); // Release the resource completely
            }
            Application.Exit(); // Close all background processes
        }

        private void button1_Click(object sender, EventArgs e) // Connect
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    MessageBox.Show("Connection successful!");
                }
                else
                {
                    MessageBox.Show("Connection is already open.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Disconnect
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    MessageBox.Show("Connection closed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Connection is already closed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Test
        {
            try
            {
                connection.Open();
                MessageBox.Show("Connection was successfully tested!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection test failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e) // Refresh
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    // You can add code here to load data.
                    MessageBox.Show("Connection refreshed and data updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("You need to connect first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e) // Login
        {
            string user_email = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(user_email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("E-mail and password can not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT COUNT(*) FROM users WHERE user_email = @user_email AND password = @password";
            try
            {
                connection.Open(); // Here, the connection is opened but not closed
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_email", user_email);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Login successful!");
                    }
                    else
                    {
                        MessageBox.Show("Invalid E-mail or password!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close(); // Always close the connection
            }
        }

        private void button6_Click(object sender, EventArgs e) // Logout
        {
            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                textBox1.Clear();
                textBox2.Clear();

                MessageBox.Show("You have logged out.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e) // Users Form
        {
            Form2 Form2 = new Form2(this, connection);
            Form2.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e) // Query Form
        {
            Form3 Form3 = new Form3(this, connection);
            Form3.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e) // Favorites Form
        {
            Form4 Form4 = new Form4(this, connection);
            Form4.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e) // Cart Form
        {
            Form5 Form5 = new Form5(this, connection);
            Form5.Show();
            this.Hide();
        }
    }
}
