using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Custom
{
    public class UserRPDTO
    {
        public int Id { get; set; }
        public List<int> AddRole { get; set; }
        public List<int> RemRole { get; set; }
        
    }
}