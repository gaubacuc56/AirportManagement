using System.ComponentModel.DataAnnotations;

namespace AirportManagement.Dtos
{
    public class CountryDto
    {
        public Guid countryId { get; set; }
        public string countryName { get; set; }
    }
    public class UpdateCountryDto
    {
        public string countryName { get; set; }
    }
    public class SearchCountryQuery : SearchQueryDto
    {
        public string? countryName { get; set; }
        public SearchCountryQuery()
        {
            // Initialize sortField with a default value
            sortField = "countryName";
        }

    }
}
