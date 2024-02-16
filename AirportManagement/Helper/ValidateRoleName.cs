using AirportManagement.Models;
using System.ComponentModel.DataAnnotations;
namespace AirportManagement.Helper
{
    public class ValidateRoleNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var roleEnumNames = Enum.GetNames(typeof(UserRole));

            if (value != null && !roleEnumNames.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult($"Invalid {validationContext.DisplayName} value.");
            }

            return ValidationResult.Success;
        }
    }
}
