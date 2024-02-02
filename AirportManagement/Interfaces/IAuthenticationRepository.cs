using AirportManagement.Dtos;

namespace AirportManagement.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<LoginResponseDto> Login(string username, string password);

    }
}

