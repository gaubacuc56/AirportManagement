namespace AirportManagement.Helper
{
    public class ValidatePagination
    {
        public static string IsValueValid(int? pageSize, int? pageNumber)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return "Invalid page number or page size. Both should be greater than 0";
            else return "";
        }
    }
}
