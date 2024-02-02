using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    [Index(nameof(cityName), IsUnique = true)]
    public class City
    {
        [Key]
        public Guid cityId { get; set; }
        [Required]
        public string cityName { get; set; }
        [ForeignKey("Country")]
        public Guid countryId { get; set; }
        public virtual Country country { get; set; }
        //navigation property
        public virtual List<Airport> airports { get; set; }
    }
}
