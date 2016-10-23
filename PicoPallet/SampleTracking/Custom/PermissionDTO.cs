using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Custom
{
    public class PermissionDTO
    {
        /// <summary>
        /// Permission Id
        /// </summary>
        public int Id { get; set; }
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