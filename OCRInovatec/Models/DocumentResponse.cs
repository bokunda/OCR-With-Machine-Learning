using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCRInovatec.Models
{
    public class DocumentResponse
    {
        public long Id { get; set; }
        public String File_Name { get; set; }
        public String Document_Type { get; set; }
        public String Upload_By { get; set; }
        public String Folder_Name { get; set; }
    }
}