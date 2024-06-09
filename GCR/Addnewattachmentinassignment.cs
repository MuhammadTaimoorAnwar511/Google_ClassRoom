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
    public partial class Addnewattachmentinassignment : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private int assignmentid;
        private string filepaths = "";
        public Addnewattachmentinassignment()
        {
            InitializeComponent();
        }
        public Addnewattachmentinassignment(int classid, int teacherid, int assignmentid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            this.assignmentid = assignmentid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the desired options for the file dialog
                openFileDialog.Title = "Select File";
                openFileDialog.Filter = "All Files (*.*)|*.*";

                // Display the file dialog
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // The user selected a file, you can access the file path using openFileDialog.FileName
                    string selectedFilePath = openFileDialog.FileName;
                    filepaths = selectedFilePath;
                    // Get the file name from the file path
                    string fileName = Path.GetFileName(selectedFilePath);

                    // Store the file name in a string variable
                    string storedFileName = fileName;

                    label1.Text = "Attachemnet : " + fileName;
                    // Implement your file handling logic here
                    // For example, you can upload the file, process it, etc.
                }
            }
        }
        public void UploadFileToDatabase(string filePath, int assignmentid)
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);

            // Prepare SQL query with parameters
            string insertQuery = "INSERT INTO attachmentt (UserID, ClassID,AssignmentID,SubmissionFilename,SubmissionFile) VALUES (@Teacherid,@ClassID,@Materialid,@FileName, @FileData)";
            cn.Open();

            try
            {


                // Create the command and assign parameters
                using (SqlCommand command = new SqlCommand(insertQuery, cn))
                {
                    command.Parameters.AddWithValue("@FileName", Path.GetFileName(filePath));
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@Teacherid", teacherid);
                    command.Parameters.AddWithValue("@Materialid", assignmentid);

                    // Execute the command
                    command.ExecuteNonQuery();
                }
                //MessageBox.Show("File uploaded successfully!");
                //Console.WriteLine("File uploaded successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine("Error uploading file: " + ex.Message);
            }
            cn.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (filepaths != "")
            {
                // Successful login

                UploadFileToDatabase(filepaths, assignmentid);
                MessageBox.Show("Successfuly Added!");
                Assignmentdetails classdetails = new Assignmentdetails(classID, teacherid, assignmentid);
                classdetails.Show();
                this.Hide();
                //GCR cld = new GCR(teacherid);
                //cld.Show();
                //this.Hide();



            }
            else
            {
                MessageBox.Show("No file selected!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Assignmentdetails classdetails = new Assignmentdetails(classID, teacherid, assignmentid);
            classdetails.Show();
            this.Hide();
        }
    }
}
