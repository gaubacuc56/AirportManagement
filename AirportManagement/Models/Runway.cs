using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    public class Runway
    {
        [Key]
        public Guid runwayId {  get; set; }
        [Required]
        public string runwayCode { get; set; }
        public int runwayLength { get; set; }
        public bool isRunning { get; set; }
        [ForeignKey("Airport")]
        public Guid airportId { get; set; }
        //navigation properties
        public Airport airport { get; set; }
    }
}