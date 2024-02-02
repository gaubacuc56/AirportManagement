using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AirportManagement.Models
{
    [Index(nameof(aircraftName), IsUnique = true)]
    public class Aircraft
    {
        [Key]
        public Guid aircraftId { get; set; }
        [Required]
        public string aircraftName { get; set;}
        [Required]
        public int aircraftCapacity { get; set;}
    }
}
