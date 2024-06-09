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
    public partial class Studentclassdetail : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int studentid; // Variable to store the student ID
        int classid;
        public Studentclassdetail()
        {
        }
        public Studentclassdetail(int studentID, int classID)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentid = studentID;
            this.classid = classID;
            LoadAnnouncements();
            getmaterialdetail();
        }
        private void getmaterialdetail()
        {
            try
            {


                cn.Open();

                /////////////////working//
                cm = new SqlCommand("SELECT * FROM Class WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classID", classid);

                dr = cm.ExecuteReader();
                dr.Read();
                String classids = dr["ClassID"].ToString();
                String classname = dr["ClassName"].ToString();
                String classcode = dr["ClassroomCode"].ToString();
                label5.Text = "CLASS ID : " + classids;
                label1.Text = "CLASS NAME: " + classname;
                label2.Text = "CLASS CODE: " + classcode;
                ////////////////
                dr.Close();
                cn.Close();
                /////////////////



                cn.Open();
                cm = new SqlCommand("Select * from Material WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classID", classid);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    int yPosition = 10;

                    do
                    {
                        string date = dr["Date"].ToString();
                        string MaterialTitle = dr["MaterialTitle"].ToString();
                        string MaterialDescription = dr["MaterialDescription"].ToString();
                        string MaterialID = dr["MaterialID"].ToString();
                        Panel announcementPanel = new Panel();
                        announcementPanel.BackColor = Color.LightGreen;
                        announcementPanel.BorderStyle = BorderStyle.FixedSingle;
                        announcementPanel.Location = new Point(10, yPosition);
                        announcementPanel.Size = new Size(647, 100);
                        Button btn1 = new Button();
                        btn1.Text = "Details";
                        btn1.UseVisualStyleBackColor = true;
                        // btn1.BackColor = Color.Controle;
                        btn1.Location = new Point(400, 10);
                        btn1.Click += new System.EventHandler(this.detailsofmaterial);
                        int announcementID = int.Parse(MaterialID); // Replace with the actual ID value
                                                                    // button 
                        btn1.Tag = new { id = announcementID, classid = this.classid, studentid = this.studentid };
                        // rest of your code...
                        Label announcementLabel = new Label();
                        announcementLabel.Text = "Title : " + MaterialTitle;
                        announcementLabel.AutoSize = true;
                        announcementLabel.Location = new Point(10, 50); // Adjust the position as needed
                        announcementPanel.Controls.Add(announcementLabel);
                        Label announcementDate = new Label();
                        announcementDate.Text = "Decription : " + MaterialDescription;
                        announcementDate.AutoSize = true;
                        announcementDate.Location = new Point(10, 10); // Adjust the position as needed
                        Label announcementId = new Label();
                        announcementId.Text = "ID : " + MaterialID;
                        announcementId.AutoSize = true;
                        announcementId.Location = new Point(10, 30); // Adjust the position as needed
                        announcementPanel.Controls.Add(announcementId);
                        announcementPanel.Controls.Add(announcementDate);
                        announcementPanel.Controls.Add(btn1);
                        panel4.Controls.Add(announcementPanel);

                        yPosition += announcementPanel.Height + 10; // Increment the Y position for the next panel

                    } while (dr.Read());


                }
                else
                {

                }
                dr.Close();
                cn.Close();


            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void detailsofmaterial(object sender, EventArgs e)

        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int materialid = buttonTag.id;
            Materialdetailstudent materialdetails = new Materialdetailstudent(classid, studentid, materialid);
            materialdetails.Show();
            this.Hide();
        }
        private void LoadAnnouncements()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT a.*, u.Name AS TeacherName FROM Announcement a INNER JOIN Userr u ON a.TeacherID = u.UserID WHERE a.ClassID = @ClassID", cn);
                cm.Parameters.AddWithValue("@ClassID", classid);
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    int announcementID = Convert.ToInt32(dr["AnnouncementID"]);
                    string announcementText = dr["AnnouncementText"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Date"]);
                    string teacherName = dr["TeacherName"].ToString();

                    // Create a new panel for each announcement
                    Panel announcementPanel = new Panel();
                    announcementPanel.BorderStyle = BorderStyle.FixedSingle;
                    announcementPanel.Size = new Size(400, 120);
                    announcementPanel.BackColor = Color.LightYellow;
                    announcementPanel.Padding = new Padding(5); // Optional: Add padding to the panel

                    // Create and configure label for announcement ID
                    Label lblAnnouncementID = new Label();
                    lblAnnouncementID.Text = "Announcement ID: " + announcementID;
                    lblAnnouncementID.Location = new Point(10, 10);
                    lblAnnouncementID.AutoSize = true;
                    lblAnnouncementID.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    announcementPanel.Controls.Add(lblAnnouncementID);

                    // Create and configure label for announcement text
                    Label lblAnnouncementText = new Label();
                    lblAnnouncementText.Text = "Announcement Text: " + announcementText;
                    lblAnnouncementText.Location = new Point(10, 30);
                    lblAnnouncementText.AutoSize = true;
                    lblAnnouncementText.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    announcementPanel.Controls.Add(lblAnnouncementText);

                    // Create and configure label for date
                    Label lblDate = new Label();
                    lblDate.Text = "Date: " + date.ToString("yyyy-MM-dd");
                    lblDate.Location = new Point(10, 50);
                    lblDate.AutoSize = true;
                    lblDate.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    announcementPanel.Controls.Add(lblDate);

                    // Create and configure label for teacher name
                    Label lblTeacherName = new Label();
                    lblTeacherName.Text = "Teacher: " + teacherName;
                    lblTeacherName.Location = new Point(10, 70);
                    lblTeacherName.AutoSize = true;
                    lblTeacherName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    announcementPanel.Controls.Add(lblTeacherName);

                    // Create and configure comment button
                    Button btnComment = new Button();
                    btnComment.Text = "Comment";
                    btnComment.Location = new Point(announcementPanel.Width - 100, 50);
                    btnComment.Tag = announcementID; // Set the announcement ID as the button's tag for identifying the selected announcement
                    btnComment.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    btnComment.ForeColor = Color.Black;

                    btnComment.Click += btnComment_Click; // Assign the click event handler
                    announcementPanel.Controls.Add(btnComment);
                   

                    // Add the announcement panel to the main panel
                    panel3.Controls.Add(announcementPanel);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            
            }
            getassignmentdetail();
        }
        private void getassignmentdetail()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select * from Assignment WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classID", classid);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    int yPosition = 10;

                    do
                    {
                        string date = dr["Deadline"].ToString();
                        string MaterialTitle = dr["Title"].ToString();
                        string MaterialDescription = dr["timee"].ToString();
                        string MaterialID = dr["AssignmentID"].ToString();
                        Panel announcementPanel1 = new Panel();
                        announcementPanel1.BackColor = Color.LightGreen;
                        announcementPanel1.BorderStyle = BorderStyle.FixedSingle;
                        announcementPanel1.Location = new Point(10, yPosition);
                        announcementPanel1.Size = new Size(647, 100);
                        Button btn1 = new Button();
                        Button btn3 = new Button();
                        btn1.Text = "Details";
                        btn3.Text = "Comments";
                        btn1.UseVisualStyleBackColor = true;
                        btn3.UseVisualStyleBackColor = true;
                        // btn1.BackColor = Color.Controle;
                        btn1.Location = new Point(400, 10);
                        btn3.Location = new Point(550, 10);
                        btn1.Click += new System.EventHandler(this.button5_Click);
                        btn3.Click += new System.EventHandler(this.button3_Click);
                        int announcementID = int.Parse(MaterialID); // Replace with the actual ID value
                        // button 
                        btn1.Tag = new { id = announcementID, classid = this.classid, teacherid = this.studentid };
                        btn3.Tag = new { id = announcementID, classid = this.classid, teacherid = this.studentid };
                        // rest of your code...
                        Label announcementLabel = new Label();
                        announcementLabel.Text = "Title : " + MaterialTitle;
                        announcementLabel.AutoSize = true;
                        announcementLabel.Location = new Point(10, 50); // Adjust the position as needed
                        announcementPanel1.Controls.Add(announcementLabel);
                        Label announcementDate = new Label();
                        announcementDate.Text = "Date : " + date;
                        announcementDate.AutoSize = true;
                        announcementDate.Location = new Point(10, 30); // Adjust the position as needed
                        Label announcementTime = new Label();
                        announcementTime.Text = "Time : " + MaterialDescription;
                        announcementTime.AutoSize = true;
                        announcementTime.Location = new Point(100, 30); // Adjust the position as needed
                        Label announcementId = new Label();
                        announcementId.Text = "ID : " + MaterialID;
                        announcementId.AutoSize = true;
                        announcementId.Location = new Point(10, 10); // Adjust the position as needed
                        announcementPanel1.Controls.Add(announcementId);
                        announcementPanel1.Controls.Add(announcementDate);
                        announcementPanel1.Controls.Add(btn1);
                        announcementPanel1.Controls.Add(btn3);
                        panel2.Controls.Add(announcementPanel1);

                        yPosition += announcementPanel1.Height + 10; // Increment the Y position for the next panel

                    } while (dr.Read());
                }
                else
                {

                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //assignment comment
            Button commentlButton = (Button)sender;
            dynamic buttonTag = commentlButton.Tag;
            int assignmentid = buttonTag.id;
            Assignmentcommentsstudent sac = new Assignmentcommentsstudent(studentid, classid,assignmentid);
            sac.Show();

            // Optionally, you could hide the current form
            this.Hide();

        }
        private void btnComment_Click(object sender, EventArgs e)
        {
            // Get the button that was clicked
            Button btn = (Button)sender;

            // Get the announcement ID from the button's tag property
            int announcementID = (int)btn.Tag;

            // Here you can handle the comment functionality.
            // For example, you might open a new form to show and add comments for the specific announcement:
            Announcementcommentsstudent acf = new Announcementcommentsstudent(studentid, classid, announcementID);
            acf.Show();

            // Optionally, you could hide the current form
            this.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            //back  button 
            Studenthome cld = new Studenthome(studentid);
            cld.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
          
            //material detail button 
            Materialdetailstudent cld = new Materialdetailstudent(classid, studentid,2001);
            cld.Show();
            this.Hide();
        }



        private void button5_Click(object sender, EventArgs e)
        {
            //assignmnetdetailstudent

            Button detailButton = (Button)sender;
            dynamic buttonTag = detailButton.Tag;
            int assignmentid = buttonTag.id;

            Assignmentdetailsstudent ads = new Assignmentdetailsstudent(classid, studentid, assignmentid);
            ads.Show();
            this.Hide();
        }

    
        private void Studentclassdetail_Load(object sender, EventArgs e)
        {
          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //assignmentpanel
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
