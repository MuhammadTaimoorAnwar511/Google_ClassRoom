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
    public partial class Assignmentdetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private int assignmentid;
        string downloadpaths = "";
        public Assignmentdetails()
        {

        }

        public Assignmentdetails(int classid, int teacherid, int assignmentid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            this.assignmentid = assignmentid;
            LoadComments();
            loaddataofassignment();
            getnoofsubmissions();
            report();
            loadgrading();
            getallsubmissions();
        }

        private void getallsubmissions()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Submission WHERE AssignmentID = @assignmentid", cn);
                cm.Parameters.AddWithValue("@assignmentid", assignmentid);
                dr = cm.ExecuteReader();

                int yPosition = 10;

                while (dr.Read())
                {
                    string submissionid = dr["SubmissionID"].ToString();
                    string marks = dr["MarksReceived"].ToString();
                    string timeofsub = dr["SubmissionTime"].ToString();
                    string dateofsub = dr["SubmissionDate"].ToString();
                    string roll = dr["StudentID"].ToString();
                    int getmarks = int.Parse(marks);
                    int subid = int.Parse(submissionid);
                    Panel gradePanel = new Panel();
                    gradePanel.BackColor = Color.LightGreen;
                    gradePanel.BorderStyle = BorderStyle.FixedSingle;
                    gradePanel.Location = new Point(10, yPosition);
                    gradePanel.Size = new Size(300, 300);

                    Label gradeLabel = new Label();
                    gradeLabel.Text = "StudentID: "+roll;
                    gradeLabel.AutoSize = true;
                    gradeLabel.Location = new Point(10, 30);
                    gradePanel.Controls.Add(gradeLabel);

                    Label grade = new Label();
                    grade.Text = "Marks Received: " +marks;
                    grade.AutoSize = true;
                    grade.Location = new Point(10, 70);
                    gradePanel.Controls.Add(grade);
                    Label subdate = new Label();
                    subdate.Text = "Time: " + dateofsub;
                    subdate.AutoSize = true;
                    subdate.Location = new Point(10, 110);
                    gradePanel.Controls.Add(subdate);

                    Button btn1 = new Button();
                    btn1.Text = "Download";
                    btn1.UseVisualStyleBackColor = true;
                    btn1.Location = new Point(130, 150);
                    btn1.Click += new System.EventHandler(this.downloadthesubmission);

                    // Store the student ID and marks obtained in the button's tag
                    btn1.Tag = new
                    {
                        studentid = subid,
                        marksobtained = getmarks
                    };

                    gradePanel.Controls.Add(btn1);
                    panel4.Controls.Add(gradePanel);

                    yPosition += gradePanel.Height + 10; // Increment the Y position for the next panel
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
        private void downloadthesubmission(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int sID = buttonTag.studentid;
            getdownloadpath();
            RetrieveFileDataFromDatabase(sID, downloadpaths);

        }
        public void RetrieveFileDataFromDatabase(int fileId, string savePath)
        {
            // Prepare SQL query with parameters
            string selectQuery = "SELECT SubmissionFilename, SubmissionFile FROM Submission WHERE SubmissionID = @FileId";


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
            int teacherid = buttonTag.teacherid;
        }
        private void loadgrading()
        {

            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Submission WHERE AssignmentID = @assignmentid", cn);
                cm.Parameters.AddWithValue("@assignmentid", assignmentid);
                dr = cm.ExecuteReader();

                int yPosition = 10;

                while (dr.Read())
                {
                    Panel gradePanel = new Panel();
                    gradePanel.BackColor = Color.LightGreen;
                    gradePanel.BorderStyle = BorderStyle.FixedSingle;
                    gradePanel.Location = new Point(10, yPosition);
                    gradePanel.Size = new Size(300, 300);

                    Label gradeLabel = new Label();
                    gradeLabel.Text = "Roll Number: ";
                    gradeLabel.AutoSize = true;
                    gradeLabel.Location = new Point(10, 30);
                    gradePanel.Controls.Add(gradeLabel);

                    TextBox t1 = new TextBox();
                    t1.Location = new Point(150, 30);
                    gradePanel.Controls.Add(t1);

                    Label grade = new Label();
                    grade.Text = "Marks Received: ";
                    grade.AutoSize = true;
                    grade.Location = new Point(10, 70);
                    gradePanel.Controls.Add(grade);

                    TextBox t2 = new TextBox();
                    t2.Location = new Point(150, 70);
                    gradePanel.Controls.Add(t2);

                    Button btn1 = new Button();
                    btn1.Text = "Grade";
                    btn1.UseVisualStyleBackColor = true;
                    btn1.Location = new Point(130, 150);
                    btn1.Click += new System.EventHandler(this.button100_Click);

                    // Store the student ID and marks obtained in the button's tag
                    btn1.Tag = new
                    {
                        studentid = t1,
                        marksobtained = t2
                    };

                    gradePanel.Controls.Add(btn1);
                    panel2.Controls.Add(gradePanel);

                    yPosition += gradePanel.Height + 10; // Increment the Y position for the next panel
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

        private void getnoofsubmissions()
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

                    label4.Text =  "Submissions : "+dr["TotalSubmissions"].ToString();

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
                    label2.Text = MaterialID;
                    label13.Text = MaterialTitle;
                    label14.Text = date;
                    label11.Text = MaterialTime;
                    label21.Text = MaterialDescription;
                    dr.Close();
                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("Select * from attachmentt WHERE AssignmentID = @Assignmentid AND ClassID=@classid", cn);
                    cm.Parameters.AddWithValue("@Assignmentid", assignmentid);
                    cm.Parameters.AddWithValue("@classid", classID);
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
                            Button btn3 = new Button();
                            btn1.Text = "Download";
                            btn3.Text = "Delete";
                            btn1.UseVisualStyleBackColor = true;
                            btn3.UseVisualStyleBackColor = true;
                            //btn1.BackColor = Color.Controle;
                            btn3.Location = new Point(400, 10);
                            //btn1.Location = new Point(500, 10);
                            //btn1.Click += new System.EventHandler(this.savefile);
                            btn3.Click += new System.EventHandler(this.deleteattachment);
                            int announcementID = int.Parse(AttachmentID); // Replace with the actual ID value
                                                                          // button 
                            btn1.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
                            btn3.Tag = new { id = announcementID, classid = this.classID, teacherid = this.teacherid };
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
                            announcementPanel.Controls.Add(btn3);
                            panel5.Controls.Add(announcementPanel);

                            yPosition += announcementPanel.Height + 10; // Increment the Y position for the next panel

                        } while (dr.Read());
                    }



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
        private void deleteattachment(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int atID = buttonTag.id;
            int classid = buttonTag.classid;
            int teacherid = buttonTag.teacherid;
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM attachmentt WHERE AttachmentID = @id", cn);
                cm.Parameters.AddWithValue("@id", atID);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Assignmentdetails m = new Assignmentdetails(classID, teacherid, assignmentid);
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
        private void LoadComments()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT c.CommentID, c.CommentText, c.CommentDate, u.Name AS UserName " +
                                     "FROM Commenttt c " +
                                     "JOIN Userr u ON c.UserID = u.UserID " +
                                     "WHERE c.AssignmentID = @assignmentID", cn);
                cm.Parameters.AddWithValue("@assignmentID", assignmentid);

                dr = cm.ExecuteReader();

                panel6.Controls.Clear(); // Clear existing controls in panel4

                int yPos = 10; // Starting position for the comments

                while (dr.Read())
                {
                    int commentID = Convert.ToInt32(dr["CommentID"]);
                    string commentText = dr["CommentText"].ToString();
                    DateTime commentDate = Convert.ToDateTime(dr["CommentDate"]);
                    string userName = dr["UserName"].ToString();

                    // Create a panel for each comment
                    Panel commentPanel = new Panel();
                    commentPanel.BackColor = Color.LightGreen;
                    commentPanel.BorderStyle = BorderStyle.FixedSingle;
                    commentPanel.Size = new Size(panel6.ClientSize.Width - 20, 90);
                    commentPanel.Location = new Point(10, yPos);

                    // Create labels for each comment attribute and add them to the comment panel
                    Label lblCommentID = new Label();
                    lblCommentID.Text = "Comment ID: " + commentID;
                    lblCommentID.AutoSize = true;
                    lblCommentID.Font = new Font("Arial", 12, FontStyle.Regular);
                    lblCommentID.Location = new Point(10, 10);

                    Label lblCommentText = new Label();
                    lblCommentText.Text = "Comment: " + commentText;
                    lblCommentText.AutoSize = true;
                    lblCommentText.Font = new Font("Arial", 12, FontStyle.Regular);
                    lblCommentText.Location = new Point(10, 30);

                    Label lblCommentDate = new Label();
                    lblCommentDate.Text = "Date: " + commentDate.ToString("yyyy-MM-dd");
                    lblCommentDate.AutoSize = true;
                    lblCommentDate.Font = new Font("Arial", 12, FontStyle.Regular);
                    lblCommentDate.Location = new Point(10, 50);

                    Label lblUserName = new Label();
                    lblUserName.Text = "User: " + userName;
                    lblUserName.AutoSize = true;
                    lblUserName.Font = new Font("Arial", 12, FontStyle.Regular);
                    lblUserName.Location = new Point(10, 70);

                    // Create a delete button for each comment and add it to the comment panel
                    Button deleteButton = new Button();
                    deleteButton.Text = "Delete";
                    deleteButton.Size = new Size(60, 30);
                    deleteButton.Location = new Point(commentPanel.ClientSize.Width - deleteButton.Width - 10, (commentPanel.ClientSize.Height - deleteButton.Height) / 2);
                    deleteButton.Tag = commentID; // Set the comment ID as the button's Tag to identify it when clicked
                    deleteButton.Click += DeleteButton_Click;

                    // Add the controls to the comment panel
                    commentPanel.Controls.Add(lblCommentID);
                    commentPanel.Controls.Add(lblCommentText);
                    commentPanel.Controls.Add(lblCommentDate);
                    commentPanel.Controls.Add(lblUserName);
                    commentPanel.Controls.Add(deleteButton);

                    // Add the comment panel to panel4
                    panel6.Controls.Add(commentPanel);

                    yPos += commentPanel.Height + 10; // Increase the vertical position for the next comment panel
                }

                dr.Close();
                cn.Close();
            }

            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void report()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM Submission WHERE AssignmentID = @assignmentid", cn);
                cm.Parameters.AddWithValue("@assignmentid",assignmentid);
                dr = cm.ExecuteReader();

                int yPosition = 10;

                while (dr.Read())
                {
                    string assm = dr["StudentID"].ToString();
                    string subm = dr["SubmissionID"].ToString();
                    string date = dr["SubmissionDate"].ToString();
                    string mk = dr["MarksReceived"].ToString();
                    int assign = int.Parse(assm);
                    int submit = int.Parse(subm);
                    int marks = int.Parse(mk);
                    Panel gradePanel = new Panel();
                    gradePanel.BackColor = Color.LightGreen;
                    gradePanel.BorderStyle = BorderStyle.FixedSingle;
                    gradePanel.Location = new Point(10, yPosition);
                    gradePanel.Size = new Size(400, 200);

                    Label gradeLabel = new Label();
                    gradeLabel.Text = "Roll Number : " + assm;
                    gradeLabel.AutoSize = true;
                    gradeLabel.Location = new Point(10, 30);
                    gradePanel.Controls.Add(gradeLabel);


                    Label grade = new Label();
                    grade.Text = "Submission ID: " + subm;
                    grade.AutoSize = true;
                    grade.Location = new Point(100, 30);
                    gradePanel.Controls.Add(grade);

                    Label grade1 = new Label();
                    grade1.Text = "Submission Date : " + date;
                    grade1.AutoSize = true;
                    grade1.Location = new Point(10, 70);
                    gradePanel.Controls.Add(grade1);

                    Label grade2 = new Label();
                    grade2.Text = "Marks obtained : " + mk;
                    grade2.AutoSize = true;
                    grade2.Location = new Point(10, 120);
                    gradePanel.Controls.Add(grade2);

                    panel7.Controls.Add(gradePanel);

                    yPosition += gradePanel.Height + 10; // Increment the Y position for the next panel
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
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // Handle the delete button click event
            Button deleteButton = (Button)sender;
            int commentID = (int)deleteButton.Tag;

            // Check if the comment belongs to the current user
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT UserID FROM Commenttt WHERE CommentID = @commentID", cn);
                cm.Parameters.AddWithValue("@commentID", commentID);
                int commentUserID = Convert.ToInt32(cm.ExecuteScalar());
                cn.Close();

                // Compare the comment's user ID with the current user's ID
                if (commentUserID == teacherid)
                {
                    // Delete the comment from the database using the comment ID
                    try
                    {
                        cn.Open();
                        cm = new SqlCommand("DELETE FROM Commenttt WHERE CommentID = @commentID", cn);
                        cm.Parameters.AddWithValue("@commentID", commentID);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        // Refresh the comments after deleting one
                        LoadComments();
                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You can only delete your own comments.", "Unauthorized Deletion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void AddComment(string commentText)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO Commenttt (UserID, AssignmentID,ClassID, CommentText, CommentDate) VALUES (@userID, @assignmentID, @class,@commentText, @commentDate)", cn);
                cm.Parameters.AddWithValue("@userID", teacherid); // Provide the appropriate user ID
                cm.Parameters.AddWithValue("@assignmentID", assignmentid);
                cm.Parameters.AddWithValue("@commentText", commentText);
                cm.Parameters.AddWithValue("@commentDate", DateTime.Now);

                cm.Parameters.AddWithValue("@class", classID);

                cm.ExecuteNonQuery();

                cn.Close();

                // Refresh the comments after adding a new one
                LoadComments();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ///
        private void button100_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();

                // Retrieve the student ID and marks obtained from the button's tag
                Button gradeButton = (Button)sender;
                TextBox t1 = ((dynamic)gradeButton.Tag).studentid;
                TextBox t2 = ((dynamic)gradeButton.Tag).marksobtained;
                int studentid = int.Parse(t1.Text);
                int marksobtained = int.Parse(t2.Text);

                cm = new SqlCommand("UPDATE Submission SET MarksReceived = @marksreceived WHERE StudentID = @studentid AND AssignmentID = @assignmentid", cn);
                cm.Parameters.AddWithValue("@marksreceived", marksobtained);
                cm.Parameters.AddWithValue("@studentid", studentid);
                cm.Parameters.AddWithValue("@assignmentid", assignmentid);
                cm.ExecuteNonQuery();

                // Display a success message to the user
                MessageBox.Show("Grade added successfully!");

                // Clear the textboxes
                t1.Text = "";
                t2.Text = "";

                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ///
        
        ///
        private void button1_Click(object sender, EventArgs e)
        {
            //edit button 
            Editassignmentdetails eda = new Editassignmentdetails(classID, teacherid, assignmentid);
            eda.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //go back 
            Classdetails cld = new Classdetails(classID, teacherid);
            cld.Show();
            this.Hide();
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
            //lower panel 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //upper panel 
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            //below panel
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //text
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //button
            string commentText = textBox2.Text;

            if (!string.IsNullOrEmpty(commentText))
            {
                AddComment(commentText);
                textBox2.Text = ""; // Clear the comment text box after adding the comment
            }
        }
    


        private void Assignmentdetails_Load(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            //comment page 

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {
            //submission

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Addnewattachmentinassignment addnewattachmentinassignment = new Addnewattachmentinassignment(classID, teacherid, assignmentid);
            addnewattachmentinassignment.Show();
            this.Hide();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //GRADING PANEL
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
