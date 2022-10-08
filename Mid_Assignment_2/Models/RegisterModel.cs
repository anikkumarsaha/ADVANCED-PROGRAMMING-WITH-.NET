using Mid_Assignment_2.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid_Assignment_2.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please provide your name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should contain 3 to 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s\.\-]*$", ErrorMessage = "Use letters/dashes/spaces/dots only please")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please provide your ID")]
        [RegularExpression(@"[0-9]{2}-[0-9]{5}-[1-3]", ErrorMessage = "ID has to be in xx-xxxxx-(1/2/3) this format")]
        public string ID { get; set; }


        [Required(ErrorMessage = "Please select a gender")]
        private string gender;
        public string Gender {
            set { gender = value; }
            get { return gender; }
        }


        [MyEmailValidation]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please provide a password")]
        [RegularExpression(@"^.*(?=.{3,})(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[\d])(?=.*[@!$#%]).*$", ErrorMessage = "Password should contain at least 1 uppercase, 1 lowercase, 1 special character and 1 number")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password should contain at least 8 characters")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please re-enter your password")]
        [Compare("Password", ErrorMessage = "Password didn't match")]
        public string Confirm_Password { get; set; }


        [MyDOBValidation]
        public string DOB { get; set; }
    }
}