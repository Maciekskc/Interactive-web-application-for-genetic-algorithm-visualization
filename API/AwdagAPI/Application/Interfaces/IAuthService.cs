using Application.Dtos.Auth.Requests;
using Application.Dtos.Auth.Responses;
using System.Threading.Tasks;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest request);

        Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterRequest request);

        Task<ServiceResponse<RefreshTokenResponse>> RefreshTokenAsync(string accessToken, string refreshToken);
    }
}