using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleTracking.UserScope;

namespace SampleTracking.Controllers
{
    public class AppUserController : Controller
    {
        AssetTrackingEntities db = new AssetTrackingEntities();

        // GET: AppUser
        //List Users

        [Authorize]
        [CustAuthAttribute("USRINDX")]

        public ActionResult Index()
        {
            var users = db.users.ToList();
            return View(users);
        }

        [Authorize]
        [CustAuthAttribute("USRINDX")]
        //get user
        public ActionResult UserRoles(int id)
        {
            var user = db.users.Where(i => i.User_Id == id).FirstOrDefault();
            return View(user);
        }


        [Authorize]
        [CustAuthAttribute("VIEWROLES")]
        //get user
        public ActionResult Roles()
        {
            var roles = db.roles.Where(i=>i.IsSysAdmin==false).ToList();
            return View(roles);
        }

        [Authorize]
        [CustAuthAttribute("CRTROLES")]

        public ActionResult CreateRole()
        {
            return View();
        }

        [Authorize]
        [CustAuthAttribute("VIEWROLES")]
        public ActionResult Role(int id)
        {
            var role = db.roles.Where(i=>i.Role_Id== id).FirstOrDefault();
            
            return View(role);
        }


   

    }
}