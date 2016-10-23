using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PecoPallet.Data.DbContext
{
    public class DBContext
    {
        public static string dbConnectionString = null;
        

        public static bool SaveToDb(string qry)
        {
            bool ret = false;
            SqlConnection con = null;
            try
            {
                //string conStr = "Data Source=PTGHYDLPP;Initial Catalog = PecoDb;User ID = sa; Password = mssql!@12;";
                con = new SqlConnection(dbConnectionString);
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return ret;
        }

        public static DataSet GetDataSetFromQuery(string qryString,string connString)
        {
            //bool flg = false;
            DataSet retDs = new DataSet();
            SqlConnection conn = null;
            try
            {
//                dbConnectionString = @"Data Source=SATPAL-PC\SQLEXPRESS;Initial Catalog = ioT_AT_deV;;Integrated Security=True";
                using (conn = new SqlConnection(connString))
                {
                    SqlDataAdapter dad = new SqlDataAdapter(qryString, conn);
                    dad.Fill(retDs);
                    //flg = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            return retDs;
        }
    }
}
