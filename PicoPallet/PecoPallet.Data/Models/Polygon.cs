using PecoPallet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Models
{
    public class Polygon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public List<PolygonPoint> Points { get; set; }
    }
}