using PecoPallet.Data.DbContext;
using PecoPallet.Data.Models;
using SampleTracking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecoPallet.Data.ServiceFactory
{
    public class PolygonFactory
    {
        public static bool SavePolygonData(List<Polygon> polygons, string conStr)
        {
            var result = true;
           

            DbContext.DBContext.dbConnectionString = conStr;
            int i = 0;
            foreach (var poly in polygons)
            {
                i++;
                //string qry = "insert into location(loc_name, zip, shape_type) values('" + poly.Name + "'," + poly.Zip + ", 'Polygon');\n";
                string qry = "insert into peco.TBL_AT_GEOFENCE(GEOFENCE_DESC,GEOFENCE_ZIP,GEOFENCE_SHAPE) values('" + poly.Name + "'," + poly.Zip + ", 'Polygon');\n";

                int j = 0;
                foreach (var p in poly.Points)
                {
                    j++;
                    qry += "insert into peco.TBL_AT_GEOFENCE_COOR(GEOFENCE_ID,GEOFENCE_COOR_ORDER,GEOFENCE_COOR_LONG,GEOFENCE_COOR_LAT) values((select max(IID) from peco.TBL_AT_GEOFENCE), "
                        + j + ", " + p.Longitude + "," + p.Latitude + ");\n";
                }

                result = result && DBContext.SaveToDb(qry);
            }

            return result;
        }


        public static List<Polygon> GetAllPolygons(string connStr)
        {
            var lstData = new List<Polygon>();
            Polygon data = null;

            DbContext.DBContext.dbConnectionString = connStr;
            string qry = "select l.loc_id, l.loc_name, g.geo_id, g.latitude, g.longitude from location l join geo_location g on l.loc_id = g.loc_id "
                        + "where l.shape_type = 'Polygon' "
                        + "order by l.loc_id, g.geo_id; ";

            var ds = DbContext.DBContext.GetDataSetFromQuery(qry,connStr);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                data = new Polygon
                {
                    Id = int.Parse(dt.Rows[0][0].ToString()),
                    Name = dt.Rows[0][1].ToString(),
                    Zip = int.Parse(dt.Rows[0][2].ToString())
                };

                foreach (DataRow dr in dt.Rows)
                {
                    data.Points.Add(new Models.PolygonPoint
                    {
                        Pid = int.Parse(dr[2].ToString()),
                        Latitude = dr[3].ToString(),
                        Longitude = dr[4].ToString()
                    });
                }
            }

            lstData.Add(data);

            return lstData;
        }

        public static List<Polygon> GetAllPolygonsByZipCode(int zipcode,string connStr)
        {
            var lstData = new List<Polygon>();
            Polygon polydata = null;

            DbContext.DBContext.dbConnectionString = connStr;
            //string qry = "select * from location where shape_type='Polygon' order by 1; \n";
            //qry += "select l.loc_id, g.geo_id, g.latitude, g.longitude from location l join geo_location g on l.loc_id = g.loc_id "
            //            + "where l.shape_type = 'Polygon' "
            //            + "order by l.loc_id, g.geo_id; ";

            string qry = "select IID , GEOFENCE_DESC, GEOFENCE_ZIP from peco.TBL_AT_GEOFENCE Where GEOFENCE_ZIP = '" + zipcode + "';";
            qry += "select C.GEOFENCE_COOR_LAT,C.GEOFENCE_COOR_LONG,C.GEOFENCE_COOR_ORDER ,C.GEOFENCE_ID "
                       + "from peco.TBL_AT_GEOFENCE_COOR C "
                        + "JOIN peco.TBL_AT_GEOFENCE G ON G.IID = C.GEOFENCE_ID "
                           + "where G.GEOFENCE_ZIP ='" + zipcode + "';";


            //string qry = "select G.IID,G.GEOFENCE_DESC,G.GEOFENCE_ZIP,C.GEOFENCE_COOR_LAT,C.GEOFENCE_COOR_LONG,C.GEOFENCE_COOR_ORDER from peco.TBL_AT_GEOFENCE G "
            //                + "JOIN peco.TBL_AT_GEOFENCE_COOR C ON G.IID = C.GEOFENCE_ID "
            //                + "where G.GEOFENCE_SHAPE = 'Polygon' "
            //                + "ORDER BY G.IID ,C.GEOFENCE_COOR_ORDER ";

            var ds = DbContext.DBContext.GetDataSetFromQuery(qry,connStr);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr1 in ds.Tables[0].Rows)
                {
                    polydata = new Polygon
                    {
                        Id = int.Parse(dr1[0].ToString()),
                        Name = dr1[1].ToString(),
                        Zip = int.Parse(dr1[2].ToString())
                    };

                    DataTable dtPoints = ds.Tables[1];
                    DataRow[] rows = dtPoints.Select("GEOFENCE_ID=" + polydata.Id);
                    PolygonPoint point = null;
                    polydata.Points = new List<PolygonPoint>();
                    foreach (DataRow dr2 in rows)
                    {
                        point = new PolygonPoint
                        {
                            Pid = int.Parse(dr2[2].ToString()),
                            Latitude = dr2[0].ToString(),
                            Longitude = dr2[1].ToString()
                        };

                        polydata.Points.Add(point);
                    }
                    //foreach (DataRow dr2 in rows)
                    //{
                    //    point = new PolygonPoint
                    //    {
                    //        Pid = int.Parse(dr2[1].ToString()),
                    //        Latitude = dr2[2].ToString(),
                    //        Longitude = dr2[3].ToString()
                    //    };
                    //    polydata.Points.Add(point);
                    //}

                    lstData.Add(polydata);
                }
            }

            return lstData;
        }
    }
}
