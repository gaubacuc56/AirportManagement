using AirportManagement.Data;
using AirportManagement.Interfaces;
using AirportManagement.Models;

namespace AirportManagement.Repository
{
    public class AircraftRepository : IAircraftRepository
    {
        private readonly DataContext _context;
        public AircraftRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Aircraft> SearchAircraft(string? search, string? sortField, string? sortDirection)
        {
            var searchResult = _context.tblAircraft.AsQueryable();
            return searchResult.ToList();
        }
    }
}
