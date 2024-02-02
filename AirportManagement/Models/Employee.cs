using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    [Index(nameof(employeeAccount), IsUnique = true)]
    [Index(nameof(employeeEmail), IsUnique = true)]
    [Index(nameof(employeePhone), IsUnique = true)]
    [Index(nameof(employeeAccount), IsUnique = true)]
    public class Employee
    {
        [Key]
        public Guid employeeId { get; set; }
        [Required]
        public string employeeName { get; set; }
        [Required]
        [EmailAddress]
        public string employeeEmail { get; set; }
        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string employeePhone { get; set;}
        public string employeeAccount { get; set; }
        [Required]
        [StringLength(Int32.MaxValue, MinimumLength = 6, ErrorMessage = "Password must be at least 20 characters")]
        public string employeePassword { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Invalid user role")]
        [EnumDataType(typeof(UserRole))]
        [ForeignKey("SystemRole")]
        public UserRole empRole { get; set; }
        [ForeignKey("Airport")]
        public Guid airportId { get; set; }
        //navigation properties
        public Airport airport { get; set; }
    }
}