using SampleTracking.Custom;
using SampleTracking.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleTracking.Controllers
{
    [Authorize]
 [RoutePrefix("api/Ping")]
    public class PingController : ApiController
    {

        [Route("Locations")]
        public List<LocationDTO> GetLocations()
        {
            List<LocationDTO> dtos = new List<LocationDTO>();
         
     
          dtos=  PingDAO.GetLocations();
            return dtos;

        }

        [Route("Locations/{DeviceId}")]
        public List<LocationDTO> GetLocationsFromDeviceID([FromUri]string DeviceId)
        {
            List<LocationDTO> dtos = new List<LocationDTO>();


            dtos = PingDAO.GetLocations(DeviceId);
            return dtos;
        }

    }
}
