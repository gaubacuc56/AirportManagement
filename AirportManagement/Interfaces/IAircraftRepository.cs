using AirportManagement.Models;

namespace AirportManagement.Interfaces
{
    public interface IAircraftRepository
    {
        ICollection<Aircraft> SearchAircraft(string? search, string? sortField, string? sortDirection);
    }
}
