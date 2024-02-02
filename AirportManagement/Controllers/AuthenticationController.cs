using AirportManagement.Dtos;
using AirportManagement.Interfaces;
using AirportManagement.Repository;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AirportManagement.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationController (IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto data)
        {
            try
            {
                var token = await _authenticationRepository.Login(data.username, data.password);
                return CustomResult("Successfully", token);
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
