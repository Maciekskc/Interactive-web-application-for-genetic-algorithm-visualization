namespace Application.Dtos.Auth.Responses
{
    public class RegisterResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}