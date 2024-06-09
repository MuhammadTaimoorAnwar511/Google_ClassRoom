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
    public partial class Materialdetails : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private string filepaths = "";
        private string downloadpaths = "";
        private int materialid;
        public Materialdetails()
        {
            InitializeComponent();
        }
        public Materialdetails(int classid, int teacherid,int materialid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
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
                    label11.Text = MaterialTitle;
                    label12.Text = MaterialDescription;
                    label9.Text = Materialdate;

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
                            Button btn3 = new Button();
                            btn1.Text = "Download";
                            btn3.Text = "Delete";
                            btn1.UseVisualStyleBackColor = true;
                            btn3.UseVisualStyleBackColor = true;
                            //btn1.BackColor = Color.Controle;
                            btn3.Location = new Point(400, 10);
                            btn1.Location = new Point(500, 10);
                            btn1.Click += new System.EventHandler(this.savefile);
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
                            announcementPanel.Controls.Add(btn1);
                            announcementPanel.Controls.Add(btn3);
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
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void deleteattachment(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            dynamic buttonTag = deleteButton.Tag;
            int matID = buttonTag.id;
            int classid = buttonTag.classid;
            int teacherid = buttonTag.teacherid;
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM attachmentt WHERE AttachmentID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Materialdetails m = new Materialdetails(classID, teacherid,materialid);
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
        private void button6_Click(object sender, EventArgs e)
        {
            Editmaterial edm = new Editmaterial(classID,teacherid,materialid);
            edm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Classdetails cld = new Classdetails(classID,teacherid);
            cld.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Addnewattachment addnewattachment = new Addnewattachment(classID, teacherid,materialid);
            addnewattachment.Show();
            this.Hide();
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
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
