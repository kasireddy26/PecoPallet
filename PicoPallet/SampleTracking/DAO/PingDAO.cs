using SampleTracking.Custom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SampleTracking.DAO
{
    public class PingDAO
    {
        static AssetTrackingEntities db = new AssetTrackingEntities();
        public static List<LocationDTO> GetLocations()
        {
            List<LocationDTO> dtos = new List<LocationDTO>();
           var views=  db.pingdataviews.Take(5000);
           foreach(pingdataview v in views)
            {

                LocationDTO dto = new LocationDTO
                {
                    Date=v.Date,
                    DeviceID=v.DeviceID,
                    Lat=v.Lat,
                    Long=v.Long,
                    Status=v.Status,
                    Temp=v.Temp,
                    Type=v.Type
                };
                dtos.Add(dto);
            }
            return dtos;
        }
        public static List<LocationDTO> GetLocations(string DeviceId)
        {
            List<LocationDTO> dtos = new List<LocationDTO>();
            var views = db.pingdataviews.Where(i=>i.DeviceID==DeviceId);
            foreach (pingdataview v in views)
            {
                LocationDTO dto = new LocationDTO
                {
                    Date = v.Date,
                    DeviceID = v.DeviceID,
                    Lat = v.Lat,
                    Long = v.Long,
                    Status = v.Status,
                    Temp = v.Temp,
                    Type = v.Type
                };
                dtos.Add(dto);
            }
            return dtos;
        }
        
        
    }
}