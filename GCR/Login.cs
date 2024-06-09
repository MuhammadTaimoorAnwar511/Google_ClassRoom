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
    public partial class Login : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;

        public Login()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Register cld = new Register();
            cld.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string email = textBox1.Text;
            string password = textBox2.Text;
            string role = "Teacher"; // Assuming role for GCR

            cn.Open();
            cm = new SqlCommand("SELECT * FROM Userr WHERE Email = @Email AND Password = @Password AND Role = @Role", cn);
            // int teacherid = (int)dr["UserID"];
            cm.Parameters.AddWithValue("@Email", email);
            cm.Parameters.AddWithValue("@Password", password);
            cm.Parameters.AddWithValue("@Role", role);

            dr = cm.ExecuteReader();

            if (dr.Read())
            {
                // Successful login
                int teacherid = (int)dr["UserID"];
                MessageBox.Show("Login successful!");
                GCR cld = new GCR(teacherid);
                cld.Show();
                this.Hide();
            }
            else
            {
                // Invalid credentials
                MessageBox.Show("Invalid login credentials. Please try again.");
            }

            dr.Close();
            cn.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {

            string email = textBox1.Text;
            string password = textBox2.Text;
            string role = "Student";

            cn.Open();
            cm = new SqlCommand("SELECT * FROM Userr WHERE Email = @Email AND Password = @Password AND Role = @Role", cn);
            cm.Parameters.AddWithValue("@Email", email);
            cm.Parameters.AddWithValue("@Password", password);
            cm.Parameters.AddWithValue("@Role", role);

            dr = cm.ExecuteReader();

            if (dr.Read())
            {
                // Successful login
                int studentId = (int)dr["UserID"];
                MessageBox.Show("Login successful!");
                Studenthome l = new Studenthome(studentId);
                l.Show();
                this.Hide();
            }
            else
            {
                // Invalid credentials
                MessageBox.Show("Invalid login credentials. Please try again.");
            }

            dr.Close();
            cn.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
