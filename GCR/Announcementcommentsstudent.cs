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
    public partial class Announcementcommentsstudent : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int studentid; // Variable to store the student ID
        int classid;
        int announcementid;

        public Announcementcommentsstudent()
        {

        }
        public Announcementcommentsstudent(int studentID, int classID, int announcementID)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentid = studentID;
            this.classid = classID;
            this.announcementid = announcementID;
            LoadComments();
        }
        private void LoadComments()
        {
            panel1.Controls.Clear(); // Clear any existing comment controls

            try
            {


                ///////////////////
                cn.Open(); // Open the database connection
                cm = new SqlCommand("SELECT * FROM Announcement WHERE AnnouncementID = @announcementID", cn);
                cm.Parameters.AddWithValue("@announcementID", announcementid);

                SqlDataReader an = cm.ExecuteReader();
                an.Read();
                String annoucementids = an["AnnouncementID"].ToString();
                String annoucementdate = an["Date"].ToString();
                String annoucementtext = an["AnnouncementText"].ToString();
                label22.Text = " ID : " + annoucementids;
                label21.Text = " DATE : " + annoucementdate;
                label18.Text = " TEXT : " + annoucementtext;
                an.Close();
                cn.Close();
                /////////////////


                cn.Open();
                cm = new SqlCommand("SELECT u.Name AS UserName, c.CommentText, c.CommentDate, c.CommentID FROM Commenttt c INNER JOIN Userr u ON c.UserID = u.UserID WHERE c.AnnouncementID = @announcementID", cn);
                cm.Parameters.AddWithValue("@announcementID", announcementid);
                dr = cm.ExecuteReader();

                int commentCount = 0;
                int yPos = 10;  // set an initial y-position

                while (dr.Read())
                {
                    commentCount++;
                    string userName = dr["UserName"].ToString();
                    string commentText = dr["CommentText"].ToString();
                    DateTime commentDate = (DateTime)dr["CommentDate"];
                    int commentId = (int)dr["CommentID"]; // Get the comment ID

                    // Create a panel to hold the comment label and delete button
                    Panel commentPanel = new Panel();
                    commentPanel.BackColor = Color.LightGreen;
                    commentPanel.BorderStyle = BorderStyle.FixedSingle;
                    commentPanel.Location = new Point(10, yPos);
                    commentPanel.Size = new Size(500, 100); // Set the panel's size

                    // Create a label to display the comment information
                    Label commentLabel = new Label();
                    commentLabel.Text = "Name: " + userName + "\nComment: " + commentText + "\nDate: " + commentDate;

                    commentLabel.AutoSize = true;
                    commentLabel.Font = new Font("Arial", 12, FontStyle.Regular); // Set the font size and style
                    commentLabel.Padding = new Padding(10);

                    // Create a delete button and position it within the comment panel
                    Button deleteButton = new Button();
                    deleteButton.Text = "Delete";
                    deleteButton.AutoSize = true;
                    deleteButton.Location = new Point(commentPanel.Width - deleteButton.Width - 10, (commentPanel.Height - deleteButton.Height) / 2); // Position the delete button to the right within the panel
                    deleteButton.Click += (sender, e) => DeleteComment(commentId);

                    // Add the comment label and delete button to the comment panel
                    commentPanel.Controls.Add(commentLabel);
                    commentPanel.Controls.Add(deleteButton);

                    // Add the comment panel to the main panel
                    panel1.Controls.Add(commentPanel);

                    // Increase the y-position for the next comment panel
                    yPos += commentPanel.Height + 10;
                }

                Console.WriteLine("Loaded {commentCount} comments.");

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteComment(int commentId)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT UserID FROM Commenttt WHERE CommentID = @commentID", cn);
                cm.Parameters.AddWithValue("@commentID", commentId);
                int userId = (int)cm.ExecuteScalar();

                if (userId == studentid)
                {
                    // The current user is allowed to delete the comment
                    cm = new SqlCommand("DELETE FROM Commenttt WHERE CommentID = @commentID", cn);
                    cm.Parameters.AddWithValue("@commentID", commentId);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Comment deleted successfully!");

                    LoadComments(); // Refresh the comments after deletion
                }
                else
                {
                    cn.Close();
                    MessageBox.Show("You can only delete your own comments!");
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //go back 
            Studentclassdetail cld = new Studentclassdetail(studentid, classid);
            cld.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //send button 
            try
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO Commenttt (UserID, AnnouncementID, AssignmentID,ClassID, CommentText, CommentDate) VALUES (@userID, @announcementID, NULL,@class, @commentText, GETDATE())", cn);
                cm.Parameters.AddWithValue("@userID", studentid);
                cm.Parameters.AddWithValue("@announcementID", announcementid);
                cm.Parameters.AddWithValue("@commentText", textBox1.Text);
                cm.Parameters.AddWithValue("@class",classid);
                cm.ExecuteNonQuery();

                // Display a success message to the user
                MessageBox.Show("Comment added successfully!");

                // Clear the comment textbox
                textBox1.Text = "";

                // Refresh the comments
                // LoadComments();

                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }

            LoadComments();
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //above panel 
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //text feild
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Set panel properties
            panel1.BackColor = Color.WhiteSmoke; // Set the background color
            panel1.BorderStyle = BorderStyle.FixedSingle; // Add a border to the panel
            //lower panel 
        }

        private void Announcementcommentsstudent_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            //lower panel
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
