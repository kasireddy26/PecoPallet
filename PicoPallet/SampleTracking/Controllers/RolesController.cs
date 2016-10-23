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
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        AssetTrackingEntities db = new AssetTrackingEntities();
        // GET: api/Roles
        public List<RolesDTO> Get()
        {
            List<RolesDTO> dtos = new List<RolesDTO>();
            var roles = db.roles.ToList();
            foreach (role r in roles)
            {
                RolesDTO dto = new RolesDTO
                {
                    Id = r.Role_Id,
                    RoleName = r.RoleName,
                    RoleDescription=r.RoleDescription
                };
                dto.permissions = new List<PermissionDTO>();
                var permissions = r.permissions.ToList();
                foreach (permission p in permissions)
                {
                    PermissionDTO permDTO = new PermissionDTO
                    {
                        Id = p.Permission_Id,
                        PermissionName = p.PermissionName,
                        PermissionDescription = p.PermissionDescription
                    };
                    dto.permissions.Add(permDTO);

                }
                dtos.Add(dto);
            }
            return dtos;
        }
        [HttpGet]
        [ResponseType(typeof(RolesDTO))]
        [Route("{RoleId}")]
        public IHttpActionResult GetRole([FromUri] int RoleId)
        {
            RolesDTO dto = null;
            var r = db.roles.Where(i=>i.Role_Id==RoleId).FirstOrDefault();
           if(r!=null)
            {
                 dto = new RolesDTO
                {
                    Id = r.Role_Id,
                    RoleName = r.RoleName,
                    RoleDescription=r.RoleDescription
                };
                dto.permissions = new List<PermissionDTO>();
                var permissions = r.permissions.ToList();
                foreach (permission p in permissions)
                {
                    PermissionDTO permDTO = new PermissionDTO
                    {
                        Id = p.Permission_Id,
                        PermissionName = p.PermissionName,
                        PermissionDescription = p.PermissionDescription
                    };
                    dto.permissions.Add(permDTO);

                }
               
            }
            else
            {
                return NotFound();
            }
            return Ok(dto);
        }
        [HttpPost]
        [Route("Create")]
        [ResponseType(typeof(RolesDTO))]

        public IHttpActionResult CreateRole([FromBody]RolesDTO roledto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(roledto.RoleName))
            {
                role r = new role()
                {
                    RoleName=roledto.RoleName,
                    RoleDescription=roledto.RoleDescription
                   
                };
                r.SuperRoleId = 1;
                db.roles.Add(r);
                db.SaveChanges();
                roledto.Id = r.Role_Id;
                return Ok(roledto);
            }
            else
            return BadRequest("Role Name Empty");
        }
        [HttpPost]
        [ResponseType(typeof(RolesDTO))]
        [Route("EditPermission")]
        public IHttpActionResult EditPermissions([FromBody]RolesDTO roledto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (roledto.Id == 0)
            {
                return BadRequest("Invalid Id");
            }
            var roledb = db.roles.Where(i => i.Role_Id == roledto.Id).FirstOrDefault();
            if (roledb == null)
            {
                return NotFound();
            }
            foreach(int permId in roledto.AddPermission)
            {
                var perm = db.permissions.Where(i => i.Permission_Id == permId).FirstOrDefault();
                var rperm=  roledb.permissions.Where(i => i.Permission_Id == permId).FirstOrDefault();
                if (perm!=null&&rperm == null)
                {
                    roledb.permissions.Add(perm);
                }
            }
            foreach (int permId in roledto.RemPermission)
            {
                var rperm = roledb.permissions.Where(i => i.Permission_Id == permId).FirstOrDefault();
                if ( rperm != null)
                {
                    roledb.permissions.Remove(rperm);
                   
                }
            }
            db.Entry(roledb).State = EntityState.Modified;

            db.SaveChanges();
            roledto.RoleName = roledb.RoleName;
            roledto.RoleDescription = roledb.RoleDescription;

            roledto.permissions = new List<PermissionDTO>();
            var permissions = roledb.permissions.ToList();
            foreach (permission p in permissions)
            {
                PermissionDTO permDTO = new PermissionDTO
                {
                    Id = p.Permission_Id,
                    PermissionName = p.PermissionName,
                    PermissionDescription = p.PermissionDescription
                };
                roledto.permissions.Add(permDTO);

            }
            return Ok(roledto);
          }

    }
}
