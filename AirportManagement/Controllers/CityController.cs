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
    public class CityController : BaseController
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpGet("search"), Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CreateCity(string cityName, string cityId)
        {
            try
            {
                var cityIdGuid = new Guid(cityId);
                await _cityRepository.CreateCity(cityName, cityIdGuid);
                return CustomResult("Successfully", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                if (ex != null)
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [AllowAnonymous]
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
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCity(Guid id, [FromBody] Dtos.UpdateCityDto city)
        {
            try
            {
                await _cityRepository.UpdateCity(id, city);
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
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            try
            {
                await _cityRepository.DeleteCity(id);
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
