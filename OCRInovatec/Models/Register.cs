using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OCRInovatec.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Username required.", AllowEmptyStrings = false)]
        [System.Web.Mvc.Remote("IsUserExists", "User", ErrorMessage = "Username already in use")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Firstname required.", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname required.", AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email required.", AllowEmptyStrings = false)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [System.Web.Mvc.Remote("IsUserExists2", "User", ErrorMessage = "Email already in use")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password required.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}