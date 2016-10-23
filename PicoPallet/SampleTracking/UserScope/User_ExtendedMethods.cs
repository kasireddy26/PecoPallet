using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleTracking.UserScope
{
    public static class User_ExtendedMethods
    {
        public static bool HasPermission(this ControllerBase controller, string permission)
        {
            bool Found = false;
            try
            {
                Found = new AppUser(controller.ControllerContext
                                     .HttpContext.User.Identity.Name).HasPermission(permission);
            }
            catch { }
            return Found;
        }
    }
}