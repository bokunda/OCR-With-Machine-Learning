using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OCRInovatec.ViewModel
{
    public class DirectoryFilesList
    {
        public List<FileInfo> finfo
        {
            get;
            set;
        }
        public List<DirectoryInfo> dinfo
        {
            get;
            set;
        }
    }
}