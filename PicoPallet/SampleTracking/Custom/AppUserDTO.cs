using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Custom
{
    public class AppUserDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailID { get; set; }
        public List<RolesDTO> roles { get; set; }
     //   public List<PermissionDTO> permissions { get; set; }
    }
}