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
    public partial class Editmaterial : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private int classID;
        private int teacherid;
        private int materialid;
        public Editmaterial()
        {
            InitializeComponent();
        }
        public Editmaterial(int classid, int teacherid, int materialid)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            this.classID = classid;
            this.teacherid = teacherid;
            this.materialid = materialid;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Materialdetails cl = new Materialdetails(classID,teacherid,materialid);
            cl.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            string title = textBox1.Text.ToString();
            string description = richTextBox1.Text.ToString();
            string insertQuery = "UPDATE Material\r\nSET MaterialTitle = @Title, MaterialDescription = @Description \r\nWHERE MaterialID =@Materialid;";

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
                    command.Parameters.AddWithValue("@Materialid", materialid);

                    // Execute the command
                    command.ExecuteNonQuery();
                }

                //Console.WriteLine("File uploaded successfully!");
                MessageBox.Show("Updated material");
                Materialdetails materialdetails = new Materialdetails(classID, teacherid, materialid);
                materialdetails.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                Console.WriteLine("Error uploading file: " + ex.Message);
            }
            cn.Close();
        }
    }
}
