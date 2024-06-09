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

namespace i211130
{
    public partial class Register : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;

        public Register()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login cld = new Login();
            cld.Show();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //register button 

            // Get the input values from the text boxes
            string name = textBox3.Text;
            string email = textBox1.Text;
            string password = textBox2.Text;
            string role = comboBox1.Text;

            // Check if any of the input fields is empty
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            try
            {
                // Open the database connection
                cn.Open();

                // Set up the SQL command
                cm = new SqlCommand("INSERT INTO Userr (Name, Email, Password, Role) VALUES (@Name, @Email, @Password, @Role)", cn);

                // Add the parameter values
                cm.Parameters.AddWithValue("@Name", name);
                cm.Parameters.AddWithValue("@Email", email);
                cm.Parameters.AddWithValue("@Password", password);
                cm.Parameters.AddWithValue("@Role", role);

                // Execute the SQL command
                cm.ExecuteNonQuery();

                // Close the database connection
                cn.Close();

                MessageBox.Show(" User registered successfully. ");

                // Clear the input fields
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox1.SelectedIndex = -1;
                Login l = new Login();
                l.Show();
                this.Hide();
            }

            catch (Exception ex)
            {
                MessageBox.Show(" Error: " + ex.Message);
            }


        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
