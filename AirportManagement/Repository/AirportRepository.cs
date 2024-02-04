using AirportManagement.Data;
using AirportManagement.Dtos;
using AirportManagement.Helper;
using AirportManagement.Interfaces;
using AirportManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirportManagement.Repository
{
    public class AirportRepository : IAirportRepository
    {
        private readonly DataContext _context;
        public AirportRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Airport> GetAirportById(Guid id)
        {
            var airpoirt = _context.tblAirport.Where(c => c.airportId == id).FirstOrDefault();
            return airpoirt;
        }
        public async Task<SearchResponseDto<Airport>> SearchAirport(SearchAirportQuery query)
        {
            var searchResult = _context.tblAirport.Where(c => c.airportName.Contains(query.airportName ?? "")).AsQueryable();

            var sortErrorMsg = ValidateSortingAttribute.ValidationSortError<Airport>(query.sortField, query.sortDirection);

            if (sortErrorMsg.Length > 0)
                throw new Exception(sortErrorMsg);
            else
            {
                Expression<Func<Airport, object>> keySelector = query.sortField.ToLower() switch
                {
                    "airportname" => c => c.airportName,
                    _ => throw new NotImplementedException(),
                };

                searchResult = ValidateSortingAttribute.IsDescending(query.sortDirection)
                 ? searchResult.OrderByDescending(keySelector)
                 : searchResult.OrderBy(keySelector);

                return await SearchResponseDto<Airport>.CreateAsync(
                   searchResult,
                   query.pageNumber,
                   query.pageSize
                );
            }
        }
        public async Task<Airport> CreateAirport(string airportName, Guid cityId)
        {
            var city = _context.tblCity.Find(cityId);
            if (city == null)
            {
                throw new Exception("City does not exist");
            }
         
            var newAirport = new Airport
            {
                airportName = airportName,
                cityId = cityId,
                city = city
            };
            _context.Add(newAirport);
            _context.SaveChangesAsync();

            return newAirport;
        }
        public async Task<SearchResponseDto<Airport>> SearchAirportByCountry(SearchAirportByCountryQuery query)
        {

            var searchResult = _context.tblCountry.Where(c => c.countryId == query.countryId)
                                                  .SelectMany(c => c.cities)
                                                  .SelectMany(city => city.airports)
                                                  .AsQueryable();

            var sortErrorMsg = ValidateSortingAttribute.ValidationSortError<Airport>(query.sortField, query.sortDirection);

            if (sortErrorMsg.Length > 0)
                throw new Exception(sortErrorMsg);
            else
            {
                Expression<Func<Airport, object>> keySelector = query.sortField.ToLower() switch
                {
                    "airportname" => c => c.airportName,
                    _ => throw new NotImplementedException(),
                };

                searchResult = ValidateSortingAttribute.IsDescending(query.sortDirection)
                 ? searchResult.OrderByDescending(keySelector)
                 : searchResult.OrderBy(keySelector);

                return await SearchResponseDto<Airport>.CreateAsync(
                   searchResult,
                   query.pageNumber,
                   query.pageSize
                );
            }
        }
    }
}
