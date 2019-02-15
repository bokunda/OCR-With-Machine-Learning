using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using OCRInovatec.Services;
using OCRInovatec.Models;
using DataPreprocess;

namespace OCRInovatec.Controllers
{
    public class DocumentGetController : ApiController
    {
        private FileService fileService = new FileService();
        public FileService DocFileService { get { return fileService; } set { fileService = value; } }
        private DocumentClassificationService docClassService = new DocumentClassificationService();
        public DocumentClassificationService DocClassService { get { return docClassService; } set { docClassService = value; } }

        private DBService dbService = new DBService();
        public DBService DbService { get { return dbService; } set { dbService = value; } }


        private UserService userService = new UserService();
        public UserService AppUserService { get { return userService; } set { userService = value; } }

        private OCRService ocrService = new OCRService();
        public OCRService OcrService { get { return ocrService; } set { ocrService = value; } }
        /// <summary>
        /// ODRADJENOOO
        /// </summary>
        /// <returns></returns>
        [Route("api/virtualpath")]
        public IHttpActionResult GetAllFolders()
        {
            //OCRDatabaseEntities db = new OCRDatabaseEntities();
            List<Document> virtualFolders = DocFileService.GetAllVirutalFolders();

            List<DirectoryResponse> folders = new List<DirectoryResponse>();

            //var forReturn = virtualFolders.GroupBy(f => f.Virtual_Path)
            //              .Select(f => f.First())
            //              .ToList();


            List<Document> allFolders = DocFileService.GetAllFolders();

            //DBService dbService = new DBService();

            foreach(Document f in allFolders)
            {
                var user = "-";
                if (DbService.FindUserById(f.Uploader) != null)
                {
                    user = dbService.FindUserById(f.Uploader).Username;
                }
                folders.Add(new DirectoryResponse() { Folder_Name = f.Virtual_Path, Last_Modified = user, Files = virtualFolders.Where(n => n.Virtual_Path == f.Virtual_Path).Count() });            
            }

            if (virtualFolders == null)
            {
                return NotFound();
            }

            return Ok(folders);
        }

        //[Route("api/docs")]
        //public IHttpActionResult GetUsers(long id)
        //{
        //    OCRDatabaseEntities db = new OCRDatabaseEntities();
        //    Document doc = db.Documents.Find(id);

        //    if (doc == null)
        //    {
        //        //Debug.WriteLine("cao1");
        //        return NotFound();
        //    }
        //    //Debug.WriteLine("cao");
        //    return Ok(doc);
        //}

        //[Route("api/alldocs")]
        //public IHttpActionResult GetAllDocs()
        //{
        //    OCRDatabaseEntities db = new OCRDatabaseEntities();
        //    List<Document> docs = db.Documents.ToList();

        //    if (docs.Count == 0)
        //    {
        //        //Debug.WriteLine("cao1");
        //        return NotFound();
        //    }
        //    //Debug.WriteLine("cao");
        //    return Ok(docs);
        //}
        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>

        [Route("api/virtualpath")]
        public IHttpActionResult GetFilesFromFolder(string folder)
        {

            //OCRDatabaseEntities db = new OCRDatabaseEntities();
            // DBService dbService = new DBService();
            List<Document> files = DocFileService.GetAllFilesFromFolder(folder);

            List<DocumentResponse> docs = new List<DocumentResponse>();

            foreach(var f in files)
            {
                
                //string type = DocFileService.getDocType(f);
                //string username = DbService.FindUserById(f.Uploader).Username;
                docs.Add(DocFileService.MakeDocumentResponse(folder,f));
            }

            if (files.Count == 0)
            {
                //Debug.WriteLine("cao1");
                return NotFound();
            }
            //Debug.WriteLine("cao");
            return Ok(docs);


        }

        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{folder}")]
        public IHttpActionResult GetFileFromFolder(int id)
        {

            //OCRDatabaseEntities db = new OCRDatabaseEntities();
            Document file = DocFileService.GetFileById(id);
            if (file == null)
            {
                //Debug.WriteLine("cao1");
                return NotFound();
            }
            //Debug.WriteLine("cao");
            return Ok(file);


        }



        [Route("api/virtualpath/{folder}/delete")]
        [AcceptVerbs("DELETE", "GET")]
        public IHttpActionResult DeleteFolder(string folder)
        {
            //string[] path = folderAndName.Split('/');
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            List<Document> files = db.Documents.Where(a => a.Virtual_Path == folder).ToList();

            if (files.Count == 0)
            {
                //Debug.WriteLine("cao1");

                return NotFound();
            }
            //Debug.WriteLine("cao");
            foreach (Document doc in files)
            {
                db.Documents.Remove(doc);
                string file = doc.Path;
                if ((System.IO.File.Exists(file)))
                {
                    System.IO.File.Delete(file);
                }
            }
            db.SaveChanges();
            return Ok();



        }


