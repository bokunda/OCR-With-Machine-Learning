using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OCRInovatec.ViewModel;
using System.Runtime.Remoting.Contexts;
using System.Web.Security;
using System.Net;
using System.Web.Http.Description;
using OCRInovatec.Services;
using OCRInovatec.Models;
using System.Text;

namespace OCRInovatec.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document

        FileService fileService = new FileService();
        FileService FileService { get; set; }
        [HttpGet]
        public ActionResult UploadDocument()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadDocument(UploadModel files)
        {
            HttpPostedFileBase[] postedFile = files.PostedFiles;
            string virtualPath = files.VirtualPath;

            if (virtualPath == null)
                virtualPath = "default";
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            virtualPath = virtualPath.Trim().Replace(' ','-');
            string path = Server.MapPath("~/Uploads/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            OCRDatabaseEntities db = new OCRDatabaseEntities();
            List<string> extensions = new List<string>(new string[] { ".jpg", ".jpeg", ".png", ".gif", ".JPG", ".PNG", ".JPEG", ".GIF" });

            if (postedFile != null)
            {

                foreach (HttpPostedFileBase file in postedFile)
                {
                    if (file != null)
                    {

                        //FileService.saveFile(ServerPathName);
                        string extension = Path.GetExtension(file.FileName);
                        var InputFileName = Path.GetFileName(file.FileName);
                        if (extensions.Contains(extension))
                        {
                            var ServerPathName = path + virtualPath + "_" + InputFileName;
                            InputFileName = virtualPath + "_" + InputFileName;


                            var list = db.Documents.Select(item => item.Path == ServerPathName).ToList();

                            int count = list.Count;
                            file.SaveAs(ServerPathName);

                            string messageText;
                            var uploader = db.Users.Where(a => a.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Id;

                            System.Diagnostics.Debug.WriteLine(uploader);
                            Document doc = new Document()
                            {
                                Name = InputFileName,
                                Path = ServerPathName,
                                DocumentText = null,
                                DocumentType = "0",
                                DocumentLock = false,
                                Owner = null,
                                Uploader = uploader,
                                Language = "en",
                                Virtual_Path = virtualPath
                            };

                            if (count > 0)
                            {
                                var duplicateDocs = db.Documents.Where(c => c.Path == ServerPathName);
                                foreach (var dupDoc in duplicateDocs)
                                {
                                    //db.Documents.Remove(dupDoc);
                                    db.Documents.Remove(dupDoc);
                                }

                            }

                            db.Documents.Add(doc);
                            db.SaveChanges();
                            System.Diagnostics.Debug.WriteLine(db.Documents.ToList()[0]);
                            //file.SaveAs(path);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.Message += string.Format("<span style='color: green;'><b>{0}</b> uploaded.<br /><span>", InputFileName);
                            TempData["ourmessage"] += string.Format("<span style='color: green;'><b>{0}</b> uploaded.<br /></span>", InputFileName);
                        }
                        else
                        {
                            ViewBag.Message += string.Format("<span style='color: red;'><font color=\"red\"><b>{0}</b> cannot be uploaded. Wrong file format.<br /></font></span>", InputFileName);
                            TempData["ourmessage"] += string.Format("<span style='color: red;'><font color=\"red\"><b>{0}</b> cannot be uploaded. Wrong file format.<br /></font></span>", InputFileName);
                        }
                    }
                }
            }

            return RedirectToAction("UploadDocument");
        }

        // GET: Document
        public ActionResult ViewDocuments()
        {
            string str = System.Web.HttpContext.Current.User.Identity.Name;
            ViewBag.Message = "Your file page.";
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\");
            List<FileInfo> files = dirInfo.GetFiles().ToList();
            Console.WriteLine(" ");
            //List<DirectoryInfo> directories = dirInfo.GetDirectories().ToList();
            List<DirectoryInfo> dir = dirInfo.GetDirectories().ToList();
            //pass the data trough the "View" method

            DirectoryFilesList naziv = new DirectoryFilesList();
            naziv.dinfo = dir;
            naziv.finfo = files;
            return View(naziv);
        }

        public ActionResult Image()
        {

            return View();
        }
    }
}