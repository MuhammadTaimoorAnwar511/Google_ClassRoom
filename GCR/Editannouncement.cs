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
    public partial class Editannouncement : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private int announcementid;
        public Editannouncement()
        {
           
        }
        public Editannouncement(int classid, int teacherid,int announcementID)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            this.announcementid= announcementID;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //back button 
            Classdetails cl = new Classdetails(classID,teacherid);
            cl.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Addclass_Click(object sender, EventArgs e)
        {
            //update button
            try
            {
                string updateText = richTextBox1.Text; // Assuming you have a richTextBox1 control in your form where you input the new announcement text
                cn.Open();
                cm = new SqlCommand("UPDATE Announcement SET AnnouncementText = @text WHERE AnnouncementID = @id", cn);
                cm.Parameters.AddWithValue("@text", updateText);
                cm.Parameters.AddWithValue("@id", announcementid);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Announcement updated successfully!");
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            //move to calss detail page 
            Classdetails cl = new Classdetails(classID, teacherid);
            cl.Show();
            this.Hide();
        }

    }
}
