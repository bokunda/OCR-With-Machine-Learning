using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCRInovatec.Models
{
    public class DirectoryResponse
    {
        public string Folder_Name { get; set; }
        public string Last_Modified { get; set; }
        public int Files { get; set; }
    }
}