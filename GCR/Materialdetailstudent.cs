using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i211130
{
    public partial class Materialdetailstudent : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int studentid; // Variable to store the student ID
        int classID;
        int materialid;
        string downloadpaths = "";
        public Materialdetailstudent()
        {

        }
            public Materialdetailstudent(int classID,int studentID,int materialid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.studentid = studentID;
            this.classID = classID;
            this.materialid = materialid;
            loaddataofmaterial();
        }
        private void loaddataofmaterial()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select * from Material WHERE MaterialID = @Materialid", cn);
                cm.Parameters.AddWithValue("@Materialid", materialid);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    int yPosition = 10;
                    string MaterialTitle = dr["MaterialTitle"].ToString();
                    string MaterialDescription = dr["MaterialDescription"].ToString();
                    string Materialdate = dr["Date"].ToString();

                    label9.Text = MaterialTitle;
                    label11.Text = Materialdate;
                    label12.Text = MaterialDescription;
                    
                    dr.Close();
                    cn.Close();
                    cn.Open();
                    cm = new SqlCommand("Select * from attachmentt WHERE MaterialID = @Materialid AND ClassID=@classid", cn);
                    cm.Parameters.AddWithValue("@Materialid", materialid);
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
                            btn1.Text = "Download";
                            btn1.UseVisualStyleBackColor = true;
                            //btn1.BackColor = Color.Controle;
                            btn1.Location = new Point(500, 10);
                            btn1.Click += new System.EventHandler(this.savefile);
                            int announcementID = int.Parse(AttachmentID); // Replace with the actual ID value
                                                                          // button 
                            btn1.Tag = new { id = announcementID, classid = this.classID, teacherid = this.studentid };
                            // rest of your code...
                            Label announcementLabel = new Label();
                            announcementLabel.Text = "Name of File : " + SubmissionFilename;
                            announcementLabel.AutoSize = true;
                            announcementLabel.Location = new Point(10, 10); // Adjust the position as needed
                            announcementPanel.Controls.Add(announcementLabel);
                            announcementPanel.Controls.Add(btn1);
                            panel2.Controls.Add(announcementPanel);

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
            int teacherid = buttonTag.teacherid;
            getdownloadpath();
            RetrieveFileDataFromDatabase(atID, downloadpaths);
        }
        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //back 
            Studentclassdetail cld = new Studentclassdetail(studentid,classID);
            cld.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            //
        }
    }
}
