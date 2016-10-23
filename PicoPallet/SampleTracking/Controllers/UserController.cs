using SampleTracking.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleTracking.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            UserContext userContext = new UserContext();
           List<UserDTO>  dtos = userContext.Users.ToList();
           
            return View(dtos );
        }



        public ActionResult Details(int id)
        {
            UserContext userContext = new UserContext();
            UserDTO dto = userContext.Users.Single(usr => usr.UserID == id);

            return View(dto);
        }   

    }
}