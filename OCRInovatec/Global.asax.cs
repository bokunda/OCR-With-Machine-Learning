using OCRInovatec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OCRInovatec
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Session_OnStart()
        {
            System.Diagnostics.Debug.WriteLine("ULETEO");
        }

        public void Session_OnEnd()
        {
            System.Diagnostics.Debug.WriteLine("GOTOVO");
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            System.Diagnostics.Debug.WriteLine(Session["Username"] + "########################################");

            string username;

            if (Session["Username"] != null)
            {
                username = Session["Username"].ToString();

                var user = db.Users.Where(a => a.Username == username).FirstOrDefault().Id;

                FileService fs = new FileService();

                var docs = db.Documents.Where(d => d.Owner == user).ToList();

                foreach (var doc in docs)
                {
                    doc.Owner = null;
                    doc.DocumentLock = false;
                }

                db.SaveChanges();
            }
            
            
        }
    }
}
