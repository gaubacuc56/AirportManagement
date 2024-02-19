using System.ComponentModel.DataAnnotations;

namespace AirportManagement.Dtos
{
    public class CityDto
    {
        public Guid cityId { get; set; }
        public string cityName { get; set; }
    }
    public class SearchCityQuery : SearchQueryDto
    {
        public string? cityName { get; set; }
        public SearchCityQuery()
        {
            // Initialize sortField with a default value
            sortField = "cityName";
        }
    }
    public class SearchCityByCountryQuery : SearchQueryDto
    {
        [Required(ErrorMessage = "The countryId field is required.")]
        public Guid countryId { get; set; }
        public SearchCityByCountryQuery()
        {
            // Initialize sortField with a default value
            sortField = "cityName";
        }
    }
    public class UpdateCityDto
    {
        public string cityName { get; set; }
    }
}
