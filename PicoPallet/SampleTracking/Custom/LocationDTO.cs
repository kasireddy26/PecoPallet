using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Custom
{
    public class LocationDTO
    {
        public string DeviceID { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public Double? Lat { get; set; }
        public Double? Long { get; set; }
        public Decimal? Temp { get; set; }
    }
}