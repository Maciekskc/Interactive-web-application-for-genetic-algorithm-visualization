namespace Application.Dtos.Auth.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}