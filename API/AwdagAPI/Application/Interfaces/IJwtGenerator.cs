using Application.Security;
using Domain.Models;
using Domain.Models.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        Task<JwtToken> CreateTokenAsync(ApplicationUser user);

        ClaimsPrincipal GetPrincipalFromToken(string token);

        string GetUserIdFromToken(ClaimsPrincipal validatedToken);

        public List<string> ValidateRefreshToken(RefreshToken refreshToken, ClaimsPrincipal validatedToken);
    }
}