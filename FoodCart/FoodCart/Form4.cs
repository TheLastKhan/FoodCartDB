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
    public partial class Form4 : Form
    {
        private NpgsqlConnection connection;
        private Form mainForm;

        public Form4(Form mainForm, NpgsqlConnection existingConnection)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = existingConnection;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            // To close the entire application when the user clicks the close button:
            if (!mainForm.Visible) // If the main form is not visible:
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Add To Favorites
        {
            string user_id = textBox1.Text.Trim();
            string restaurant_id = textBox2.Text.Trim();
            string menu_id = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(restaurant_id) || string.IsNullOrEmpty(menu_id))
            {
                MessageBox.Show("Please fill in all fields before adding to favorites.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO favorites (user_id, restaurant_id, menu_id) VALUES (@user_id, @restaurant_id, @menu_id)";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@restaurant_id", restaurant_id);
                    command.Parameters.AddWithValue("@menu_id", menu_id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully added to favorites!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No item was added to favorites. Please check your input.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Remove From Favorites
        {
            string user_id = textBox1.Text.Trim();
            string restaurant_id = textBox2.Text.Trim();
            string menu_id = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(restaurant_id) || string.IsNullOrEmpty(menu_id))
            {
                MessageBox.Show("Please fill in all fields before removing from favorites.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE FROM favorites WHERE user_id = @user_id AND restaurant_id = @restaurant_id AND menu_id = @menu_id";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@restaurant_id", restaurant_id);
                    command.Parameters.AddWithValue("@menu_id", menu_id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully removed from favorites!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No item was removed from favorites. Please check your input.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Main Form
        {
            mainForm.Show();
            this.Close();
        }

    }
}
