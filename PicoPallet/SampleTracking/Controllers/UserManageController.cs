using SampleTracking.Custom;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SampleTracking.Controllers
{


    [RoutePrefix("api/UserManage")]
    public class UserManageController : ApiController
    {

        AssetTrackingEntities db = new AssetTrackingEntities();
        // GET: api/Permissions
        public List<AppUserDTO> Get()
        {
            List<AppUserDTO> dtos = new List<AppUserDTO>();
            var users = db.users.ToList();
            foreach (user u in users)
            {
                AppUserDTO dto = new AppUserDTO()
                {
                    UserID = u.User_Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    EmailID = u.MiddleName,
                    Username = u.Username
                };


            }
            return dtos;
        }

        [Route("{id}")]
        [ResponseType(typeof(AppUserDTO))]

        public IHttpActionResult Get(int id)
        {
            if (id == 0)
                return BadRequest();
            var u = db.users.Where(i=>i.User_Id==id).FirstOrDefault();
            if (u!=null)
            {
                AppUserDTO dto = new AppUserDTO()
                {
                    UserID = u.User_Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    EmailID = u.MiddleName,
                    Username = u.Username
                };
                dto.roles = new List<RolesDTO>();
                foreach (role r in u.roles)
                {
                    RolesDTO rdto = new RolesDTO
                    {
                        Id = r.Role_Id,
                        RoleName = r.RoleName,
                        RoleDescription = r.RoleDescription
                    };
                    rdto.permissions = new List<PermissionDTO>();
                    var permissions = r.permissions.ToList();
                    foreach (permission p in permissions)
                    {
                        PermissionDTO permDTO = new PermissionDTO
                        {
                            Id = p.Permission_Id,
                            PermissionName = p.PermissionName,
                            PermissionDescription = p.PermissionDescription
                        };
                        rdto.permissions.Add(permDTO);

                    }
                    dto.roles.Add(rdto);
                }
                return Ok(dto);
            }
            else
            return NotFound();
        }


        [HttpPost]
        [Route("Edit")]
        [ResponseType(typeof(AppUserDTO))]
        public IHttpActionResult Edit([FromBody] AppUserDTO rpdto)
        {
            var user = db.users.Where(i => i.User_Id == rpdto.UserID).FirstOrDefault();
            if(user == null)
            {
                return NotFound();
            }
          //  user.Email = rpdto.EmailID;
            user.FirstName = rpdto.FirstName;
            user.LastName = rpdto.LastName;
            user.MiddleName = rpdto.MiddleName;
        //    user.Username = rpdto.Username;
            db.Entry(user).State = EntityState.Modified;

            db.SaveChanges();
            AppUserDTO dto = new AppUserDTO()
            {
                UserID = user.User_Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailID = user.MiddleName,
                Username = user.Username
            };
            dto.roles = new List<RolesDTO>();
          
            return Ok(dto);
        }


        [HttpPost]
        [Route("EditRole")]
        [ResponseType(typeof(AppUserDTO))]
        public IHttpActionResult EditRole([FromBody] UserRPDTO rpdto)
        {
            var user = db.users.Where(i => i.User_Id == rpdto.Id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            foreach (int roleId in rpdto.AddRole)
            {
                var role = db.roles.Where(i => i.Role_Id == roleId).FirstOrDefault();
                var urole = user.roles.Where(i => i.Role_Id == roleId).FirstOrDefault();
                if (role != null && urole == null)
                {
                    user.roles.Add(role);
                }
            }
            foreach (int roleId in rpdto.RemRole)
            {
                var urole = user.roles.Where(i => i.Role_Id == roleId).FirstOrDefault();
                if (urole != null)
                {
                    user.roles.Remove(urole);

                }
            }
            db.Entry(user).State = EntityState.Modified;

            db.SaveChanges();
            AppUserDTO dto = new AppUserDTO()
            {
                UserID = user.User_Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailID = user.MiddleName,
                Username = user.Username
            };
            dto.roles = new List<RolesDTO>();
            foreach (role r in user.roles)
            {
                RolesDTO rdto = new RolesDTO
                {
                    Id = r.Role_Id,
                    RoleName = r.RoleName,
                    RoleDescription = r.RoleDescription
                };
                rdto.permissions = new List<PermissionDTO>();
                var permissions = r.permissions.ToList();
                foreach (permission p in permissions)
                {
                    PermissionDTO permDTO = new PermissionDTO
                    {
                        Id = p.Permission_Id,
                        PermissionName = p.PermissionName,
                        PermissionDescription = p.PermissionDescription
                    };
                    rdto.permissions.Add(permDTO);

                }
                dto.roles.Add(rdto);
            }

            return Ok(dto);
        }
    }
}
