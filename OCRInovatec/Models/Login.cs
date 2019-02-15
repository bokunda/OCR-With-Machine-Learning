using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OCRInovatec.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}