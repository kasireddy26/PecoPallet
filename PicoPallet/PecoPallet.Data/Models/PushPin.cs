using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Models
{
    public class PushPin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Zip { get; set; }
    }
}