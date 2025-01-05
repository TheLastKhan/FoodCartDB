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
    public partial class Form5 : Form
    {
        private NpgsqlConnection connection;
        private Form mainForm;

        public Form5(Form mainForm, NpgsqlConnection existingConnection)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = existingConnection;
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e) // Add To Cart
        {
            string user_id = textBox1.Text.Trim();
            string meal_id = textBox2.Text.Trim();
            string quantity = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(meal_id) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Please fill in all fields before adding to the cart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO cart (user_id, meal_id, quantity) VALUES (@user_id, @meal_id, @quantity)";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@meal_id", meal_id);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully added to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No item was added to the cart. Please check your input.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button2_Click(object sender, EventArgs e) // Remove From Cart
        {
            string user_id = textBox1.Text.Trim();
            string meal_id = textBox2.Text.Trim();
            string quantity = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(user_id) || string.IsNullOrEmpty(meal_id) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Please fill in all fields before removing from the cart.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE FROM cart WHERE user_id = @user_id AND meal_id = @meal_id AND quantity = @quantity";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@meal_id", meal_id);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item successfully removed from the cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No item was removed from the cart. Please check your input.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
