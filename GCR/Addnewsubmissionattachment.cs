using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace i211130
{
    public partial class Addnewsubmissionattachment : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int studentid;
        private int assignmentid;
        private string filepaths = "";
        public Addnewsubmissionattachment()
        {
            InitializeComponent();
        }
        public Addnewsubmissionattachment(int classid, int studentid, int assignmentid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.studentid = studentid;
            this.assignmentid = assignmentid;
        }
        public void UploadFileToDatabase(string filePath, int materialid)
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);
            //MessageBox.Show(classID+"");
            // Prepare SQL query with parameters
            string insertQuery = "INSERT INTO Submission (AssignmentID,ClassID, StudentID,SubmissionDate,SubmissionFilename,SubmissionFile,MarksReceived) VALUES (@assignmentid,@ClassiD,@studentID,GETDATE(),@FileName, @FileData,0)";
            cn.Open();

            try
            {


                // Create the command and assign parameters
                using (SqlCommand command = new SqlCommand(insertQuery, cn))
                {
                    command.Parameters.AddWithValue("@FileName", Path.GetFileName(filePath));
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@ClassiD", classID);
                    command.Parameters.AddWithValue("@studentID", studentid);
                    command.Parameters.AddWithValue("@assignmentid", assignmentid);

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
                Assignmentdetailsstudent submitassignment = new Assignmentdetailsstudent(classID, studentid, assignmentid);
                submitassignment.Show();
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
            Assignmentdetailsstudent submitassignment = new Assignmentdetailsstudent(classID, studentid, assignmentid);
            submitassignment.Show();
            this.Hide();
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
    }
}
