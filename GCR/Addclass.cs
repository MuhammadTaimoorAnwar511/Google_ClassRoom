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
    public partial class Addclass : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int teacherid;
        public Addclass()
        {
            
        }
        public Addclass(int teacherid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.teacherid = teacherid;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GCR cld = new GCR(teacherid);
            cld.Show();
            this.Hide();
        }
        //add function to create new class 
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter class name and classroom code.");
                return;
            }
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                cm = new SqlCommand("INSERT INTO Class (ClassName, ClassroomCode, TeacherID) VALUES (@className, @classroomCode, @teacherID)", cn);
                cm.Parameters.AddWithValue("@className", textBox1.Text);
                cm.Parameters.AddWithValue("@classroomCode", textBox2.Text);
                cm.Parameters.AddWithValue("@teacherID", teacherid);

                int rowsAffected = cm.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Class added successfully.");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Failed to add class.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
            //////////move back to GCR//
            GCR cld = new GCR(teacherid);
            cld.Show();
            this.Hide();

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
