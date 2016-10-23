using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.UserScope
{
    public class UserPermission
    {

        /// <summary>
        /// Permission Id
        /// </summary>
        public int Permission_Id { get; set; }
        /// <summary>
        /// Permission Name
        /// </summary>
        public string PermissionName { get; set; }
        /// <summary>
        /// Permission Description
        /// </summary>
        public string PermissionDescription { get; set; }
    }
}