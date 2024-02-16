using AirportManagement.Data;
using AirportManagement.Dtos;
using AirportManagement.Helper;
using AirportManagement.Interfaces;
using AirportManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirportManagement.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<City> GetCityById(Guid id)
        {
            var city = await _context.tblCity.Include(c => c.country).Include(c => c.airports).FirstOrDefaultAsync(c => c.cityId == id);
            return city;
        }

        public async Task<SearchResponseDto<City>> SearchCity(SearchCityQuery query)
        {
            var searchResult = _context.tblCity.Where(c => c.cityName.Contains(query.cityName ?? "")).AsQueryable();

            var sortErrorMsg = ValidateSortingAttribute.ValidationSortError<City>(query.sortField, query.sortDirection);

            if (sortErrorMsg.Length > 0)
                throw new Exception(sortErrorMsg);
           
            else
            {
                Expression<Func<City, object>> keySelector = query.sortField.ToLower() switch
                {
                    "cityname" => c => c.cityName,
                    _ => throw new NotImplementedException(),
                };

                searchResult = ValidateSortingAttribute.IsDescending(query.sortDirection)
                 ? searchResult.OrderByDescending(keySelector)
                 : searchResult.OrderBy(keySelector);

                return await SearchResponseDto<City>.CreateAsync(
                   searchResult,
                   query.pageNumber,
                   query.pageSize
                );
            }
        }

        public async Task<City> CreateCity(string  cityName, Guid countryId)
        {
            // Check if the city exists before adding
            if (_context.tblCity.Where(c => c.cityName == cityName).Any())
            {
                throw new Exception("City already exists");
            }

            var country = _context.tblCountry.Find(countryId);
            if (country == null)
            {
                throw new Exception("Country does not exist");
            }

            var newCity = new City
            {
                cityName = cityName,
                countryId = countryId,
                country = country
            };
            _context.Add(newCity);
            await _context.SaveChangesAsync();

            return newCity;
        }

        public async Task<SearchResponseDto<City>> SearchCityByCountry(SearchCityByCountryQuery query)
        {
            var searchResult = _context.tblCity.Where(c => c.countryId == query.countryId).AsQueryable();

            var sortErrorMsg = ValidateSortingAttribute.ValidationSortError<City>(query.sortField, query.sortDirection);

            if (sortErrorMsg.Length > 0)
                throw new Exception(sortErrorMsg);
            else
            {
                Expression<Func<City, object>> keySelector = query.sortField.ToLower() switch
                {
                    "cityname" => c => c.cityName,
                    _ => throw new NotImplementedException(),
                };

                searchResult = ValidateSortingAttribute.IsDescending(query.sortDirection)
                 ? searchResult.OrderByDescending(keySelector)
                 : searchResult.OrderBy(keySelector);

                return await SearchResponseDto<City>.CreateAsync(
                   searchResult,
                   query.pageNumber,
                   query.pageSize
                );
            }
        }
    }   
} 
