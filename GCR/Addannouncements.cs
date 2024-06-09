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
    public partial class Addannouncements : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        public Addannouncements()
        {
            
        }
        public Addannouncements(int classid, int teacherid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
        }

    

        private void button2_Click(object sender, EventArgs e)
        {
            //go back button 
            Classdetails cld = new Classdetails(classID,teacherid);
            cld.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //annoucement button
            string announcementText = richTextBox1.Text;
            DateTime currentDate = DateTime.Now;
            string query = "INSERT INTO Announcement(ClassID, TeacherID, AnnouncementText, Date) VALUES (@classID, @teacherID, @announcementText, @date);";

            // Checking if text is empty
            if (string.IsNullOrWhiteSpace(announcementText))
            {
                MessageBox.Show("Please enter the announcement text.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database connection and operations
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@classID", this.classID);
                    cmd.Parameters.AddWithValue("@teacherID", this.teacherid);
                    cmd.Parameters.AddWithValue("@announcementText", announcementText);
                    cmd.Parameters.AddWithValue("@date", currentDate);

                    cn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Announcement added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        richTextBox1.Clear(); // Clear the text box after successful insertion
                    }
                    else
                    {
                        MessageBox.Show("Error occurred while adding announcement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
            //back page 
            Classdetails cld = new Classdetails(classID, teacherid);
            cld.Show();
            this.Hide();
        }

    }
}
