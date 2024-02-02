using AirportManagement.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;

namespace AirportManagement.Helper
{
    public enum EnumSortDirection
    {
        asc,
        desc
    }
   
    public class ValidateSortingAttribute
    {
            private const string invalidDirectionMsg = "Invalid sort direction";
            private const string invalidFieldMsg = "Invalid sort field";
            private const string requiredFieldMsg = "Please enter all required field";
        private static bool IsDirectionExists(string direction)
        {
            var roleEnumNames = Enum.GetNames(typeof(EnumSortDirection));
            if (direction != null && !roleEnumNames.Contains(direction.ToString(), StringComparer.OrdinalIgnoreCase))
                return false;
            return true;
        }
        private static bool IsFieldExists<T>(string fieldName)
        {
            return typeof(T).GetProperty(fieldName) != null;
        }
        private static bool IsAllFieldFilled(string? fieldName, string? direction)
        {
            if ((fieldName == null && direction != null) || (fieldName != null && direction == null))
                return false;
            else return true;
        }
        public static string ValidationSortError<T>(string? fieldName, string? direction)
        {
            // Both sort value must be filled or empty
            if (IsAllFieldFilled(fieldName, direction) == false)
                return requiredFieldMsg;
            // Check if sort direction is "desc" or "asc"
            else if (direction != null && !IsDirectionExists(direction))
                return invalidDirectionMsg;
            // Check if provided class contains provided fieldName
            else if (fieldName != null && !IsFieldExists<T>(fieldName))
                return invalidFieldMsg;
            else return "";
        }
        public static object GetPropertyValue(object obj, string propertyName)
        {
            dynamic dynamicObj = new ExpandoObject();
            var propertyValue = obj.GetType().GetProperty(propertyName).GetValue(obj);
            dynamicObj.Property = propertyValue;
            return dynamicObj.Property;
        }

        public static bool IsDescending(string direction)
        {
            if (Enum.TryParse(direction, out EnumSortDirection parsedEnum) && parsedEnum == EnumSortDirection.desc)
                return true;
            else return false;
        }
    }
}
