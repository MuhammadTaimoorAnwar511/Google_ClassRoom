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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace i211130
{
    public partial class Addassignment : Form
    {
        SqlConnection An = new SqlConnection();
        SqlCommand Am = new SqlCommand();
        DBConnection Abcon = new DBConnection();
        SqlDataReader dr;
        int classID;
        int teacherID;
        string filepaths = "";
        public Addassignment()
        {
            InitializeComponent();
        }
        public Addassignment(int classID, int teacherid)
        {
            InitializeComponent();
            An = new SqlConnection(Abcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classID;
            this.teacherID = teacherid;
        }


       

        private void button3_Click(object sender, EventArgs e)
        {
            Classdetails cld = new Classdetails(classID,teacherID);
            cld.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            try
            {

                An.Open();



                Am = new SqlCommand("insert into Assignment(ClassID,Title,Description,Deadline,Timee,TotalMarks) values (@classID,@Title, @Description,@Deadline,@Timee,@TotalMarks)", An);
                Am.Parameters.AddWithValue("@classID", classID);
                Am.Parameters.AddWithValue("@Title", textBox1.Text);
                Am.Parameters.AddWithValue("@Deadline", textBox2.Text);
                Am.Parameters.AddWithValue("@Timee", textBox3.Text);
                Am.Parameters.AddWithValue("@TotalMarks", int.Parse(textBox4.Text));
                Am.Parameters.AddWithValue("@Description", richTextBox1.Text);
                Am.ExecuteNonQuery();
                //MessageBox.Show("Successfully Updated", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Classdetails classdetails = new Classdetails(classID, teacherID);
                //classdetails.Show();
                this.Hide();
                An.Close();
            }
            catch (Exception ex)
            {
                An.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (filepaths != "")
            {
                // Assuming role for GCR
                try
                {
                    An.Open();
                    Am = new SqlCommand("SELECT Top 1 AssignmentID FROM Assignment ORDER BY AssignmentID DESC;", An);
                    // int teacherid = (int)dr["UserID"];
                    //cm.Parameters.AddWithValue("@Email", email);
                    //cm.Parameters.AddWithValue("@Password", password);
                    //cm.Parameters.AddWithValue("@Role", role);

                    dr = Am.ExecuteReader();

                    if (dr.Read())
                    {
                        // Successful login
                        int materialid = (int)dr["AssignmentID"];
                        dr.Close();
                        UploadFileToDatabase(filepaths, materialid);
                        MessageBox.Show("Successfuly Added!");
                        Classdetails classdetails = new Classdetails(classID, teacherID);
                        classdetails.Show();
                        this.Hide();
                        //GCR cld = new GCR(teacherid);
                        //cld.Show();
                        //this.Hide();
                    }
                    else
                    {
                        // Invalid credentials
                        MessageBox.Show("Invalid login credentials. Please try again.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //dr.Close();
                An.Close();


            }
            else
            {
                MessageBox.Show("Successfuly Added!");
                Classdetails classdetails = new Classdetails(classID, teacherID);
                classdetails.Show();
                this.Hide();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void UploadFileToDatabase(string filePath, int materialid)
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);

            // Prepare SQL query with parameters
            string insertQuery = "INSERT INTO attachmentt (UserID, ClassID,AssignmentID,SubmissionFilename,SubmissionFile) VALUES (@Teacherid,@ClassID,@Materialid,@FileName, @FileData)";


            try
            {
                //An.Open();

                // Create the command and assign parameters
                using (SqlCommand command = new SqlCommand(insertQuery, An))
                {
                    command.Parameters.AddWithValue("@FileName", Path.GetFileName(filePath));
                    command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@Teacherid", teacherID);
                    command.Parameters.AddWithValue("@Materialid", materialid);

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

                    label3.Text = "Attachemnet : " + fileName;
                    // Implement your file handling logic here
                    // For example, you can upload the file, process it, etc.
                }
            }
        }

        private void Addassignment_Load(object sender, EventArgs e)
        {
        
        }
    }
}
