using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using System.Security.Claims;
using ProjectManagementSystem.Core.Contracts;
using ProjectManagementSystem.Services.Contracts;

namespace ProjectManagementSystemWebApp.WebApi.Authentication
{  
    public class CustomAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        IRedisService redisService,
        ITokenService tokenService, 
        ILoggerFactory loggerFactory,
        UrlEncoder urlEncoder
    ) : AuthenticationHandler<AuthenticationSchemeOptions>(options, loggerFactory, urlEncoder)
    {
        private readonly IRedisService _redisService = redisService;
        private readonly ITokenService _tokenService = tokenService; 
        private readonly ILogger<CustomAuthenticationHandler> _logger = loggerFactory.CreateLogger<CustomAuthenticationHandler>();

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var token = Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
                token ??= Request.Query["token"].FirstOrDefault();
                _logger.LogInformation("Token received: {Token}", token);

                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogError("Unauthorized access attempt: No token provided.");
                    return AuthenticateResult.Fail("Unauthorized: No Token Provided");
                }

                
                if (!_tokenService.VerifyToken(token))
                {
                    _logger.LogError("Unauthorized: Invalid JWT Token.");
                    return AuthenticateResult.Fail("Unauthorized: Invalid Token");
                }

                
                var cachedToken = await _redisService.GetValueAsync(token, CancellationToken.None);
                if (cachedToken == null)
                {
                    _logger.LogError("Unauthorized: Token not found or expired in Redis.");
                    return AuthenticateResult.Fail("Unauthorized: Token Not Found or Expired");
                }

                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, cachedToken),
                new Claim(ClaimTypes.Authentication, token),
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                _logger.LogInformation("Authentication successful for user: {User}", cachedToken);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while authenticating the token.");
                return AuthenticateResult.Fail("Internal Server Error");
            }
        }
    }

}
