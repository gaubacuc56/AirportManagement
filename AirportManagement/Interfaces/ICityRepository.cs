using AirportManagement.Dtos;
using AirportManagement.Models;

namespace AirportManagement.Interfaces
{
    public interface ICityRepository
    {
        Task<City> GetCityById(Guid id);
        Task<SearchResponseDto<City>> SearchCity(SearchCityQuery search);
        Task<City> CreateCity(string cityName, Guid countryId);
        Task<SearchResponseDto<City>> SearchCityByCountry(SearchCityByCountryQuery search);
    }
}
