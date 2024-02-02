using AirportManagement.Dtos;
using AirportManagement.Models;
using AutoMapper;

namespace AirportManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Country, CountryDto>();
            CreateMap<Country, UpdateCountryDto>();
            CreateMap<City, CityDto>();
            CreateMap<Airport, AirportDto>();

            CreateMap<SearchResponseDto<Country>, SearchResponseDto<CountryDto>>();
            CreateMap<SearchResponseDto<City>, SearchResponseDto<CityDto>>();
            CreateMap<SearchResponseDto<Airport>, SearchResponseDto<AirportDto>>();
        }
    }
}
