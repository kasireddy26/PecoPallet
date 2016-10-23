using Microsoft.AspNet.Identity.Owin;
using SampleTracking.UserScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleTracking.Controllers
{
    public class MapsController : Controller
    {
        AssetTrackingEntities db = new AssetTrackingEntities();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {

                return _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

      //[Authorize]
      //[CustAuthAttribute("TRK")]

        // GET: Maps`

        public ActionResult Index()
        {
            return View();
        }
       //[Authorize]
       //[CustAuthAttribute("TRK")]

        // GET: Maps`

        public ActionResult Test()
        {
            return View();
        }
        //[Authorize]
        public ActionResult MapDrawing()
        {
            return View();
        }
        public ActionResult DeviceView (string DeviceId)
        {
            return View("DeviceView");
        }

        public ActionResult Drawing()
        {
            return View("Drawing");
        }
        //[Authorize]
        //[CustAuthAttribute("CRTUSR")]
        public ActionResult CreateUser()
        {
            return View();
        }
        //[Authorize]
        //[CustAuthAttribute("USRINDX")]
        public ActionResult ListUsers()
        {
            return View();
        }

        //[Authorize]
        //[CustAuthAttribute("PLCY")]
        public ActionResult PalletLifeCycle()
        {
            return View();
        }
    }
}