        [Route("api/virtualpath/{folder}/{fileid:int}/delete")]
        [AcceptVerbs("DELETE", "GET")]
        public IHttpActionResult DeleteFile(string folder, int fileid)
        {
            //string[] path = folderAndName.Split('/');
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            Document file = db.Documents.Where(a => a.Virtual_Path == folder && a.Id == fileid).FirstOrDefault();

            if (file == null)
            {
                //Debug.WriteLine("cao1");

                return NotFound();
            }
            //Debug.WriteLine("cao");
            //foreach (Document doc in files)
            //    db.Documents.Remove(doc);
            db.Documents.Remove(file);
            if ((System.IO.File.Exists(file.Path)))
            {
                System.IO.File.Delete(file.Path);
            }
            db.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{fileId:int}/isLocked")]
        [AcceptVerbs("GET")]
        public IHttpActionResult IsLocked(int fileId)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            var owner = DocFileService.GetOwner(fileId);
            DBService dbService = new DBService();

            if (owner != null)
            {
                string ownerName = DbService.FindUserById((int)owner).Username;

                if (System.Web.HttpContext.Current.User.Identity.Name == ownerName)
                {
                    return Ok(true);
                }

                return Ok(false);
            }

            return Ok(-1);
        }


        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <returns></returns>
        [Route("api/virtualpath/unlockAll")]
        [AcceptVerbs("GET")]
        public IHttpActionResult UnlockAll()
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            //get { return dbService; } set { dbService = value; }
            int? user = AppUserService.getCurrentUser();
            System.Diagnostics.Debug.WriteLine(user + " USER ID ");
            //FileService fs = new FileService();

            var docs = DocFileService.GetFilesByOwner(user);

            foreach (var doc in docs)
            {
                DocFileService.UnlockDocument(doc.Id);
            }

            db.SaveChanges();

            return Ok(1991);
        }
        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>

        [Route("api/virtualpath/{fileId:int}/lock")]
        [AcceptVerbs("PUT", "GET")]
        public IHttpActionResult LockFile(int fileId)
        {
            //OCRDatabaseEntities db = new OCRDatabaseEntities();
            //var owner = db.Users.Where(a => a.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Id;
            int? owner = AppUserService.getCurrentUser();
            //fileService = new FileService();
            
            if (!fileService.LockDocument(fileId,owner))
            {
                //Debug.WriteLine("cao1");
                return NotFound();
            }
            //Debug.WriteLine("cao");
            return Ok();
        }

        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{fileId:int}/unlock")]
        [AcceptVerbs("PUT", "GET")]
        public IHttpActionResult UnlockFile(int fileId)
        {
            //fileService = new FileService();

            if (!DocFileService.UnlockDocument(fileId))
            {
                //Debug.WriteLine("cao1");
                return NotFound();
            }
            //Debug.WriteLine("cao");
            return Ok();


        }

        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{fileId:int}/ocr")]
        [AcceptVerbs("PUT", "GET")]
        public IHttpActionResult FileOCR(int fileId)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();

            
            System.Diagnostics.Debug.WriteLine("Init TEXT:" + db.Documents.Find(fileId).DocumentText);
            string text = DocFileService.GetFileText(fileId); 
                //db.Documents.Find(fileId).DocumentText;
            if (text == null)
            {
                System.Diagnostics.Debug.WriteLine("usao u prvi if");
                if ((text = OcrService.RecognizeText(fileId)).Length == 0)
                {
                    //System.Diagnostics.Debug.WriteLine("usao u drugi if");
                    //Debug.WriteLine("cao1");
                    return NotFound();
                }
                //text = db.Documents.Find(fileId).DocumentText;
            }
            
            //Debug.WriteLine("cao");

            //System.Diagnostics.Debug.WriteLine("TEXT:" +doc.DocumentText);
            //Document doc = db.Documents.Find(fileId);

