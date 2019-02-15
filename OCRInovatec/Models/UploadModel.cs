using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OCRInovatec.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "Please select file.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.pdf)$", ErrorMessage = "Only Image and pdf files allowed.")]
        private HttpPostedFileBase[] postedFile;
        private string virtualPath;


        public HttpPostedFileBase[] PostedFiles { get; set; }
        public String VirtualPath { get; set; }
    }
}