using AirportManagement.Dtos;
using AirportManagement.Interfaces;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AirportManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : BaseController
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public AirportController(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAirportById(Guid id)
        {
            try
            {
                var airport = await _airportRepository.GetAirportById(id);
                return Ok(airport);
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAirport([FromQuery] SearchAirportQuery query)
        {
            try
            {
                var airport = await _airportRepository.SearchAirport(query);
                return CustomResult(_mapper.Map<SearchResponseDto<AirportDto>>(airport));
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAirport(string cityId, string airportName, string airportCode)
        {
            try
            {
                var cityIdGuid = new Guid(cityId);
                var airport = await _airportRepository.CreateAirport(cityIdGuid, airportName, airportCode);
                return CustomResult("Successfully", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("searchByCountry")]
        public async Task<IActionResult> SearchAiportByCountry([FromQuery] SearchAirportByCountryQuery query)
        {
            try
            {
                var city = await _airportRepository.SearchAirportByCountry(query);
                return CustomResult(_mapper.Map<SearchResponseDto<AirportDto>>(city));
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
