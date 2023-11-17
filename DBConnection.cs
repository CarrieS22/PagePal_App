using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public class DbConnection
    {
        public DbConnection()
        {
        class DbConnection
        {
            // Replace these values with your actual SQL Server connection details
            string connectionString;

            SqlConnection conn;

            public DbConnection()
            {
                connectionString = "Server= DESKTOP-MKGI3I1\\SQLEXPRESS; + Trusted_Connection=true;"
                }

            public DbConnection(string conn)
            {
                connectionString = conn;
            }

            public SqlConnection GetConn()
            {
                return conn;
            }

            public string getBook_Name(SqlConnection conn)
            {
                Int32 count = 0;

                connectionString = "Server = DESKTOP-MKGI3I1\\SQLEXPRESS;" + "Trusted_Connection=true;" + "Database=PagePal;";

                conn = new SqlConnection(connectionString);
                conn.Open();
                string randomQuery = "Select (*) from dbo.Books;";
                SqlCommand cmd = new SqlCommand(conn, randomQuery);
            }
        }
    }
}