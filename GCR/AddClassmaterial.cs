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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace i211130
{
    public partial class AddClassmaterial : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private string filepaths="";
        public AddClassmaterial(int classid, int teacherid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
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

                    label3.Text ="Attachemnet : "+ fileName;
                    // Implement your file handling logic here
                    // For example, you can upload the file, process it, etc.
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Classdetails cl = new Classdetails(classID,teacherid);
            cl.Show();
            this.Hide();
        }
        public void UploadFileToDatabase(string filePath,int materialid)
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);

            // Prepare SQL query with parameters
            string insertQuery = "INSERT INTO attachmentt (UserID, ClassID,MaterialID,SubmissionFilename,SubmissionFile) VALUES (@Teacherid,@ClassID,@Materialid,@FileName, @FileData)";

            
                try
                {
                    //cn.Open();

                    // Create the command and assign parameters
                    using (SqlCommand command = new SqlCommand(insertQuery, cn))
                    {
                        command.Parameters.AddWithValue("@FileName", Path.GetFileName(filePath));
                        command.Parameters.AddWithValue("@FileData", fileData);
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@Teacherid", teacherid);
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
        private void button2_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text.ToString();
            string description = richTextBox1.Text.ToString();

            string insertQuery = "INSERT INTO Material (ClassID, TeacherID,MaterialTitle,MaterialDescription,Date) VALUES (@ClassID,@Teacherid,@Title, @Description, @Date)";

            try
            {
                cn.Open();

                // Create the command and assign parameters
                using (SqlCommand command = new SqlCommand(insertQuery, cn))
                {
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@Teacherid", teacherid);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);

                    // Execute the command
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("File uploaded successfully!");
                //MessageBox.Show("Uploaded material");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                Console.WriteLine("Error uploading file: " + ex.Message);
            }
            if (filepaths != "")
            {
                // Assuming role for GCR
                try
                {
                    //cn.Open();
                    cm = new SqlCommand("SELECT Top 1 MaterialID FROM Material ORDER BY MaterialID DESC;", cn);
                    // int teacherid = (int)dr["UserID"];
                    //cm.Parameters.AddWithValue("@Email", email);
                    //cm.Parameters.AddWithValue("@Password", password);
                    //cm.Parameters.AddWithValue("@Role", role);

                    dr = cm.ExecuteReader();

                    if (dr.Read())
                    {
                        // Successful login
                        int materialid = (int)dr["MaterialID"];
                        dr.Close();
                        UploadFileToDatabase(filepaths, materialid);
                        MessageBox.Show("Successfuly Added!");
                        Classdetails classdetails = new Classdetails(classID, teacherid);
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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //dr.Close();
                cn.Close();


            }
            else
            {
                MessageBox.Show("Successfuly Added!");
                Classdetails classdetails = new Classdetails(classID, teacherid);
                classdetails.Show();
                this.Hide();
            }
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddClassmaterial_Load(object sender, EventArgs e)
        {
        
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
