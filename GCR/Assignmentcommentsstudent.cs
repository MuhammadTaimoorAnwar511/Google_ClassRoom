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
    public partial class Assignmentcommentsstudent : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int studentid; // Variable to store the student ID
        int classid;
        int assignmentid;
        public Assignmentcommentsstudent()
        {

        }

        public Assignmentcommentsstudent(int studentID, int classID, int assignmnetID)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentid = studentID;
            this.classid = classID;
            this.assignmentid = assignmnetID;
            LoadComments();
        }

        private void LoadComments()
        {
            try
            {
                ///////////
                cn.Open();
                cm = new SqlCommand("Select * from Assignment WHERE AssignmentID = @Assignmentid", cn);
                cm.Parameters.AddWithValue("@Assignmentid", assignmentid);
                dr = cm.ExecuteReader();
                dr.Read();
                string date = dr["Deadline"].ToString();
                string MaterialTitle = dr["Title"].ToString();
                string MaterialTime = dr["timee"].ToString();
                string MaterialID = dr["AssignmentID"].ToString();
                string MaterialDescription = dr["Description"].ToString();
                label27.Text = MaterialTitle;
                label30.Text = date;
                label29.Text = MaterialTime;
                label1.Text = MaterialDescription;
                dr.Close();
                cn.Close();
                ///////////

                ///
                cn.Open();
                cm = new SqlCommand("SELECT c.CommentID, c.CommentText, c.CommentDate, u.Name AS UserName FROM Commenttt c INNER JOIN Userr u ON c.UserID = u.UserID WHERE c.AssignmentID = @assignmentID", cn);
                cm.Parameters.AddWithValue("@assignmentID", assignmentid);

                dr = cm.ExecuteReader();

                panel1.Controls.Clear(); // Clear existing controls in panel1

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
                    commentPanel.Size = new Size(panel1.ClientSize.Width - 20, 100); // Adjust the panel size as needed
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
                    lblUserName.Text = "User Name: " + userName;
                    lblUserName.AutoSize = true;
                    lblUserName.Font = new Font("Arial", 12, FontStyle.Regular);
                    lblUserName.Location = new Point(10, 70);

                    Button btnDelete = new Button();
                    btnDelete.Text = "Delete";
                    btnDelete.Tag = commentID; // Store the comment ID in the button's Tag property
                    btnDelete.Font = new Font("Arial", 10, FontStyle.Regular);
                    btnDelete.Location = new Point(commentPanel.Width - 80, 10);
                    btnDelete.Click += new EventHandler(DeleteComment_Click);

                    // Add the controls to the comment panel
                    commentPanel.Controls.Add(lblCommentID);
                    commentPanel.Controls.Add(lblCommentText);
                    commentPanel.Controls.Add(lblCommentDate);
                    commentPanel.Controls.Add(lblUserName);
                    commentPanel.Controls.Add(btnDelete);

                    // Add the comment panel to panel1
                    panel1.Controls.Add(commentPanel);

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


        private void DeleteComment_Click(object sender, EventArgs e)
        {
            // Get the comment ID from the button's Tag property
            int commentID = Convert.ToInt32(((Button)sender).Tag);

            try
            {
                cn.Open();
                cm = new SqlCommand("DELETE FROM Commenttt WHERE CommentID = @commentID AND UserID = @userID", cn);
                cm.Parameters.AddWithValue("@commentID", commentID);
                cm.Parameters.AddWithValue("@userID", studentid);
                int rowsAffected = cm.ExecuteNonQuery();
                cn.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Comment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadComments(); // Refresh the comments list
                }
                else
                {
                    MessageBox.Show("You are not authorized to delete this comment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //back button //
            Studentclassdetail cld = new Studentclassdetail(studentid, classid);
            cld.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //upper panel 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string commentText = textBox1.Text;

            try
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO Commenttt (UserID, AnnouncementID, AssignmentID, CommentText, CommentDate) VALUES (@userID, null, @assignmentID, @commentText, GETDATE())", cn);
                cm.Parameters.AddWithValue("@userID", studentid);
                cm.Parameters.AddWithValue("@assignmentID", assignmentid);
                cm.Parameters.AddWithValue("@commentText", commentText);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("Comment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadComments(); // Refresh the comments list
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //text 
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //label 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //lower panel 
        }


        private void Assignmentcommentsstudent_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //send button
            string commentText = textBox1.Text;

            try
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO Commenttt (UserID, AnnouncementID, AssignmentID,ClassID, CommentText, CommentDate) VALUES (@userID, null, @assignmentID,@class, @commentText, GETDATE())", cn);
                cm.Parameters.AddWithValue("@userID", studentid);
                cm.Parameters.AddWithValue("@assignmentID", assignmentid);
                cm.Parameters.AddWithValue("@commentText", commentText);

                cm.Parameters.AddWithValue("@class", classid);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("Comment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadComments(); // Refresh the comments list
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }
    }
}
