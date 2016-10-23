using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.Custom
{
    public class RolesDTO
    {
        /// <summary>
        /// Permission Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }
        public List<PermissionDTO> permissions { get; set; }
        public List<int> AddPermission { get; set; }
        public List<int > RemPermission { get; set; }
    }
}

