using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodCart
{
    public partial class Form3 : Form
    {
        private NpgsqlConnection connection;
        private Form mainForm;

        public Form3(Form mainForm, NpgsqlConnection existingConnection)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = existingConnection;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e) // Query
        {
            try
            {
                // Get the SQL query entered by the user
                string query = textBox1.Text;

                if (string.IsNullOrWhiteSpace(query))
                {
                    MessageBox.Show("Please enter a query.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!query.Trim().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Only SELECT queries are allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Execute the SQL query
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Bind the data to the DataGridView
                dataGridView1.DataSource = dataTable;

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"PostgreSQL Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {   
                // Ensure the connection is closed in all cases
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Main Form
        {
            mainForm.Show();
            this.Close();
        }

    }
}
