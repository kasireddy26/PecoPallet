using PecoPallet.Data.DbContext;
using SampleTracking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecoPallet.Data.ServiceFactory
{
    public class PushPinsFactory
    {
        public static bool SavePushPinsData(List<PushPin> pushpins, string conStr)
        {
            var result = true;
            DbContext.DBContext.dbConnectionString = conStr;
            int i = 0;
            foreach (var pin in pushpins)
            {
                i++;
                string qry = "insert into location(loc_name, zip, shape_type) values('name 1'," + pin.Zip + ", 'Pushpin'); " +
                        "insert into geo_location values((select max(loc_id) from location), " + i  + ", " + pin.Latitude + "," + pin.Longitude + ")";
                result = result && DBContext.SaveToDb(qry);
            }

            return result;

        }


        public static List<PushPin> GetPushPinsDatabyZip(int zipcode, string connStr)
        {
            var dmData = new List<PushPin>();

            DbContext.DBContext.dbConnectionString = connStr;
            var ds = DbContext.DBContext.GetDataSetFromQuery("select l.zip, g.geo_id, g.latitude, g.longitude from location l join geo_location g on l.loc_id = g.loc_id where l.zip=" + zipcode + " order by g.geo_id",connStr);

            if (ds.Tables.Count > 0)
            {
                PushPin data = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data = new PushPin
                    {
                        Zip = int.Parse(dr[0].ToString()),
                        Id = int.Parse(dr[1].ToString()),
                        Latitude = dr[2].ToString(),
                        Longitude = dr[3].ToString()
                    };

                    dmData.Add(data);
                }
            }

            return dmData;
        }

        public static List<PushPin> GetAllPushPinsData(string connStr)
        {
            var dmData = new List<PushPin>();

            DbContext.DBContext.dbConnectionString = connStr;
            var ds = DbContext.DBContext.GetDataSetFromQuery("select IID ,latitude, longitude from v_GPS_Ping order by IID", connStr);            

            if (ds.Tables.Count > 0)
            {
                PushPin data = null;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data = new PushPin
                    {                        
                        Id = int.Parse(dr[0].ToString()),
                        Latitude = dr[1].ToString(),
                        Longitude = dr[2].ToString(),
                        Zip = 0
                    };

                    dmData.Add(data);
                }
            }

            return dmData;
        }
    }
}
