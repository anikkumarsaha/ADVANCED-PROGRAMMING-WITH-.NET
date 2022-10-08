using Mid_Assignment_2.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mid_Assignment_2.Helper
{
    public class MyDOBValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Global.G_DOB = value.ToString();
            DateTime InputDate = Convert.ToDateTime(Global.G_DOB);
            int age = (int)((DateTime.Now - InputDate).TotalDays / 365.242199);

            if (value != null)
            {
                if (age > 18)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Go home kid. Only 18+");
            }
            return new ValidationResult("Please provide your date of birth");
        }
    }
}