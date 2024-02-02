
using AirportManagement.Helper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AirportManagement.Models
{
    public enum UserRole
    {
        Admin,
        Employee,
    }
    [Index(nameof(RoleName), IsUnique = true)]
    public class SystemRole
    {
        [Key]
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
        [Required]
        [ValidateRoleName]
        public string RoleName { get; set; }
    }
}