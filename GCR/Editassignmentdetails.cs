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
    public partial class Editassignmentdetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private int assignmentid;
        public Editassignmentdetails()
        {
            InitializeComponent();
        }
        public Editassignmentdetails(int classid, int teacherid, int assignmentid)
        {

            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            this.assignmentid = assignmentid;
            //loaddataofassignment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Assignmentdetails cld = new Assignmentdetails(classID,teacherid,assignmentid);
            cld.Show();
            this.Hide();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            
            string insertQuery = "UPDATE Assignment\r\nSET Title = @Title, Description = @Description,Deadline=@Deadline,timee=@Timee,TotalMarks=@TotalMarks \r\nWHERE AssignmentID =@Assignmentid;";
            cn.Open();
            try
            {
             

                // Create the command and assign parameters
                using (SqlCommand command = new SqlCommand(insertQuery, cn))
                {
                    command.Parameters.AddWithValue("@classID", classID);
                    command.Parameters.AddWithValue("@Title", textBox1.Text);
                    command.Parameters.AddWithValue("@Deadline", textBox2.Text);
                    command.Parameters.AddWithValue("@Timee", textBox3.Text);
                    command.Parameters.AddWithValue("@TotalMarks", int.Parse(textBox4.Text));
                    command.Parameters.AddWithValue("@Description", richTextBox1.Text);
                    command.Parameters.AddWithValue("@Assignmentid", assignmentid);

                    // Execute the command
                    command.ExecuteNonQuery();
                }

                //Console.WriteLine("File uploaded successfully!");
                MessageBox.Show("Updated material");
                Assignmentdetails materialdetails = new Assignmentdetails(classID, teacherid, assignmentid);
                materialdetails.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                Console.WriteLine("Error uploading file: " + ex.Message);
            }
            cn.Close();
        }
    }
    
}
