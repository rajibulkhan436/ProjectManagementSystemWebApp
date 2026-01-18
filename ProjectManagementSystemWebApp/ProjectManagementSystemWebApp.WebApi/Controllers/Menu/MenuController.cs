using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystemWebApp.WebApi.Authorization.Attributes;
using ProjectManagementSystemWebApp.WebApi.Constants;
using static ProjectManagementSystem.Services.Features.Menu.GetAllFeatureMenu;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.Menu
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class MenuController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMediator mediator, ILogger<MenuController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(RouteKeys.DisplayFeaturesMenu)]
        public async Task<IActionResult> GetFeatureMenuAsync(CancellationToken cancellationToken)
        {
            try
            {
                var featuresMenu = await _mediator.Send(new GetAllFeatureMenuQuery(), cancellationToken);
                return Ok(featuresMenu);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error in retrieving features Menu");
            }

        }

    }
}
