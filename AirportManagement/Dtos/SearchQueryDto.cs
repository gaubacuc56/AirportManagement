using AirportManagement.Helper;
using System.ComponentModel.DataAnnotations;

namespace AirportManagement.Dtos
{
    public class SearchQueryDto
    {
        public string? sortField { get; set; }
        public string? sortDirection { get; set; } = "asc";
        [Required(ErrorMessage = "Page size is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0.")]
        public int pageSize { get; set; }
        [Required(ErrorMessage = "Page number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0.")]
        public int pageNumber { get; set; }
    }
}
