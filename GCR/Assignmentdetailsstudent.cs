using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i211130
{
    public partial class Assignmentdetailsstudent : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlCommand cm1 = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr,dr1;
        int studentid; // Variable to store the student ID
        int classid;
        int assignmentid;
        string downloadpaths = "";
        public Assignmentdetailsstudent()
        {
            
        }
        public Assignmentdetailsstudent(int classID,int studentID,int assignmentID)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentid = studentID;
            this.classid = classID;
            this.assignmentid = assignmentID;
            loaddataofassignment();
        }
        private void loaddataofassignment()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select * from Assignment WHERE AssignmentID = @Assignmentid", cn);
                cm.Parameters.AddWithValue("@Assignmentid", assignmentid);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    int yPosition = 10;
                    string date = dr["Deadline"].ToString();
                    string MaterialTitle = dr["Title"].ToString();
                    string MaterialTime = dr["timee"].ToString();
                    string MaterialID = dr["AssignmentID"].ToString();
                    string MaterialDescription = dr["Description"].ToString();
                    //label2.Text = MaterialID;
                    label11.Text = MaterialTitle;
                    label9.Text = date;
                    label8.Text = MaterialTime;
                    label12.Text = MaterialDescription;
                    dr.Close();
                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("Select * from attachmentt WHERE AssignmentID = @Assignmentid AND ClassID=@classid", cn);
                    cm.Parameters.AddWithValue("@Assignmentid", assignmentid);
                    cm.Parameters.AddWithValue("@classid", classid);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        do
                        {
                            
                            string AttachmentID = dr["AttachmentID"].ToString();
                            string SubmissionFilename = dr["SubmissionFilename"].ToString();
                            Panel announcementPanel = new Panel();
                            announcementPanel.Location = new Point(10, yPosition);
                            announcementPanel.Size = new Size(647, 50);
                            Button btn1 = new Button();
                            btn1.Text = "Download";
                            btn1.UseVisualStyleBackColor = true;
                            //btn1.BackColor = Color.Controle;
                            btn1.Location = new Point(400, 10);
                            btn1.Click += new System.EventHandler(this.savefile);
                            int announcementID = int.Parse(AttachmentID); // Replace with the actual ID value
                                                                          // button 
                            btn1.Tag = new { id = announcementID, classid = this.classid, studentid = this.studentid };
                            // rest of your code...
                            Label announcementLabel = new Label();
                            announcementLabel.Text = "Name of File : " + SubmissionFilename;
                            announcementLabel.AutoSize = true;
                            announcementLabel.Location = new Point(10, 10); // Adjust the position as needed
                            announcementPanel.Controls.Add(announcementLabel);
                            //Label announcementDate = new Label();
                            //announcementDate.Text = "Decription : " + MaterialDescription;
                            //announcementDate.AutoSize = true;
                            //announcementDate.Location = new Point(10, 10); // Adjust the position as needed
                            //Label announcementId = new Label();
                            //announcementId.Text = "ID : " + MaterialID;
                            //announcementId.AutoSize = true;
                            //announcementId.Location = new Point(10, 30); // Adjust the position as needed
                            //announcementPanel.Controls.Add(announcementId);
                            //announcementPanel.Controls.Add(announcementDate);
                            //announcementPanel.Controls.Add(btn1);
                            announcementPanel.Controls.Add(btn1);
                            panel2.Controls.Add(announcementPanel);
                            
                           
                            yPosition += announcementPanel.Height + 10; // Increment the Y position for the next panel

                        } while (dr.Read());
                    }



                }
                else
                {

                }

                //dr1.Close();
                dr.Close();
                cn.Close();
                cn.Open();
                
                Button btn2 = new Button();
                Button btn3 = new Button();
                btn2.Text = "Submit";
                btn3.Text = "Unsubmit";
                btn2.UseVisualStyleBackColor = true;
                btn3.UseVisualStyleBackColor = true;
                btn2.Location = new Point(604, 116);
                btn3.Location = new Point(604, 116);
                btn2.Click += new System.EventHandler(this.button2_Click);
                btn3.Click += new System.EventHandler(this.unsubmit);
                cm1 = new SqlCommand("Select * from Submission WHERE AssignmentID = @Assignmentid AND StudentID=@studentid", cn);
                cm1.Parameters.AddWithValue("@Assignmentid", assignmentid);
                cm1.Parameters.AddWithValue("@studentid", studentid);
                dr1 = cm1.ExecuteReader();
                dr1.Read();
                if (dr1.HasRows)
                {
                    string marks = dr1["MarksReceived"].ToString();
                    string AttachmentID = dr1["SubmissionID"].ToString();
                    int announcementID = int.Parse(AttachmentID);
                    //MessageBox.Show(AttachmentID);
                    btn3.Tag = new { id = announcementID, classid = this.classid, studentid = this.studentid };

                    panel1.Controls.Add(btn3);
                    Label marksrecieved = new Label();
                    marksrecieved.Text = "Marks : " + marks;
                    marksrecieved.AutoSize = true;
                    marksrecieved.Location = new Point(590, 90); // Adjust the position as needed
                    panel1.Controls.Add(marksrecieved);

                }
                else
                {
                    //btn2.Tag = new { id = announcementID, classid = this.classid, studentid = this.studentid };
                    panel1.Controls.Add(btn2);
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
        public void RetrieveFileDataFromDatabase(int fileId, string savePath)
        {
            // Prepare SQL query with parameters
            string selectQuery = "SELECT SubmissionFilename, SubmissionFile FROM attachmentt WHERE AttachmentID = @FileId";


            try
            {
                cn.Open();

                // Create the command and assign parameters
                using (SqlCommand command = new SqlCommand(selectQuery, cn))
                {
                    command.Parameters.AddWithValue("@FileId", fileId);

                    // Execute the command and retrieve the data reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get the file name and file data from the reader
                            string fileName = reader.GetString(0);
                            byte[] fileData = (byte[])reader.GetValue(1);

                            // Save the file data to disk
                            File.WriteAllBytes(Path.Combine(savePath, fileName), fileData);

                            Console.WriteLine("File retrieved and saved successfully!");
                            MessageBox.Show("File retrieved and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            Console.WriteLine("No file found with the specified ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving file data: " + ex.Message);
            }
        }
        private void getdownloadpath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Set the initial directory (optional)
            folderBrowserDialog.SelectedPath = "C:\\";

            // Show the FolderBrowserDialog and check if the user clicked the OK button
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected folder path
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                downloadpaths = selectedFolderPath;
                // Use the selectedFolderPath to download the file or perform any desired action
                Console.WriteLine("Selected Folder Path: " + selectedFolderPath);
            }
            else
            {
                // User cancelled the folder selection
                Console.WriteLine("Folder selection cancelled.");
            }

        }
        private void savefile(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int atID = buttonTag.id;
            int classid = buttonTag.classid;
            getdownloadpath();
            RetrieveFileDataFromDatabase(atID, downloadpaths);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //back
            Studentclassdetail cld = new Studentclassdetail(studentid,classid);
            cld.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Addnewsubmissionattachment submitassignment = new Addnewsubmissionattachment(classid,studentid,assignmentid);
            submitassignment.Show();
            this.Hide();
        }
        private void unsubmit(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int matID = buttonTag.id;
            int classid = buttonTag.classid;
            int teacherid = buttonTag.studentid;
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Submission WHERE SubmissionID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Assignmentdetailsstudent m = new Assignmentdetailsstudent(classid, teacherid, assignmentid);
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
    }
}
