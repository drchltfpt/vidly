using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check the selected membership type => get ObjectInstance
            var customer = (Customer)validationContext.ObjectInstance;

            // Membership type is Pay as you go => don't care about user's birthdate
            if (customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                // Success is static feild
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                // New a custom feild
                return new ValidationResult("Birthdate is required.");

            // Calculate customer's age
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}