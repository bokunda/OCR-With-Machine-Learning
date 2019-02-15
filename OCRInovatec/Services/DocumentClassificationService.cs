using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataAccess;
using DataPreprocess;
using libsvm;
using OCRInovatec;
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
    public class DocumentClassificationService
    {
        OCRDatabaseEntities db = new OCRDatabaseEntities();
        OCRDatabaseEntities Db { get { return db; } set { db = value; } }

        Dictionary<int, string> _predictionDictionary;
        OCRService ocr = new OCRService();
        OCRService OCRService { get { return ocr; } set { ocr = value; } }

        public int PredictByNumOfWords()
        {
            int numOfWords = TextPreprocessorService.count;
            TextPreprocessorService.clearCount();
            int retVal = 0;
            if (numOfWords > 250) retVal = 2;
            return retVal;
        }

        public string RecognizeDocText(int id)
        {
            System.Diagnostics.Debug.WriteLine(OCRService.FaceFlag);
            return OCRService.RecognizeText(id);
        }

        public string ProcessText(string input)
        {
            return TextPreprocessorService.ProcessText(ref input);
        }

        public Dictionary<int, double> PredictByText(string input)
        {
            // STEP 4: Read the data

            string dataFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Data/data_train.csv");
            var dataTable = DataAccess.DataTable.New.ReadCsv(dataFilePath);
            List<string> x = dataTable.Rows.Select(row => row["Text"]).ToList();

            double[] y = dataTable.Rows.Select(row => double.Parse(row["Type"])).ToArray();

            var vocabulary = x.SelectMany(GetWords).Distinct().OrderBy(word => word).ToList();
            Console.WriteLine("Creating problem");
            var problemBuilder = new DataPreprocess.TextClassificationProblemBuilder();
            var problem = problemBuilder.CreateProblem(x, y, vocabulary.ToList());

            //        // If you want you can save this problem with : 
            //        //ProblemHelper.WriteProblem(@"C:\Users\", problem);
            //        // And then load it again using:
            //        //var problem2 = ProblemHelper.ReadProblem(@"D:\MACHINE_LEARNING\SVM\Tutorial\sunnyData.problem");

            System.Diagnostics.Debug.WriteLine("Creating model");
            const int C = 1;
            var model = new C_SVC(problem, KernelHelper.LinearKernel(), C, 100, true);

            var accuracy = model.GetCrossValidationAccuracy(10);

            System.Diagnostics.Debug.WriteLine(new string('=', 50));
            System.Diagnostics.Debug.WriteLine("Accuracy of the model is {0:P}", accuracy);
            model.Export(string.Format(@"model_{0}_accuracy.model", accuracy));

            System.Diagnostics.Debug.WriteLine(new string('=', 50));
            System.Diagnostics.Debug.WriteLine("The model is trained. \r\nEnter a sentence to make a prediction.");
            System.Diagnostics.Debug.WriteLine(new string('=', 50));

            _predictionDictionary = new Dictionary<int, string> { { 1, "ID" }, { 2, "Documents" }, { 3, "Forme" } };

            int numOFWords = 0;
            string processedText = TextPreprocessorService.parseJSONText(input);

            processedText = TextPreprocessorService.ProcessText(ref processedText);
            Dictionary<int, double> dict = new Dictionary<int, double>() { { 1, 0 }, { 2, 0 }, { 3, 0 } };
            if (processedText.Equals(""))
                return dict;

            var newX = TextClassificationProblemBuilder.CreateNode(processedText, vocabulary);
            var predictedY = model.Predict(newX);
            System.Diagnostics.Debug.WriteLine(predictedY);

            dict = model.PredictProbabilities(newX);
            System.Diagnostics.Debug.WriteLine("Prob(1): " + dict[1]);
            System.Diagnostics.Debug.WriteLine("Prob(2): " + dict[2]);
            System.Diagnostics.Debug.WriteLine("Prob(3): " + dict[3]);

            System.Diagnostics.Debug.WriteLine("The prediction is {0}  value is {1} ", _predictionDictionary[(int)predictedY], predictedY);

            return dict;
        }

        public string classify(int id)
        {
            OCRDatabaseEntities db = new OCRDatabaseEntities();
            Document doc = db.Documents.Find(id);
            string text = doc.DocumentText;

            if ((text == null))
            {
                text = RecognizeDocText(id);
                text = TextPreprocessorService.parseJSONText(text);
                text = TextPreprocessorService.ProcessText(ref text);

                if (text == null)
                    return null;
            }

            Dictionary<int, double> dict = PredictByText(text);

            System.Diagnostics.Debug.WriteLine("ByText");
            System.Diagnostics.Debug.WriteLine(dict[1].ToString());
            System.Diagnostics.Debug.WriteLine(dict[2].ToString());
            System.Diagnostics.Debug.WriteLine(dict[3].ToString());

            int predictionNumOFWords = PredictByNumOfWords();
            System.Diagnostics.Debug.WriteLine(predictionNumOFWords);
            if (predictionNumOFWords == 2) dict[2] += 0.2;

            System.Diagnostics.Debug.WriteLine("ByNumOfWords");
            System.Diagnostics.Debug.WriteLine(dict[1].ToString());
            System.Diagnostics.Debug.WriteLine(dict[2].ToString());
            System.Diagnostics.Debug.WriteLine(dict[3].ToString());

            System.Diagnostics.Debug.WriteLine(OCRService.FaceFlag);

            int faceFlag = OCRService.FaceFlag;

            System.Diagnostics.Debug.WriteLine("FaceFlag");
            System.Diagnostics.Debug.WriteLine(faceFlag);

            if (faceFlag == 1) dict[1] += 0.75;

            System.Diagnostics.Debug.WriteLine("ByFace");
            System.Diagnostics.Debug.WriteLine(dict[1].ToString());
            System.Diagnostics.Debug.WriteLine(dict[2].ToString());
            System.Diagnostics.Debug.WriteLine(dict[3].ToString());

            double maxPerc = dict.Values.Max();
            if (maxPerc <= 0.1)
                return "0";
            string retVal = dict.FirstOrDefault(x => x.Value == maxPerc).Key.ToString();

            string type = "-" + retVal;
            doc.DocumentType = type;
            System.Diagnostics.Debug.WriteLine(doc.DocumentType);
            db.SaveChanges();
            return type;

        }

        private static IEnumerable<string> GetWords(string x)
        {
            return x.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}