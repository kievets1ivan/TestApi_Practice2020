using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApi.DataLayer;

namespace TestApi.ValidationAttributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
            var entity = context.ProductSet.SingleOrDefault(e => e.Name == value.ToString());

            if (entity != null)
            {
                return new ValidationResult($"Product {value.ToString()} is already exist.");
            }
            return ValidationResult.Success;
        }
    }
}
