using AirportManagement.Dtos;
using AirportManagement.Models;

namespace AirportManagement.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetCountryById(Guid id);
        Task<SearchResponseDto<Country>> SearchCountry(SearchCountryQuery search);
        Task<Country> CreateCountry(string countryName);
        Task<Country> UpdateCountry(Guid id, UpdateCountryDto country);
        Task<Country> DeleteCountry(Guid id);
    }
}