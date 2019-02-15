using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OCRInovatec.Models;
using System.Diagnostics;
using OCRInovatec;

namespace OCRInovatec.Services
{
    public class DBService
    {
        public User FindUserById(long id)
        {
            using (OCRDatabaseEntities db = new OCRDatabaseEntities())
            {
                var user = db.Users.Where(a => a.Id == id).FirstOrDefault();

                return user;
            }
        }
        public Document FindDocumentById(long id)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();

            var document = db.Documents.Where(a => a.Id == id).FirstOrDefault();

            return document;

        }
        public void UpdateDocument(string text, long id)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();

            try
            {
                Document doc = db.Documents.Find(id);
                doc.DocumentText = text;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }

        }

        public bool ChangeText(int id, string text)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            Document doc = db.Documents.Find(id);
            doc.DocumentText = text;
            db.SaveChanges();
            return true;
        }

        public void SaveDocument(Document doc)
        {
            using (OCRDatabaseEntities db = new OCRDatabaseEntities())
            {

                db.Documents.Add(new Document
                {
                    Name = doc.Name,
                    Path = doc.Path,
                    DocumentText = doc.DocumentText,
                    DocumentType = doc.DocumentType,
                    DocumentLock = doc.DocumentLock,
                    Owner = doc.Owner,
                    Uploader = doc.Uploader,
                    Language = doc.Language,
                    Virtual_Path = doc.Virtual_Path

                });
                db.SaveChanges();
            }
        }
    }
}