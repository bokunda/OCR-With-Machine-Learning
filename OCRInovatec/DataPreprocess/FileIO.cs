using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPreprocess
{
    static class FileIO
    {
        public static string readFile(string filePath)
        {
            try
            {
                string text = System.IO.File.ReadAllText(@filePath);
                return text;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static void CSVWrite(string text, int type, string filePath)
        {
            var newLine = string.Format("{0},{1}\n", text, type);
            File.AppendAllText(filePath, newLine);

        }

    }
}
