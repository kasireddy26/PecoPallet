using SampleTracking.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleTracking.Controllers
{
    [RoutePrefix("api/Permissions")]
    public class PermissionsController : ApiController
    {
        AssetTrackingEntities db = new AssetTrackingEntities();
        // GET: api/Permissions
        public List<PermissionDTO> Get()
        {
            List<PermissionDTO> dtos = new List<PermissionDTO>();
            var permissions = db.permissions.ToList();
            foreach (permission p in permissions)
            {
                PermissionDTO dto = new PermissionDTO
                {
                    Id = p.Permission_Id,
                    PermissionName = p.PermissionName,
                    PermissionDescription = p.PermissionDescription
                };
                dtos.Add(dto);
            }
            return dtos;
        }


    }
}