            //System.Diagnostics.Debug.WriteLine("Controller TEXT:" + doc.DocumentText);
            return Ok(text);
        }
        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{fileId:int}/againocr")]
        [AcceptVerbs("PUT", "GET")]
        public IHttpActionResult AgainOCR(int fileId)
        {
            //OCRDatabaseEntities db = new OCRDatabaseEntities();            
            //System.Diagnostics.Debug.WriteLine("Init TEXT:" + db.Documents.Find(fileId).DocumentText);
            string text = DocFileService.GetFileText(fileId);

            if ((text = OcrService.RecognizeText(fileId)).Length == 0)
                {
                    //System.Diagnostics.Debug.WriteLine("usao u drugi if");
                    //Debug.WriteLine("cao1");
                    return NotFound();
                }
                //text = db.Documents.Find(fileId).DocumentText;
            

            //Debug.WriteLine("cao");

            //System.Diagnostics.Debug.WriteLine("TEXT:" +doc.DocumentText);
            //Document doc = db.Documents.Find(fileId);

            //System.Diagnostics.Debug.WriteLine("Controller TEXT:" + doc.DocumentText);
            return Ok(text);
        }
        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{fileId:int}/update")]
        [AcceptVerbs("PUT", "GET", "POST")]
        public IHttpActionResult UpdateFile(int fileId)
        {                    
            HttpContent requestContent = Request.Content;
            System.Diagnostics.Debug.WriteLine(requestContent);
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            //System.Diagnostics.Debug.WriteLine(jsonContent.GetType());
            //string text = JsonConvert.DeserializeObject<String>(jsonContent);
            dynamic jsonText = JsonConvert.DeserializeObject(jsonContent);

            string text = JsonConvert.SerializeObject(jsonText.text);

            /*
            string text = "";
            for(int i = 0; i < 40; i++)
            {
                text += jsonText.text[i].Text;
                text += jsonText.text[i].Coords;
            }
            */

            System.Diagnostics.Debug.WriteLine(text);

            //string text = jsonText.text;
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            //Document doc = DocFileService.getFileById(fileId);
            Document doc = db.Documents.Find(fileId);
            if (doc == null)
            { 
                    return NotFound();
            }
            System.Diagnostics.Debug.WriteLine("Tekst na pocetku:" + doc.DocumentText + " , id = "  + doc.Id + ", request id: "  + fileId);
            doc.DocumentText = text;
            //db.SaveChanges();
            db.SaveChanges();
            System.Diagnostics.Debug.WriteLine("Tekst na pocetku:" + doc.DocumentText);
            DocFileService.UnlockDocument(fileId);
            return Ok();
            //
            //Document doc = db.Documents.Find(fileId);
            //if (doc.DocumentText == null)
            //{
            //    OCRService ocr = new OCRService();

            //    if (!ocr.RecognizeText(fileId))
            //    {
            //        //Debug.WriteLine("cao1");
            //        return NotFound();
            //    }
            //}
            ////Debug.WriteLine("cao");
            //return Ok(doc.DocumentText);
        }

        /// <summary>
        /// ODRADJENO
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Route("api/virtualpath/{fileId:int}/classify")]
        [AcceptVerbs("PUT", "GET")]
        public IHttpActionResult ClassifyDocument(int fileId)
        {
            //OCRDatabaseEntities db = new OCRDatabaseEntities();
            Document doc = DocFileService.GetFileById(fileId);
            string type = doc.DocumentType;
            //string result = null;
            if (type == "0")
            {
                type = DocClassService.classify(fileId);
            } 
            
            if (type != null) return Ok(type);
            return NotFound();
            //return Ok(result);
        }



        [Route("api/virtualpath/{fileId:int}/type")]
        [AcceptVerbs("PUT", "GET", "POST")]
        public IHttpActionResult UpdateDocumentType(int fileId)
        {
            HttpContent requestContent = Request.Content;
            System.Diagnostics.Debug.WriteLine(requestContent);
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            //System.Diagnostics.Debug.WriteLine(jsonContent.GetType());
            //string text = JsonConvert.DeserializeObject<String>(jsonContent);
            dynamic jsonText = JsonConvert.DeserializeObject(jsonContent);

            string type = JsonConvert.SerializeObject(jsonText.type);
            int val = Int32.Parse(type);
            /*
            string text = "";
            for(int i = 0; i < 40; i++)
            {
                text += jsonText.text[i].Text;
                text += jsonText.text[i].Coords;
            }
            */

            //System.Diagnostics.Debug.WriteLine(type);

            //string text = jsonText.text;
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            Document doc = db.Documents.Find(fileId);
            if (doc == null)
            {
                return NotFound();
            }
            doc.DocumentType = type;
            System.Diagnostics.Debug.WriteLine("Type: " + type);
            db.SaveChanges();
            string dataFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Data/data_train.csv");
            string processedText = TextPreprocessorService.parseJSONText(db.Documents.Find(fileId).DocumentText);
            //System.Diagnostics.Debug.WriteLine("Nakon JSON parse-a:" + processedText);

            processedText = TextPreprocessorService.ProcessText(ref processedText);

            FileIO.CSVWrite(processedText, val, dataFilePath);
            fileService.UnlockDocument(fileId);
            return Ok();
            //
            //Document doc = db.Documents.Find(fileId);
            //if (doc.DocumentText == null)
            //{
            //    OCRService ocr = new OCRService();

            //    if (!ocr.RecognizeText(fileId))
            //    {
            //        //Debug.WriteLine("cao1");
            //        return NotFound();
            //    }
            //}
            ////Debug.WriteLine("cao");
            //return Ok(doc.DocumentText);
        }



    }
}
