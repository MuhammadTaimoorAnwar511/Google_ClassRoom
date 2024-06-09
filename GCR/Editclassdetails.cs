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
    public partial class Editclassdetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        public Editclassdetails()
        {
            
        }
        public Editclassdetails(int classid, int teacherid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Classdetails cl = new Classdetails(classID , teacherid);
            cl.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //name
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //old code
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //new code
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Get the values from the text fields
            string className = textBox1.Text.Trim();

            string newClassCode = textBox3.Text.Trim();

            // Validate that the text fields are not empty
            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(newClassCode))
            {
                MessageBox.Show("Please enter a class name and new class code.");
                return;
            }

            // Check if the old class code matches the first before updating
            string oldClassCode = textBox2.Text.Trim();
            string sql = "SELECT ClassroomCode FROM Class WHERE ClassID = @classID";
            cm = new SqlCommand(sql, cn);
            cm.Parameters.AddWithValue("@classID", classID);
            cn.Open();
            dr = cm.ExecuteReader();

            if (dr.Read())
            {
                string existingClassCode = dr["ClassroomCode"].ToString();
                
                if (existingClassCode != oldClassCode)
                {
                    cn.Close();
                    MessageBox.Show("Old class code does not match.");
                    return;
                }
            }
            else
            {
                cn.Close();
                MessageBox.Show("Invalid class ID.");
                return;
            }

            cn.Close();

            // Update the class details in the database
            sql = "UPDATE Class SET ClassName = @className, ClassroomCode = @newClassCode WHERE ClassID = @classID";
            cm = new SqlCommand(sql, cn);
            cm.Parameters.AddWithValue("@className", className);
            cm.Parameters.AddWithValue("@newClassCode", newClassCode);
            cm.Parameters.AddWithValue("@classID", classID);

            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();

            MessageBox.Show("Class details updated successfully.");

            // Refresh the class details form
            Classdetails cl = new Classdetails(classID, teacherid);
            cl.Show();
            this.Hide();
        }


    }
}
