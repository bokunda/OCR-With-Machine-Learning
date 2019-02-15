using OCRInovatec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCRInovatec.Services
{
    public class FileService
    {
        private OCRDatabaseEntities _db;
        public OCRDatabaseEntities Db { get { return _db; } set { _db = value; } }
        private DBService dbService = new DBService();
        public DBService DbService { get { return dbService; } set { dbService = value; } }

        public FileService()
        {
            _db = new OCRDatabaseEntities();
        }

        public bool LockDocument(int id, int? owner)
        {

            Document doc = Db.Documents.Find(id);
            if (doc.Owner != null)
            {
                return false;
            }
            else
            {
                doc.DocumentLock = true;
                doc.Owner = owner;
                Db.SaveChanges();
                return true;
            }
        }

        public bool UnlockDocument(int? id)
        {
            Document doc = Db.Documents.Find(id);
            doc.DocumentLock = false;
            doc.Owner = null;
            Db.SaveChanges();
            return true;
        }

        public List<Document> GetAllFolders()
        {
            IList<Document> virtualFolders = Db.Documents.ToList();

            List<DirectoryResponse> folders = new List<DirectoryResponse>();

            List<Document> listOfFolders = virtualFolders.GroupBy(f => f.Virtual_Path)
                          .Select(f => f.First())
                          .ToList();
            return listOfFolders;
        }

        public List<Document> GetAllVirutalFolders()
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            List<Document> virtualFolders = db.Documents.ToList();
            return virtualFolders;

        }

        public List<Document> GetAllFilesFromFolder(string folder)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            List<Document> files = db.Documents.Where(a => a.Virtual_Path == folder).ToList();
            return files;

        }

        public string GetDocType(Document file)
        {
            string type = "-";
            if (file.DocumentType == "0")
            {
                type = "Unknown";
            }
            else
            {
                if (file.DocumentType == "-1" || file.DocumentType == "1") type = "ID";
                else if (file.DocumentType == "-2" || file.DocumentType == "2") type = "Document";
                else if (file.DocumentType == "-3" || file.DocumentType == "3") type = "Form";
            }

            return type;
        }
        public DocumentResponse MakeDocumentResponse(string folder, Document file)
        {
            string type = GetDocType(file);
            string username = DbService.FindUserById(file.Uploader).Username;
            return new DocumentResponse() { Id = file.Id, File_Name = file.Name, Upload_By = username, Document_Type = type, Folder_Name = folder };
        }

        public Document GetFileById(int id)
        {
            Document file = DbService.FindDocumentById(id);
            return file;
        }

        public void RemoveDocuments(Document file)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();

            db.Documents.Remove(file);
            string fileName = file.Path;
            if ((System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
            }
            db.SaveChanges();
        }

        public int? GetOwner(int fileId)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            int? owner = db.Documents.Where(a => a.Id == fileId).FirstOrDefault().Owner;
            return owner;
        }

        public List<Document> GetFilesByOwner(int? owner)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            List<Document> docs = db.Documents.Where(d => d.Owner == owner).ToList();
            return docs;
        }

        public string GetFileText(int fileId)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            string text = db.Documents.Find(fileId).DocumentText;
            return text;
        }
    }

}