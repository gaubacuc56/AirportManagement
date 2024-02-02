namespace AirportManagement.Dtos
{
    public class AirportDto
    {
        public Guid airportId { get; set; }
        public string airportName { get; set; }
    }
    public class SearchAirportQuery : SearchQueryDto
    {
        public string? airportName { get; set; }
        public SearchAirportQuery()
        {
            // Initialize sortField with a default value
            sortField = "airportName";
        }

    }
}
