using AirportManagement.Dtos;
using AirportManagement.Models;

namespace AirportManagement.Interfaces
{
    public interface IAirportRepository
    {
        Task<Airport> GetAirportById(Guid id);
        Task<SearchResponseDto<Airport>> SearchAirport(SearchAirportQuery query);
        Task<Airport> CreateAirport(Guid cityId, string airportName, string airportCode);
        Task<SearchResponseDto<Airport>> SearchAirportByCountry(SearchAirportByCountryQuery query);

    }
}
