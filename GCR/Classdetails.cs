using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i211130
{
    public partial class Classdetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        public Classdetails()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }
        public Classdetails(int classid, int teacherid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            ////

            ////

            try
            {

                ///////////////////
                cn.Open();
                cm = new SqlCommand("SELECT *from Class WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classId", classID);
                dr = cm.ExecuteReader();
                dr.Read();
                String classids = dr["ClassID"].ToString();
                String classname = dr["ClassName"].ToString();
                String classcode = dr["ClassroomCode"].ToString();
                label1.Text = "CLASS ID : " + classids;
                label2.Text = "CLASS NAME: " + classname;
                label3.Text = "CLASS CODE: " + classcode;
                dr.Close();
                cn.Close();
                /////////////////
                
                cn.Open();
                cm = new SqlCommand("Select * from Announcement WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classID", classid);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    int yPosition = 10;

                    do
                    {
                        string date = dr["Date"].ToString();
                        string announcement = dr["AnnouncementText"].ToString();
                        string id = dr["AnnouncementID"].ToString();
                        Panel announcementPanel = new Panel();
                        announcementPanel.BackColor = Color.LightGreen;
                        announcementPanel.BorderStyle = BorderStyle.FixedSingle;
                        announcementPanel.Location = new Point(10, yPosition);
                        announcementPanel.Size = new Size(647, 100);
                        Button btn1 = new Button();
                        Button btn2 = new Button();
                        Button btn3 = new Button();
                        btn1.Text = "Comments";
                        btn2.Text = "Edit";
                        btn3.Text = "Delete";
                        btn1.UseVisualStyleBackColor = true;
                        btn2.UseVisualStyleBackColor = true;
                        btn3.UseVisualStyleBackColor = true;
                        // btn1.BackColor = Color.Controle;
                        btn1.Location = new Point(400, 10);
                        btn2.Location = new Point(475, 10);
                        btn3.Location = new Point(550, 10);
                        btn1.Click += new System.EventHandler(this.commentsclick);
                        btn2.Click += new System.EventHandler(this.editclick);
                        btn3.Click += new System.EventHandler(this.deleteclick);
                        int announcementID = int.Parse(id); // Replace with the actual ID value
                                                            // button 
                        btn1.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                        btn3.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                        btn2.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                        // rest of your code...
                        Label announcementLabel = new Label();
                        announcementLabel.Text = announcement;
                        announcementLabel.AutoSize = true;
                        announcementLabel.Location = new Point(10, 50); // Adjust the position as needed
                        announcementPanel.Controls.Add(announcementLabel);
                        Label announcementDate = new Label();
                        announcementDate.Text = "Date : " + date;
                        announcementDate.AutoSize = true;
                        announcementDate.Location = new Point(10, 10); // Adjust the position as needed
                        Label announcementId = new Label();
                        announcementId.Text = "ID : " + id;
                        announcementId.AutoSize = true;
                        announcementId.Location = new Point(10, 30); // Adjust the position as needed
                        announcementPanel.Controls.Add(announcementId);
                        announcementPanel.Controls.Add(announcementDate);
                        announcementPanel.Controls.Add(btn1);
                        announcementPanel.Controls.Add(btn2);
                        announcementPanel.Controls.Add(btn3);
                        panelContainer.Controls.Add(announcementPanel);

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
            getmaterialdetail();
            getassignmentdetail();
            LoadStudentDetails();
        }
        ////////
        private void LoadStudentDetails()
        {
            try
            {
                //using (cn = new SqlConnection(dbcon.MyConnection()))
                //{
                    cn.Open();
                    cm = new SqlCommand("SELECT U.UserID, U.Name, U.Email FROM Userr U INNER JOIN Enrollment E ON U.UserID = E.StudentID WHERE E.ClassID = @classID", cn);
                    cm.Parameters.AddWithValue("@classID", classID);
                    dr = cm.ExecuteReader();

                    int yPosition = 10;

                    while (dr.Read())
                    {
                        string userId = dr["UserID"].ToString();
                        string name = dr["Name"].ToString();
                        string email = dr["Email"].ToString();

                        Panel studentPanel = new Panel();
                        studentPanel.BackColor = Color.LightBlue;
                        studentPanel.BorderStyle = BorderStyle.FixedSingle;
                        studentPanel.Location = new Point(10, yPosition);
                        studentPanel.Size = new Size(300, 100);

                        Label userIdLabel = new Label();
                        userIdLabel.Text = "User ID: " + userId;
                        userIdLabel.AutoSize = true;
                        userIdLabel.Location = new Point(10, 10);
                        studentPanel.Controls.Add(userIdLabel);

                        Label nameLabel = new Label();
                        nameLabel.Text = "Name: " + name;
                        nameLabel.AutoSize = true;
                        nameLabel.Location = new Point(10, 30);
                        studentPanel.Controls.Add(nameLabel);

                        Label emailLabel = new Label();
                        emailLabel.Text = "Email: " + email;
                        emailLabel.AutoSize = true;
                        emailLabel.Location = new Point(10, 50);
                        studentPanel.Controls.Add(emailLabel);

                        panel4.Controls.Add(studentPanel);
                        yPosition += studentPanel.Height + 10;
                    }

                    dr.Close();
                cn.Close();
                }
            //}
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        ///////


        private void getmaterialdetail()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select * from Material WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classID", classID);
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
                        Button btn3 = new Button();
                        btn1.Text = "Details";
                        btn3.Text = "Delete";
                        btn1.UseVisualStyleBackColor = true;
                        btn3.UseVisualStyleBackColor = true;
                        // btn1.BackColor = Color.Controle;
                        btn1.Location = new Point(400, 10);
                        btn3.Location = new Point(550, 10);
                        btn1.Click += new System.EventHandler(this.detailsofmaterial);
                        btn3.Click += new System.EventHandler(this.deleteclickmaterial);
                        int announcementID = int.Parse(MaterialID); // Replace with the actual ID value
                                                            // button 
                        btn1.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                        btn3.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
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
                        announcementPanel.Controls.Add(btn3);
                        panel3.Controls.Add(announcementPanel);

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
            Materialdetails materialdetails = new Materialdetails(classID,teacherid, materialid);
            materialdetails.Show();
            this.Hide();
        }
        private void detailsofassignment(object sender, EventArgs e)

        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int assignmentid = buttonTag.id;
            Assignmentdetails materialdetails = new Assignmentdetails(classID, teacherid, assignmentid);
            materialdetails.Show();
            this.Hide();
        }
        private void getassignmentdetail()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select * from Assignment WHERE ClassID = @classID", cn);
                cm.Parameters.AddWithValue("@classID", classID);
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
                        Panel announcementPanel = new Panel();
                        announcementPanel.BackColor = Color.LightGreen;
                        announcementPanel.BorderStyle = BorderStyle.FixedSingle;
                        announcementPanel.Location = new Point(10, yPosition);
                        announcementPanel.Size = new Size(647, 100);
                        Button btn1 = new Button();
                        Button btn3 = new Button();
                        btn1.Text = "Details";
                        btn3.Text = "Delete";
                        btn1.UseVisualStyleBackColor = true;
                        btn3.UseVisualStyleBackColor = true;
                        // btn1.BackColor = Color.Controle;
                        btn1.Location = new Point(400, 10);
                        btn3.Location = new Point(550, 10);
                        btn1.Click += new System.EventHandler(this.detailsofassignment);
                        btn3.Click += new System.EventHandler(this.deleteclickassignment);
                        int announcementID = int.Parse(MaterialID); // Replace with the actual ID value
                                                                    // button 
                        btn1.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                        btn3.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                        // rest of your code...
                        Label announcementLabel = new Label();
                        announcementLabel.Text = "Title : " + MaterialTitle;
                        announcementLabel.AutoSize = true;
                        announcementLabel.Location = new Point(10, 50); // Adjust the position as needed
                        announcementPanel.Controls.Add(announcementLabel);
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
                        announcementPanel.Controls.Add(announcementId);
                        announcementPanel.Controls.Add(announcementDate);
                        announcementPanel.Controls.Add(btn1);
                        announcementPanel.Controls.Add(btn3);
                        panel2.Controls.Add(announcementPanel);

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
       
        private void deleteattach(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM attachmentt WHERE MaterialID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletemat(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Material WHERE MaterialID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Classdetails m = new Classdetails(classID, teacherid);
                m.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deleteclickmaterial(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int matID = buttonTag.id;
            int classid = buttonTag.classid;
            int teacherid = buttonTag.teacherid;
            deleteattach(matID);
            deletemat(matID);
        }
        private void deleteattachassignment(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM attachmentt WHERE AssignmentID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletematassignment(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Assignment WHERE AssignmentID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Classdetails m = new Classdetails(classID, teacherid);
                m.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletecommentassignment(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Commenttt WHERE AssignmentID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                //MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Classdetails m = new Classdetails(classID, teacherid);
                //m.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletesubmission(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Submission WHERE AssignmentID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                //MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Classdetails m = new Classdetails(classID, teacherid);
                //m.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deleteclickassignment(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int matID = buttonTag.id;
            int classid = buttonTag.classid;
            int teacherid = buttonTag.teacherid;
            deleteattachassignment(matID);
            deletecommentassignment(matID);
            deletesubmission(matID);
            deletematassignment(matID);
        }

        //edit
        private void button1_Click(object sender, EventArgs e)
        {
            Editclassdetails ed = new Editclassdetails(classID, teacherid);
            ed.Show();
            this.Hide();
        }
        //add
        private void button2_Click(object sender, EventArgs e)
        {
            Addannouncements a = new Addannouncements( classID, teacherid);
            a.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Editannouncement ea = new Editannouncement();
            ea.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddClassmaterial a = new AddClassmaterial(classID,teacherid);
            a.Show();
            this.Hide();
        }
        private void commentsclick(object sender, EventArgs e)
        {
            //comment 
            Button commentsButton = (Button)sender;
            dynamic buttonTag = commentsButton.Tag;
            int announcementID = buttonTag.id;

            Announcementcommentsteacher a = new Announcementcommentsteacher(classID, teacherid, announcementID);
            a.Show();
            this.Hide();
        }
        private void editclick(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            dynamic buttonTag = editButton.Tag;
            int announcementID = buttonTag.id;

            Editannouncement a = new Editannouncement(classID,teacherid,announcementID);

            a.Show();
            this.Hide();

        }
        private void deleteannouncecomments(int id, int classid, int teacherid)
        {
            cn.Open();
            try
            {

                cm = new SqlCommand("DELETE FROM Commenttt WHERE AnnouncementID = @id ", cn);
                cm.Parameters.AddWithValue("@id", id);
                cm.Parameters.AddWithValue("@classid", classid);
                cm.Parameters.AddWithValue("@teacherid", teacherid);
                cm.ExecuteNonQuery();
                //MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Classdetails m = new Classdetails(classID, teacherid);
                //m.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        
        private void DeleteAnnouncement(int id, int classid, int teacherid)
        {
            deleteannouncecomments(id,classid,teacherid);
            cn.Open();
            try
            {
                
                cm = new SqlCommand("DELETE FROM Announcement WHERE AnnouncementID = @id AND ClassID = @classid AND TeacherID = @teacherid", cn);
                cm.Parameters.AddWithValue("@id", id);
                cm.Parameters.AddWithValue("@classid", classid);
                cm.Parameters.AddWithValue("@teacherid", teacherid);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Classdetails m = new Classdetails(classID, teacherid);
                m.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }

        private void deleteclick(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int announcementID = buttonTag.id;
            int classid = buttonTag.classid;
            int teacherid = buttonTag.teacherid;

            // Call a function or perform any action with the announcementID
            DeleteAnnouncement(announcementID, classid, teacherid);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Materialdetails m = new Materialdetails();
            m.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Editassignmentdetails eda = new Editassignmentdetails();
            eda.Show();
            this.Hide();
        }

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    Assignmentdetails a = new Assignmentdetails(classID, teacherid);
        //    a.Show();
        //    this.Hide();
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            Addassignment a = new Addassignment(classID,teacherid);
            a.Show();
            this.Hide();
        }

        private void Classdetails_Load(object sender, EventArgs e)
        {

        }
        //go back id 
        private void button13_Click(object sender, EventArgs e)
        {
            GCR cld = new GCR(teacherid);
            cld.Show();
            this.Hide();
        }


        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            //below panel
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            //tabpage
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //upperpanel
        }
    }
}
