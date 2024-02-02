using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    [Index(nameof(airportName), IsUnique = true)]
    public class Airport
    {
        [Key]
        public Guid airportId { get; set; }
        [Required]
        public string airportName { get; set; }
        [ForeignKey("City")]
        public Guid cityId { get; set; }
        //navigation properties
        public City city { get; set; }
    }
}
