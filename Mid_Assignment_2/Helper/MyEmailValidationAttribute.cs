using Mid_Assignment_2.Controllers;
using Mid_Assignment_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid_Assignment_2.Helper
{
    public class MyEmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string Email = value.ToString();
                if (Email == Global.G_ID + "@student.aiub.edu")
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Your email formate should be like this: YourID@student.aiub.edu");
            }
            return new ValidationResult("Please provide your email");
        }
    }
}