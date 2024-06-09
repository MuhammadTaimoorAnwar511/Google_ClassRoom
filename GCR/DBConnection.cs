using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace i211130
{
    internal class DBConnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        private string con;

        public string MyConnection()
        {
            con = @"Data Source=DESKTOP-DB0K0U1\SQLEXPRESS;Initial Catalog=dbfinalproject5;Integrated Security=True";
            return con;
        }
    }
}
