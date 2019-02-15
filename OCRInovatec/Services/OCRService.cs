using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesseract;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Web.Mvc;
using OCRInovatec.ViewModel;
using System.Runtime.Remoting.Contexts;
using System.Web.Security;
using System.Net;
using System.Web.Http.Description;
using OCRInovatec.Services;
using OCRInovatec.Models;
using System.Text;
using Leptonica;
using System.Drawing;
using Newtonsoft.Json;
using Emgu.CV;
using Emgu.CV.Structure;

namespace OCRInovatec.Services
{
    public class OCRService
    {
        OCRDatabaseEntities db = new OCRDatabaseEntities();
        OCRDatabaseEntities Db { get { return db; } set { db = value; } }
        private int faceFlag;
        public int FaceFlag { get { return faceFlag; } set { faceFlag = value; } }
        FileService fileService = new FileService();

        public OCRService()
        {
        }
        public Image<Gray, Byte> scale(Document doc)
        {
            Image<Gray, Byte> img = new Image<Gray, Byte>(doc.Path);

            //img = img.Resize(3, Inter.Linear);
            return img;
        }

        public string GetText(Document doc)
        {
            return doc.DocumentText;
        }

        public bool HasFace(Image<Gray, Byte> img)
        {
            try
            {
                string facePath = System.Web.HttpContext.Current.Server.MapPath("~/Data/lbpcascade_frontalface_improved.xml");
                //string eyePath = Path.GetFullPath(@"C:\Users\bpisk\Desktop\haarcascade_eye.xml");

                CascadeClassifier classifierFace = new CascadeClassifier(facePath);
                //CascadeClassifier classifierEye = new CascadeClassifier(eyePath);

                var imgGray = img.Convert<Gray, byte>().Clone();
                var imgRGB = img.Convert<Rgba, byte>().Clone();

                Rectangle[] faces = classifierFace.DetectMultiScale(imgGray, 1.1, 4);
                //Rectangle[] eyes = classifierEye.DetectMultiScale(imgGray, 1.1, 4);

                foreach (var face in faces)
                {
                    imgRGB.Draw(face, new Rgba(255, 0, 0, 0), 2);
                    imgGray.ROI = face;
                }


                if (faces.Length > 0) return true;

                return false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

        //public static void  clearFaceFlag() { faceFlag = 0; }
        public string RecognizeText(int id)
        {

            DBService dbs = new DBService();
            Document doc = dbs.FindDocumentById(id);

            Image<Gray, Byte> img = scale(doc);

            //var image = PixConverter.ToPix(img.ToBitmap()))

            Tesseract.Native.DllImports.TesseractDirectory = System.Web.HttpContext.Current.Server.MapPath("~/Tesseract/bin/Debug/DLLS/");
            TessBaseAPI tessBaseAPI = new TessBaseAPI();

            System.Diagnostics.Debug.WriteLine("The current version is {0}", tessBaseAPI.GetVersion());

            string dataPath = System.Web.HttpContext.Current.Server.MapPath("~/tessdata/");
            string language = "eng";

            string inputFile = doc.Path;
            OcrEngineMode oem = OcrEngineMode.DEFAULT;
            //OcrEngineMode oem = OcrEngineMode.DEFAULT;
            PageSegmentationMode psm = PageSegmentationMode.AUTO_OSD;

            // Initialize tesseract-ocr 
            if (!tessBaseAPI.Init(dataPath, language, oem))
            {
                throw new Exception("Could not initialize tesseract.");
            }

            // Set the Page Segmentation mode
            tessBaseAPI.SetPageSegMode(psm);

            // Set the input image
            Pix pix = tessBaseAPI.SetImage(inputFile);

            // Recognize image
            tessBaseAPI.Recognize();

            ResultIterator resultIterator = tessBaseAPI.GetIterator();

            // extract text from result iterator
            StringBuilder stringBuilder = new StringBuilder();
            int top, bottom, left, right, i = 0;

            List<OCRText> forJson = new List<OCRText>();

            PageIteratorLevel pageIteratorLevel = PageIteratorLevel.RIL_TEXTLINE;
            do
            {
                string textContent = resultIterator.GetUTF8Text(pageIteratorLevel);
                resultIterator.BoundingBox(pageIteratorLevel, out left, out top, out right, out bottom);
                string coordsString = "" + left + "," + top + "," + right + "," + bottom;

                forJson.Add(new OCRText() { Coords = coordsString, Text = textContent });

            } while (resultIterator.Next(pageIteratorLevel));

            tessBaseAPI.Dispose();
            pix.Dispose();

            var textForReturn = JsonConvert.SerializeObject(forJson);
            dbs.UpdateDocument(textForReturn, id);

            if (HasFace(img) == true)
            {
                FaceFlag = 1;
            }
            else
            {
                FaceFlag = 0;
            }

            return textForReturn;
        }

    }
}