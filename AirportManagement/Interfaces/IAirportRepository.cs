using AirportManagement.Dtos;
using AirportManagement.Models;

namespace AirportManagement.Interfaces
{
    public interface IAirportRepository
    {
        Task<Airport> GetAirportById(Guid id);
        Task<SearchResponseDto<Airport>> SearchAirport(SearchAirportQuery query);
        Task<Airport> CreateAirport(string cityName, Guid countryId);
        Task<SearchResponseDto<Airport>> SearchAirportByCountry(SearchAirportByCountryQuery query);

    }
}
