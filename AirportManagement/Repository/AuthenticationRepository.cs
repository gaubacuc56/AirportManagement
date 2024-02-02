using AirportManagement.Data;
using AirportManagement.Dtos;
using AirportManagement.Interfaces;
using AirportManagement.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AirportManagement.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        private IConfiguration _configuration;
        public AuthenticationRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
        }
        
        private string GenerateToken (Employee emp)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, emp.employeeName),
                new Claim(ClaimTypes.Role, emp.empRole == UserRole.Admin ? "Admin" : "Employee")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<LoginResponseDto> Login(string username, string password)
        {
            var user = _context.tblEmployee
                .Where(c => c.employeeAccount == username  && c.employeePassword == password)
                .FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var loginResponse = new LoginResponseDto
            {
                token = GenerateToken(user)
            }; 
            return loginResponse;
        }
    }
}
