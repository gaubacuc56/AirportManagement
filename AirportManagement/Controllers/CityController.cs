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
    public class CityController : BaseController
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            try
            {
                var city = await _cityRepository.GetCityById(id);
                return CustomResult(_mapper.Map<CityDto>(city));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCity([FromQuery] SearchCityQuery query)
        {
            try
            {
                var city = await _cityRepository.SearchCity(query);
                return CustomResult(_mapper.Map<SearchResponseDto<CityDto>>(city));
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCity(string cityName, string countryId)
        {
            try
            {
                var countryIdGuid = new Guid(countryId);
                await _cityRepository.CreateCity(cityName, countryIdGuid);
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
        public async Task<IActionResult> SearchCityByCountry([FromQuery] SearchCityByCountryQuery query)
        {
            try
            {
                var city = await _cityRepository.SearchCityByCountry(query);
                return CustomResult(_mapper.Map<SearchResponseDto<CityDto>>(city));
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
