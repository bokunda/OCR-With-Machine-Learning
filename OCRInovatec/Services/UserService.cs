using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCRInovatec.Services
{
    public class UserService
    {
        public int? getCurrentUser()
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            int? user = db.Users.Where(a => a.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Id;
            return user;
        }
    }
}