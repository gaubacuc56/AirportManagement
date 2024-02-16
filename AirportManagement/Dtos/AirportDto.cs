using System.ComponentModel.DataAnnotations;

namespace AirportManagement.Dtos
{
    public class AirportDto
    {
        public Guid airportId { get; set; }
        public string airportName { get; set; }
        public string airportCode { get; set; }
    }
    public class SearchAirportQuery : SearchQueryDto
    {
        public string? airportName { get; set; }
        public SearchAirportQuery()
        {
            // Initialize sortField with a default value
            sortField = "airportName";
        }
    }
    public class SearchAirportByCountryQuery : SearchQueryDto
    {
        [Required(ErrorMessage = "The countryId field is required.")]
        public Guid countryId { get; set; }
        public SearchAirportByCountryQuery()
        {
            // Initialize sortField with a default value
            sortField = "airportName";
        }
    }
}