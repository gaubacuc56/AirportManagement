using AirportManagement.Dtos;
using AirportManagement.Interfaces;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AirportManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController: BaseController
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(Guid id)
        {
            try
            {
                var country =  await _countryRepository.GetCountryById(id);
                return CustomResult(_mapper.Map<CountryDto>(country));
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchCountry([FromQuery] SearchCountryQuery query)
        {
            try
            {
                var country = await _countryRepository.SearchCountry(query);
                return CustomResult(_mapper.Map<SearchResponseDto<CountryDto>>(country));
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpPost("create"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCountry(string countryName)
        {
            try
            {
                await _countryRepository.CreateCountry(countryName);
                return CustomResult("Successfully", HttpStatusCode.Created);
            }
            catch (Exception ex)                                  
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] Dtos.UpdateCountryDto country)
        {
            try
            {
                await _countryRepository.UpdateCountry(id, country);
                return CustomResult("Successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            try
            {
                await _countryRepository.DeleteCountry(id);
                return CustomResult("Successfully", HttpStatusCode.OK);
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
