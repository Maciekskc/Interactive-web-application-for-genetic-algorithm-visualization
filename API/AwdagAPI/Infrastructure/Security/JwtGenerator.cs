using Application.Interfaces;
using Application.Security;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public JwtGenerator(IConfiguration config, DataContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<JwtToken> CreateTokenAsync(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role == null) continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;
                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:TokenExpiry"])),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:RefreshTokenExpiry"])),
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new JwtToken
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal =
                    tokenHandler.ValidateToken(token, TokenValidationParametersDefaults.GetDefaultParameters(),
                        out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<string> ValidateRefreshToken(RefreshToken refreshToken, ClaimsPrincipal validatedToken)
        {
            var errors = new List<string>();

            if (refreshToken == null)
            {
                errors.Add("Ten refresh token nie istnieje");
                return errors;
            }

            if (DateTime.UtcNow > refreshToken.ExpiryDate)
            {
                errors.Add("Ten refresh token wygasł");
            }

            if (refreshToken.Invalidated)
            {
                errors.Add("Ten refresh token jest nieważny");
            }

            if (refreshToken.Used)
            {
                errors.Add("Ten refresh token został już użyty");
            }

            var jti = GetJtiFromToken(validatedToken);

            if (refreshToken.JwtId != jti)
            {
                errors.Add("Ten refresh token nie pasuje do podanego JWT");
            }

            return errors;
        }

        public string GetUserIdFromToken(ClaimsPrincipal validatedToken)
        {
            return validatedToken.Claims.Single(x => x.Type == "userId").Value;
        }

        private string GetJtiFromToken(ClaimsPrincipal validatedToken)
        {
            return validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        }
    }
}