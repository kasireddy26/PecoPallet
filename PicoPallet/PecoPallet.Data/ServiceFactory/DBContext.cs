using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using PecoPallet.Data.Models;

namespace PecoPallet.Data.ServiceFactory
{
    public class DBContext
    {
        static SqlConnection con;

        public static DataSet GetDataSetFromQuery(string qryString)
        {
            DataSet retDs = null;


            return retDs;
        }

        public static List<DeviceManager> GetDeviceManagerData(string connStr)
        {
            var dmData = new List<DeviceManager>();
            DataSet ds = new DataSet();
            bool flg = false;
            string stmt = "select * from device_manager";
            try
            {
                con = new SqlConnection(connStr);
                SqlDataAdapter dad = new SqlDataAdapter(stmt, con);
                dad.Fill(ds);
                flg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            if (flg)
            {
                DeviceManager data = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data = new DeviceManager
                    {
                        transponder = dr[0].ToString(),
                        gateway = dr[1].ToString(),
                        batterylevel = int.Parse( dr[2].ToString()),
                        temperature = int.Parse(dr[3].ToString()),
                        lastseen = dr[4].ToString(),
                        devid = int.Parse(dr[5].ToString()),
                        devname = dr[6].ToString(),
                        location = dr[7].ToString(),
                        locationname = dr[8].ToString()
                    };

                    dmData.Add(data);
                }
            }

            return dmData;
        }
    

       
    }
}