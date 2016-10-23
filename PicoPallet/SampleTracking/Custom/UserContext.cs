using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SampleTracking.Custom
{
    public class UserContext :DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
    }
}