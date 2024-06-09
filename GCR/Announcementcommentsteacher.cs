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
    public partial class Announcementcommentsteacher : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private int annoucementid;
        public Announcementcommentsteacher(int classid, int teacherid, int annoucementid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            this.annoucementid = annoucementid;

            LoadComments();
        }
        ///  
        private void LoadComments()
        {
            try
            {
                ///////////////////
                cn.Open(); // Open the database connection
                cm = new SqlCommand("SELECT * FROM Announcement WHERE AnnouncementID = @AnnouncementID", cn);
                cm.Parameters.AddWithValue("@AnnouncementID", annoucementid);
                SqlDataReader an = cm.ExecuteReader();
                an.Read();
                String annoucementids = an["AnnouncementID"].ToString();
                String annoucementdate = an["Date"].ToString();
                String annoucementtext = an["AnnouncementText"].ToString();
                label1.Text = " ID : " + annoucementids;
                label3.Text = " DATE : " + annoucementdate;
                label4.Text = " TEXT : " + annoucementtext;
                an.Close();
                cn.Close();
                /////////////////

                cn.Open(); // Open the database connection

                // Create the SQL query to retrieve comments for the announcement
                string query = "SELECT CommentID, CommentDate, CommentText " +
                               "FROM Commenttt " +
                               "WHERE AnnouncementID = @AnnouncementID";

                // Create the SqlCommand object and set the connection and query
                cm = new SqlCommand(query, cn);

                // Add the parameter value to the SqlCommand
                cm.Parameters.AddWithValue("@AnnouncementID", annoucementid);

                // Execute the query and get the SqlDataReader
                SqlDataReader dr = cm.ExecuteReader();

                // Clear the existing comments in the panel
                panel1.Controls.Clear();

                // Set the initial position for displaying comments in the panel
                int commentY = 10;

                // Read the comments from the SqlDataReader and add them to the panel
                while (dr.Read())
                {
                    // Get the comment details from the SqlDataReader
                    int commentId = dr.GetInt32(0);
                    DateTime commentDate = dr.GetDateTime(1);
                    string commentText = dr.GetString(2);

                    // Create a Panel control to hold the comment details
                    Panel commentPanel = new Panel();
                    commentPanel.BorderStyle = BorderStyle.FixedSingle;
                    commentPanel.BackColor = Color.LightGreen;
                    commentPanel.AutoSize = true;
                    commentPanel.Location = new Point(10, commentY);

                    // Create a Label control to display the comment details
                    Label lblComment = new Label();
                    lblComment.Text = "Comment ID: " + commentId + "\nComment Date: " + commentDate + "\nComment: " + commentText;

                    lblComment.AutoSize = true;
                    lblComment.Location = new Point(10, 10);

                    // Add the Label control to the Panel
                    commentPanel.Controls.Add(lblComment);

                    // Create a Delete button for the comment
                    Button deleteButton = new Button();
                    deleteButton.Text = "Delete";
                    deleteButton.Location = new Point(lblComment.Right + 10, 10);
                    // Add an event handler for the Delete button
                    deleteButton.Click += (sender, e) =>
                    {

                        // Handle the delete button click event
                        if (MessageBox.Show("Are you sure you want to delete this comment?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            try
                            {
                                cn.Open(); // Open the database connection

                                // Create the SQL query to delete the comment from the database
                                string deleteQuery = "DELETE FROM Commenttt WHERE CommentID = @CommentID";

                                // Create the SqlCommand object and set the connection and query
                                SqlCommand deleteCmd = new SqlCommand(deleteQuery, cn);

                                // Add the parameter value to the SqlCommand
                                deleteCmd.Parameters.AddWithValue("@CommentID", commentId);

                                // Execute the delete query
                                deleteCmd.ExecuteNonQuery();

                                // Show a success message
                                MessageBox.Show("Comment deleted successfully!");


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                            finally
                            {
                                cn.Close(); // Close the database connection
                            }
                            // Refresh the comments
                            LoadComments();
                        }
                      
                        
                    };
                    // Add the Delete button to the Panel
                    commentPanel.Controls.Add(deleteButton);

                    // Add the Panel control to the main panel
                    panel1.Controls.Add(commentPanel);

                    // Increment the Y position for the next comment
                    commentY += commentPanel.Height + 20;
                }

                // Close the SqlDataReader
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cn.Close(); // Close the database connection
            }
        }


        ////
        private void button2_Click(object sender, EventArgs e)
        {
            //back button
            Classdetails cld = new Classdetails(classID,teacherid);
            cld.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //upper panel
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///send button 
           
            // Get the comment text from the TextBox
            string commentText = textBox1.Text.Trim();

            // Check if the comment text is not empty
            if (!string.IsNullOrEmpty(commentText))
            {
                try
                {
                    cn.Open(); // Open the database connection

                    // Create the SQL query to insert the comment into the database
                    string query = "INSERT INTO Commenttt (UserID, AnnouncementID,ClassID, AssignmentID, CommentText, CommentDate) " +
                                   "VALUES (@UserID, @AnnouncementID,@ClassID ,@AssignmentID, @CommentText, GETDATE())";

                    // Create the SqlCommand object and set the connection and query
                    cm = new SqlCommand(query, cn);

                    // Add the parameter values to the SqlCommand
                    cm.Parameters.AddWithValue("@UserID", teacherid);
                    cm.Parameters.AddWithValue("@ClassID", classID);
                    cm.Parameters.AddWithValue("@AnnouncementID", annoucementid);
                    cm.Parameters.AddWithValue("@AssignmentID", DBNull.Value); // Set AssignmentID to NULL
                    cm.Parameters.AddWithValue("@CommentText", commentText);

                    // Execute the query
                    cm.ExecuteNonQuery();

                    MessageBox.Show("Comment added successfully!");
                    // Clear the TextBox
                    textBox1.Clear();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    cn.Close(); // Close the database connection
                   
                }
               
               LoadComments();
            }
            else
            {
                MessageBox.Show("Please enter a comment text.");
            }
            ////refresh page
         
            ///
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //text feild//
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel below//

            // Set panel properties
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;

            // Iterate through each control within the panel
            foreach (Control control in panel1.Controls)
            {
                // Check if the control is a Label
                if (control is Label)
                {
                    // Set font size and style for the Label control
                    Label label = (Label)control;
                    label.Font = new Font(label.Font.FontFamily, 14, FontStyle.Regular);
                }
            }
        }

    }
}
