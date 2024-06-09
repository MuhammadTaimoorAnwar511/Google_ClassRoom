using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
//using System.Reflection.Emit;
using System.Windows.Forms;

namespace i211130
{
    public partial class GCR : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int teacherid;
       
        public GCR()
        {
            
        }
        public GCR(int teacherid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.teacherid = teacherid;
           
            LoadClassDetails(); // Call the method to load class details dynamically

            label4.Text = "Teacher ID: " + teacherid;

        }

        private void LoadClassDetails()
        {
            try
            {
                cn.Open();

                //////////////////////////////////////////////////
                // Retrieve the teacher name based on teacherid
                cm = new SqlCommand("SELECT Name FROM Userr WHERE UserID = @TeacherID", cn);
                cm.Parameters.AddWithValue("@TeacherID", teacherid);
                string teacherName = cm.ExecuteScalar().ToString();
                label1.Text = "Teacher Name: " + teacherName;
                //////////////////////////////////////////////////

                
                cm = new SqlCommand("SELECT ClassID, ClassName, ClassroomCode, TeacherID FROM Class WHERE TeacherID = @TeacherID", cn);
                cm.Parameters.AddWithValue("@TeacherID", teacherid);
                dr = cm.ExecuteReader();


                int x = 10; // Initial x-coordinate for positioning controls
                int y = 10; // Initial y-coordinate for positioning controls
                int panelCount = 0; // Counter for the number of panels in the current row

                while (dr.Read())
                {
                    int classID = Convert.ToInt32(dr["ClassID"]);
                    string className = dr["ClassName"].ToString();
                    string classroomCode = dr["ClassroomCode"].ToString();
                    int teacherID = Convert.ToInt32(dr["TeacherID"]);
                   

                    // Create a new panel for each class
                    Panel classPanel = new Panel();
                    classPanel.BorderStyle = BorderStyle.FixedSingle;
                    classPanel.Size = new Size(400, 120);
                    classPanel.Location = new Point(x, y);
                    classPanel.BackColor = Color.LightGreen;
                    classPanel.Padding = new Padding(5); // Optional: Add padding to the pan

                    // Create and configure label for class ID
                    Label lblClassID = new Label();
                    lblClassID.Text = "Class ID: " + classID;
                    lblClassID.Location = new Point(10, 10);
                    lblClassID.AutoSize = true;
                    lblClassID.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblClassID);

                    // Create and configure label for class name
                    Label lblClassName = new Label();
                    lblClassName.Text = "Class Name: " + className;
                    lblClassName.Location = new Point(10, 30);
                    lblClassName.AutoSize = true;
                    lblClassName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblClassName);

                    // Create and configure label for classroom code
                    Label lblClassroomCode = new Label();
                    lblClassroomCode.Text = "Classroom Code: " + classroomCode;
                    lblClassroomCode.Location = new Point(10, 50);
                    lblClassroomCode.AutoSize = true;
                    lblClassName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblClassroomCode);

                    // Create and configure label for teacher ID
                    Label lblTeacherID = new Label();
                    lblTeacherID.Text = "Teacher ID: " + teacherID;
                    lblTeacherID.Location = new Point(10, 70);
                    lblTeacherID.AutoSize = true;
                    lblClassName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    classPanel.Controls.Add(lblTeacherID);

                    // Create and configure detail button
                    Button btnDetails = new Button();
                    btnDetails.Text = "Details";
                    btnDetails.Location = new Point(10, 90);
                    btnDetails.Tag = classID; // Set the class ID as the button's tag for identifying the selected class
                    btnDetails.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btnDetails.ForeColor = Color.Black;
                    btnDetails.Click += btnDetails_Click; // Assign the click event handler
                    classPanel.Controls.Add(btnDetails);

                    Button btnDelete = new Button();
                    btnDelete.Text = "Delete";
                    btnDelete.Location = new Point(90, 90);
                    btnDelete.Tag = classID; // Set the class ID as the button's tag for identifying the selected class
                    btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btnDelete.ForeColor = Color.Black;
                    btnDelete.Click += deleteclass; // Assign the click event handler
                    classPanel.Controls.Add(btnDelete);

                    // Add the class panel to the main panel
                    panel3.Controls.Add(classPanel);

                    // Increment the panelCount
                    panelCount++;

                    // If three panels are already in the row, move to the next row
                    if (panelCount == 2)
                    {
                        x = 10; // Reset x-coordinate to the starting position
                        y += 150; // Increment y-coordinate to move to the next row
                        panelCount = 0; // Reset the panelCount
                    }
                    else
                    {
                        x += 410; // Increment x-coordinate to move to the next column
                    }
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
        private void get_classdetail(int classid,int teacherid)
        {
            Classdetails c = new Classdetails(classid,teacherid);
            c.Show();
            this.Hide();
        }
        private void btnDetails_Click(object sender, EventArgs e)
        {
            // Get the selected class ID from the button's tag
            int selectedClassID = Convert.ToInt32(((Button)sender).Tag);
            get_classdetail(selectedClassID,teacherid);
            // TODO: Implement your code to show class details for the selected class ID
            // You can access the selectedClassID and perform the necessary actions accordingly
        }

        private void Addclass_Click_1(object sender, EventArgs e)
        {
            Addclass a = new Addclass(teacherid);
            a.Show();
            this.Hide();
        }

        //detail button
        private void button1_Click(object sender, EventArgs e)
        {
            Classdetails c = new Classdetails();
            c.Show();
            this.Hide();
        }

        //logout button
        private void button8_Click(object sender, EventArgs e)
        {
            Login cld = new Login();
            cld.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            //store teacher id
        }

        private void GCR_Load(object sender, EventArgs e)
        {

        }
        private void deleteannouncecomments(int id)
        {
            cn.Open();
            try
            {

                cm = new SqlCommand("DELETE FROM Commenttt WHERE ClassID = @id ", cn);
                cm.Parameters.AddWithValue("@id", id);
                cm.ExecuteNonQuery();
                //MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Classdetails m = new Classdetails(classID, teacherid);
                //m.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        public void DeleteAnnouncement(int id)
        {
            deleteannouncecomments(id);
            cn.Open();
            try
            {

                cm = new SqlCommand("DELETE FROM Announcement WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", id);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletestudents(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Enrollment WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deleteclickmaterial(int id)
        {
            int matID = id;
            deleteattach(matID);
            deletemat(matID);
        }
        private void deleteattach(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM attachmentt WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletemat(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Material WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletematassignment(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Assignment WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deletesubmission(int matID)
        {
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Submission WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", matID);
                cm.ExecuteNonQuery();
                //MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Classdetails m = new Classdetails(classID, teacherid);
                //m.Show();
                //this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cn.Close();
        }
        private void deleteclickassignment(int id)
        {
            int matID = id;
            deletesubmission(matID);
            deletematassignment(matID);
        }
        private void deleteclass(object sender, EventArgs e)
        {
            int selectedClassID = Convert.ToInt32(((Button)sender).Tag);
            DeleteAnnouncement(selectedClassID);
            deleteclickmaterial(selectedClassID);
            deletestudents(selectedClassID);
            deleteclickassignment(selectedClassID);
            cn.Open();
            try
            {
                cm = new SqlCommand("DELETE FROM Class WHERE ClassID = @id", cn);
                cm.Parameters.AddWithValue("@id", selectedClassID);
                cm.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GCR m = new GCR( teacherid);
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
    }
}
