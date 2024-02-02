using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportManagement.Models
{
    [Index(nameof(passengerName), IsUnique = true)]
    [Index(nameof(passengerEmail), IsUnique = true)]
    [Index(nameof(passengerPhone), IsUnique = true)]
    public class Passenger
    {
        [Key]
        public Guid passengerId { get; set; }
        [Required]
        public DateTime passengerDOB { get; set; }
        [Required]
        public string passengerName { get; set; }
        [Required]
        [EmailAddress]
        public string passengerEmail { get; set; }
        [Required]
        public string passengerPhone { get; set; }
        [Required]
        public string IdType { get; set; }
        public IIdentification Identification { get; set; }
        public void SetIdentification(string idType, string code)
        {
            if (idType.Equals("DriverLicense"))
            {
                Identification = new DriverLicense() { Code = code };
            }
            else if (idType.Equals("Passport"))
            {
                Identification = new Passport() { Code = code };
            }
            else
            {
                throw new ArgumentException("Invalid identification type: " + idType);
            }
        }
        [ForeignKey("Flight")]
        public Guid flightId { get; set; }
        public Flight flight { get; set; }
        public List<Luggage> luggages { get; set;}
    }

    public interface IIdentification
    {
        string Code { get; set; }
    }

    public class DriverLicense : IIdentification
    {
        [StringLength(8, MinimumLength = 8)]
        public string Code { get; set; }
    }

    public class Passport : IIdentification
    {
        [StringLength(30, MinimumLength = 3)]
        public string Code { get; set; }
    }
}
