using AirportManagement.Data;
using AirportManagement.Dtos;
using AirportManagement.Helper;
using AirportManagement.Interfaces;
using AirportManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirportManagement.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
 
        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Country> GetCountryById(Guid id)
        {
            var country = await _context.tblCountry.FirstOrDefaultAsync(c => c.countryId == id);
            if (country == null)
            {
                throw new Exception("Country not found");
            }
            return country;
        }
        public async Task<SearchResponseDto<Country>> SearchCountry(SearchCountryQuery query)
        {
            var searchResult = _context.tblCountry.Where(c => c.countryName.Contains(query.countryName ?? "")).AsQueryable();

            var sortErrorMsg = ValidateSortingAttribute.ValidationSortError<Country>(query.sortField, query.sortDirection);

            if (sortErrorMsg.Length > 0)
                throw new Exception(sortErrorMsg);

            else
            {
                Expression<Func<Country, object>> keySelector = query.sortField.ToLower() switch
                {
                    "countryname" => c => c.countryName,
                    _ => throw new NotImplementedException(),
                }; 

                searchResult = ValidateSortingAttribute.IsDescending(query.sortDirection)
                 ? searchResult.OrderByDescending(keySelector)
                 : searchResult.OrderBy(keySelector);

                return await SearchResponseDto<Country>.CreateAsync(
                   searchResult,
                   query.pageNumber,
                   query.pageSize
                );
            }
        }
        public async Task<Country> CreateCountry(string countryName)
        {
            // Check if the city exists before adding
            if (_context.tblCountry.Where(c => c.countryName == countryName).Any())
            {
                throw new Exception("Country already exists");
            }
            var newCountry = new Country
            {
                countryName = countryName
            };
            _context.Add(newCountry);
            await _context.SaveChangesAsync();

            return newCountry;
        }
        public async Task<Country> UpdateCountry(Guid id, UpdateCountryDto country)
        {
            // Check if the country is existed before adding
            if (_context.tblCountry.Where(c => c.countryName == country.countryName).Any())
            {
                throw new Exception("Country already exists");
            }

            var existingCountry = _context.tblCountry.Where(c => c.countryId == id).FirstOrDefault();

            if (existingCountry == null)
            {
                throw new Exception("Country not found");
            }
            existingCountry.countryName = country.countryName;
            await _context.SaveChangesAsync();
            return existingCountry;
        }
        public async Task<Country> DeleteCountry(Guid id)
        {
            var existingCountry = _context.tblCountry.Where(c => c.countryId == id).FirstOrDefault();

            if (existingCountry == null)
            {
                throw new Exception("Country not found");
            }
            _context.Remove(existingCountry);
            await _context.SaveChangesAsync();
            return existingCountry;
        }
    }
}