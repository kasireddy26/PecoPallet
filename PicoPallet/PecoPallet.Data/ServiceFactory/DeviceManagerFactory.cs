using PecoPallet.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecoPallet.Data.ServiceFactory
{
    public class DeviceManagerFactory
    {

        public static List<DeviceManager> GetDeviceManagerData(string connStr)
        {
            var dmData = new List<DeviceManager>();

            DbContext.DBContext.dbConnectionString = connStr;
            var ds = DbContext.DBContext.GetDataSetFromQuery("select * from device_manager",connStr);

            if (ds.Tables.Count > 0)
            {
                DeviceManager data = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data = new DeviceManager
                    {
                        transponder = dr[0].ToString(),
                        gateway = dr[1].ToString(),
                        batterylevel = int.Parse(dr[2].ToString()),
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
