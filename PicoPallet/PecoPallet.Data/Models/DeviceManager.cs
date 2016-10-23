using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PecoPallet.Data.Models
{
    public class DeviceManager
    {
        public string transponder { get; set; }
        public string gateway { get; set; }
        public int batterylevel { get; set; }
        public int temperature { get; set; }
        public string lastseen { get; set; }
        public int devid { get; set; }
        public string devname { get; set; }
        public string location { get; set; }
        public string locationname { get; set; }
    }
}