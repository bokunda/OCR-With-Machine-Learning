using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OCRInovatec.Models;
using System.Diagnostics;

namespace OCRInovatec.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string ReturnUrl = "")
        {
            using (OCRDatabaseEntities db = new OCRDatabaseEntities())
            {
                var user = db.Users.Where(a => a.Username.Equals(l.Username) && a.Password.Equals(l.Password)).FirstOrDefault();
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, l.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["Username"] = l.Username;
                        return RedirectToAction("UploadDocument", "Document");
                    }
                }
                else
                {
                    ModelState.AddModelError("Log", "Invalid Username or Password.");
                }

            }
            ModelState.Remove("Password");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public JsonResult IsUserExists(string UserName)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method. 
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            return Json(!db.Users.Any(x => x.Username == UserName), JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsUserExists2(string email)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            return Json(!db.Users.Any(x => x.Email == email), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register r, string ReturnUrl = "")
        {
            using (OCRDatabaseEntities db = new OCRDatabaseEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(new User
                    {
                        Username = r.Username,
                        Firstname = r.FirstName,
                        Lastname = r.LastName,
                        Email = r.Email,
                        Password = r.Password
                    });

                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(r.Username, r.RememberMe); if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("UploadDocument", "Document");
                    }
                }
            }

            ModelState.Remove("Password");
            return View();
        }
    }
}