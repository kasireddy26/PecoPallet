using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.UserScope
{
    public class UserRole
    {

        /// <summary>
        /// Role Id 
        /// </summary>
        public int Role_Id { get; set; }
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Role Description
        /// </summary>
        public string RoleDescripton { get; set; }
        /// <summary>
        /// Role Permissions
        /// </summary>
        public List<UserPermission> Permissions = new List<UserPermission>();
    }
}