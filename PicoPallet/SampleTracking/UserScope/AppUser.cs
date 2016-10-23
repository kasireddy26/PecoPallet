using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleTracking.UserScope
{
    public class AppUser
    {
        public int User_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }

        private List<UserRole> Roles = new List<UserRole>();

        private List<UserPermission> Permissions = new List<UserPermission>();

        public AppUser(string _username)
        {
       //     this.User_Id = Convert.ToInt16(_username);
            this.Username = _username;
            this.IsSysAdmin = false;
            setName();
            GetDatabaseUserRolesPermissions();
        }

        private void GetDatabaseUserRolesPermissions()
        {
            //Get user roles and permissions from database tables...      
            using (AssetTrackingEntities _data = new AssetTrackingEntities())
            {
                user _user = _data.users.Where(u => u.Username == this.Username).FirstOrDefault();
                if (_user != null)
                {
                    this.User_Id = _user.User_Id;
                    var firstname = (_user.FirstName == null) ? string.Empty : _user.FirstName;
                    var lastname = (_user.LastName == null) ? string.Empty : _user.LastName;
                    this.Username = firstname + " " + lastname;
                    foreach (role _role in _user.roles)
                    {
                        UserRole _userRole = new UserRole
                        {
                            Role_Id = _role.Role_Id,
                            RoleName = _role.RoleName,
                            RoleDescripton = _role.RoleDescription
                        };
                        foreach (permission _permission in _role.permissions)
                        {
                            UserPermission lPermission = new UserPermission
                            {
                                Permission_Id = _permission.Permission_Id,
                                PermissionName = _permission.PermissionName,
                                PermissionDescription = _permission.PermissionDescription
                            };
                            _userRole.Permissions.Add(lPermission);
                            this.Permissions.Add(lPermission);
                        }
                        this.Roles.Add(_userRole);

                        if (!this.IsSysAdmin)
                            this.IsSysAdmin = _role.IsSysAdmin;
                    }
                    foreach (permission _permission in _user.permissions)
                    {
                        UserPermission lPermission = new UserPermission
                        {
                            Permission_Id = _permission.Permission_Id,
                            PermissionName = _permission.PermissionName,
                            PermissionDescription = _permission.PermissionDescription
                        };
                        this.Permissions.Add(lPermission);
                    }
                }
            }
        }

        public bool HasPermission(string requiredPermission)
        {
            bool bFound = false;
            bFound = Permissions.Where(
                          p => p.PermissionName == requiredPermission).ToList().Count > 0;
            return bFound;
        }

        public bool HasRole(string role)
        {
            return (Roles.Where(p => p.RoleName == role).ToList().Count > 0);
        }
        public bool HasRoles(string[] roles)
        {
            return (Roles.Where(p => roles.Contains(p.RoleName)).ToList().Count > 0);
        }

        public void setName()
        {
            using (AssetTrackingEntities _data = new AssetTrackingEntities())
            {
                user _user = _data.users.Where(u => u.User_Id == this.User_Id).FirstOrDefault();
                if (_user != null)
                {
                    this.Name = _user.FirstName;
                }
            }
        }
    }
}