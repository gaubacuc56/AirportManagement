namespace AirportManagement.Dtos
{
    public class AuthenticationDto
    {
    }
    public class LoginResponseDto
    {
        public string token { get; set; }
    }
    public class LoginRequestDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
