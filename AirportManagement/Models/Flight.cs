using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    [Index(nameof(flightCode), IsUnique = true)]
    public class Flight
    {
        [Key]
        public Guid flightId { get; set; }
        [Required]
        public string flightCode { get; set; }
        [ForeignKey("City")]
        public Guid cityId { get; set; }
        public City flightDestination { get; set; }
        [Required]
        public int price { get; set; }
        public DateTime takeoffTime { get; set; }
        public DateTime reachingTime { get; set; }
        [ForeignKey("Aircraft")]
        public Guid aircraftId { get; set; }
        public Aircraft aircraft { get; set; }
        [ForeignKey("Runway")]
        public Guid runwayId { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Runway runway { get; set; }
        public List<Passenger> passengers { get; set; }
        public List<Luggage> luggages { get; set; }
    }
}
