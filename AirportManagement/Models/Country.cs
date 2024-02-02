using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    [Index(nameof(countryName), IsUnique = true)]
    public class Country
    {
        [Key]
        public Guid countryId { get; set; }
        [Required]
        public string countryName { get; set; }
        //navigation property
        public virtual List<City> cities { get; set; } = new List<City>();
    }
}
