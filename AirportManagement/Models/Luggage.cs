using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    public class Luggage
    {
        [Key]
        public Guid luggageId { get; set; }
        [Required]
        public int luggageWeight { get; set; }
        [ForeignKey("Passenger")]
        public Guid passengerId { get; set; }
        [ForeignKey("Flight")]
        public Guid flightId { get; set; }
        //navigation properties
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Passenger passenger { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Flight flight { get; set; }

    }
}
