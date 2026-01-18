using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Services.DTOs.UserDTOs;
using ProjectManagementSystemWebApp.WebApi.Constants;
using static ProjectManagementSystem.Services.Features.Authentication.LoginUser;
using static ProjectManagementSystem.Services.Features.Authentication.RegisterUser;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.Authentication
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class AuthenticationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(RouteKeys.Default)]
        public async Task<IActionResult> DisplayHomePage(CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(Ok("Welcome to Home Authentication Screen"));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred while loading the page");
            }
        }


        [HttpPost(RouteKeys.Register)]
        public async Task<IActionResult> Register([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new RegisterUserCommand(userDto), cancellationToken);
                return Ok(new { message = "User Registered Successfully" });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred while Registration Process");
            }

        }

        [HttpPost(RouteKeys.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto login, CancellationToken cancellationToken)
        {
            try
            {
                var loginResult = await _mediator.Send(new LoginUserQuery(login), cancellationToken);
                return Ok(loginResult);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred while login process.");
            }
        }
    }
}
