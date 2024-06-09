using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace i211130
{
    public partial class Form3 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox7.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "";






            if (selectedOption == "All the movies/tv shows directed by Jason Sterman")
            {

                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title \r\nFROM Show s \r\nJOIN Show_Director sd ON s.show_id = sd.show_id \r\nJOIN Director d ON sd.director_id = d.director_id\r\nWHERE d.director_name = 'Jason Sterman';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView6.DataSource = dataTable;

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
            else if (selectedOption == "All the movies/tv shows with genre “Comedy”")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title \r\nFROM Show s \r\nJOIN Show_Listed_In sli ON s.show_id = sli.show_id \r\nJOIN Listed_In li ON sli.listed_in_id = li.listed_in_id\r\nWHERE li.listed_in_name = 'Comedy';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView6.DataSource = dataTable;

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



            else if (selectedOption == "All the movies/tv shows with rating “PG-13”")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT title \r\nFROM Show \r\nWHERE rating = 'PG-13';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView6.DataSource = dataTable;

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

            else if (selectedOption == "All the movies/tv shows in which actor “Johnny Depp” has worked")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title \r\nFROM Show s \r\nJOIN Show_Cast sc ON s.show_id = sc.show_id \r\nJOIN Cast c ON sc.cast_id = c.cast_id\r\nWHERE c.cast_name = 'Johnny Depp';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView6.DataSource = dataTable;

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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox1.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "";


            if (selectedOption== "No of Shows")
            {

                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT COUNT(*) as TotalShows FROM Show", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        ans = dr["TotalShows"].ToString();
                        //MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        label8.Text ="Answer:"+ ans;
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
            else if(selectedOption == "Average release year")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT AVG(release_year) as AvgReleaseYear FROM Show", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        ans = dr["AvgReleaseYear"].ToString();
                        //MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        label8.Text = "Answer:" + ans;
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
            else if(selectedOption== "Maximum length duration")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT MAX(duration) as MaxDuration FROM Show", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        ans = dr["MaxDuration"].ToString();
                        //MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        label8.Text = "Answer:" + ans;
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
            //MessageBox.Show("Selected option: " + selectedOption);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string selectedOption = comboBox2.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "";

            


            if (selectedOption == "No of directors for each show")
            {

                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title, COUNT(*) as TotalDirectors FROM Show s JOIN Show_Director sd ON s.show_id = sd.show_id GROUP BY s.title;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("TotalDirectors");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["TotalDirectors"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["TotalDirectors"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView1.DataSource = dataTable;

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
            else if (selectedOption == "No of actors for each show")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title, COUNT(*) as TotalActors FROM Show s JOIN Show_Cast sc ON s.show_id = sc.show_id GROUP BY s.title;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("TotalActors");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["TotalActors"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["TotalActors"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView1.DataSource = dataTable;

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
            else if (selectedOption == "No of countries for each show")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title, COUNT(*) as TotalCountries FROM Show s JOIN Show_Country sc ON s.show_id = sc.show_id GROUP BY s.title;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("TotalCountries");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["TotalCountries"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["TotalCountries"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView1.DataSource = dataTable;

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
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {


            string selectedOption = comboBox3.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "";




            if (selectedOption == "Time of inner join.")
            {

                try
                {
                    cn.Open();
                    cm = new SqlCommand("SET STATISTICS TIME ON;\r\nSELECT s.title, d.director_name\r\nFROM Show s \r\nJOIN Show_Director sd ON s.show_id = sd.show_id \r\nJOIN Director d ON sd.director_id = d.director_id;\r\nSET STATISTICS TIME OFF;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("director_name");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["director_name"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["director_name"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView2.DataSource = dataTable;

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
            else if (selectedOption == "Time of left join.")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SET STATISTICS TIME ON;\r\nSELECT s.title, d.director_name\r\nFROM Show s \r\nLEFT JOIN Show_Director sd ON s.show_id = sd.show_id \r\nLEFT JOIN Director d ON sd.director_id = d.director_id;\r\nSET STATISTICS TIME OFF;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("director_name");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["director_name"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["director_name"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView2.DataSource = dataTable;

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
            else if (selectedOption == "Time of right join.")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SET STATISTICS TIME ON;\r\nSELECT s.title, d.director_name\r\nFROM Show s \r\nRIGHT JOIN Show_Director sd ON s.show_id = sd.show_id \r\nRIGHT  JOIN Director d ON sd.director_id = d.director_id;\r\nSET STATISTICS TIME OFF;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("director_name");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["director_name"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["director_name"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView2.DataSource = dataTable;

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
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox6.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "";
            





            if (selectedOption == "Shows with more than the avg no of directors")
            {

                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title \r\nFROM Show s \r\nWHERE (SELECT COUNT(*) FROM Show_Director sd WHERE s.show_id = sd.show_id) > (SELECT AVG(DirectorCount) FROM (SELECT COUNT(*) as DirectorCount FROM Show_Director GROUP BY show_id) as sub);", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView3.DataSource = dataTable;

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
            else if (selectedOption == "Shows with more than the avg no of actors")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title \r\nFROM Show s \r\nWHERE (SELECT COUNT(*) FROM Show_Cast sc WHERE s.show_id = sc.show_id) > (SELECT AVG(ActorCount) FROM (SELECT COUNT(*) as ActorCount FROM Show_Cast GROUP BY show_id) as sub);\r\n", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView3.DataSource = dataTable;

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



            else if (selectedOption == "Shows with more than the avg no of countries")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title \r\nFROM Show s \r\nWHERE (SELECT COUNT(*) FROM Show_Country sc WHERE s.show_id = sc.show_id) > (SELECT AVG(CountryCount) FROM (SELECT COUNT(*) as CountryCount FROM Show_Country GROUP BY show_id) as sub);\r\n", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView3.DataSource = dataTable;

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
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            


            string selectedOption = comboBox4.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "";




            if (selectedOption == "total no of directors for each show, including shows with no directors")
            {

                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title, COUNT(d.director_id) as TotalDirectors FROM Show s LEFT JOIN Show_Director sd ON s.show_id = sd.show_id LEFT JOIN Director d ON sd.director_id = d.director_id GROUP BY s.title;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("TotalDirectors");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["TotalDirectors"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["TotalDirectors"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView4.DataSource = dataTable;

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
            else if (selectedOption == "total no of actors for each show, including shows with no actors")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title, COUNT(c.cast_id) as TotalActors FROM Show s LEFT JOIN Show_Cast sc ON s.show_id = sc.show_id LEFT JOIN Cast c ON sc.cast_id = c.cast_id GROUP BY s.title;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("TotalActors");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                       do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["TotalActors"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["TotalActors"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read()) ;

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView4.DataSource = dataTable;

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
            else if (selectedOption == "total no of countries for each show, including shows with no countries")
            {
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title, COUNT(co.country_id) as TotalCountries FROM Show s LEFT JOIN Show_Country sc ON s.show_id = sc.show_id LEFT JOIN Country co ON sc.country_id = co.country_id GROUP BY s.title;", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");
                        dataTable.Columns.Add("TotalCountries");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();
                            string director = dr["TotalCountries"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;
                            row["TotalCountries"] = director;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView4.DataSource = dataTable;

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
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox5.SelectedItem.ToString();
            // Do something with the selected option
            string ans = "1";


            if (selectedOption == "Shows  either directed by Jason Sterman or have a genre Comedy")
            {
                //MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title FROM Show s  JOIN Show_Director sd ON s.show_id = sd.show_id  JOIN Director d ON sd.director_id = d.director_id  WHERE d.director_name = 'Jason Sterman' UNION SELECT s.title FROM Show s  JOIN Show_Listed_In sli ON s.show_id = sli.show_id  JOIN Listed_In li ON sli.listed_in_id = li.listed_in_id  WHERE li.listed_in_name = 'Comedy';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                       
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable

                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                            // Set the DataTable as the DataSource of the DataGridView
                            dataGridView5.DataSource = dataTable;

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
            else if (selectedOption == "Shows directed by Jason Sterman but do not have a genre Comedy")
            {
                //MessageBox.Show("2", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title\r\nFROM Show s \r\nJOIN Show_Director sd ON s.show_id = sd.show_id \r\nJOIN Director d ON sd.director_id = d.director_id \r\nWHERE d.director_name = 'Jason Sterman'\r\nEXCEPT\r\nSELECT s.title\r\nFROM Show s \r\nJOIN Show_Listed_In sli ON s.show_id = sli.show_id \r\nJOIN Listed_In li ON sli.listed_in_id = li.listed_in_id \r\nWHERE li.listed_in_name = 'Comedy';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["TotalShows"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;

                        //dataGridView1.DataSource = dr;
                        // Create a DataTable to hold the data
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView5.DataSource = dataTable;

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



            else if (selectedOption == "Shows  rated PG-13 or have Johnny Depp as a cast member")
            {
                //MessageBox.Show("3", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                try
                {
                    cn.Open();
                    cm = new SqlCommand("SELECT s.title\r\nFROM Show s \r\nWHERE s.rating = 'PG-13'\r\nUNION\r\nSELECT s.title\r\nFROM Show s \r\nJOIN Show_Cast sc ON s.show_id = sc.show_id \r\nJOIN Cast c ON sc.cast_id = c.cast_id \r\nWHERE c.cast_name = 'Johnny Depp';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //ans = dr["MaxDuration"].ToString();
                        ////MessageBox.Show(ans, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //label8.Text = "Answer:" + ans;
                        DataTable dataTable = new DataTable();

                        // Add columns to the DataTable
                        dataTable.Columns.Add("title");

                        // Loop through the data in the SqlDataReader (variable dr) and populate the DataTable
                        do
                        {
                            // Retrieve the title and director values from the SqlDataReader
                            string title = dr["title"].ToString();

                            // Create a new row in the DataTable and populate it with the values
                            DataRow row = dataTable.NewRow();
                            row["title"] = title;

                            // Add the row to the DataTable
                            dataTable.Rows.Add(row);
                        } while (dr.Read());

                        // Set the DataTable as the DataSource of the DataGridView
                        dataGridView5.DataSource = dataTable;

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
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
