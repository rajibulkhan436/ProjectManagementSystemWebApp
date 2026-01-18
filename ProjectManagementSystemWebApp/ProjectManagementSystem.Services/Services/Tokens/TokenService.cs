using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjectManagementSystem.Services.Contracts;
using Microsoft.Extensions.Configuration;
using ProjectManagementSystem.Services.DTOs.UserDTOs;

namespace ProjectManagementSystem.Services.Services.Tokens
{    
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;  
        }

        public string GenerateToken(UserLoginDto user)
        {
            var mySecretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(mySecretKey))
            {
                throw new Exception("The Jwt Secret Key is not configured Properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var tokenExpiration = DateTime.UtcNow.AddMinutes(30);

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: tokenExpiration,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);            
        }

        public bool VerifyToken(string token)
        {
            var mySecretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(mySecretKey))
            {
                throw new Exception("The Jwt Secret Key is not configured Properly.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(mySecretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters,
                                                    out SecurityToken validatedToken);
            return claimsPrincipal != null;
        }

    }
}
