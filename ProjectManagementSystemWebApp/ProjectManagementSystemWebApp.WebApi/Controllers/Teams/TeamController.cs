using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystemWebApp.WebApi.Constants;
using static ProjectManagementSystem.Services.Features.TeamMember.GetAllTaskTeamMember;
using static ProjectManagementSystem.Services.Features.TeamMember.GetTaskTeamByTaskId;
using static ProjectManagementSystem.Services.Features.TeamMember.GetAllTeamMembers;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.Teams
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class TeamController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TeamController> _logger;

        public TeamController(IMediator mediator, ILogger<TeamController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(RouteKeys.Default)]
        public async Task<IActionResult> DisplayHomePageAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(Ok("Welcome to screen"));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred while loading the page");
            }
        }

        [HttpGet(RouteKeys.DisplayTeamMembers)]
        public async Task<IActionResult> DisplayTeamMembersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var teamList = await _mediator.Send(new GetAllTeamMembersQuery(), cancellationToken);
                return Ok(teamList);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An Error occurred while retrieving the members.");
            }
        }

        [HttpGet(RouteKeys.DisplayTaskTeam)]
        public async Task<IActionResult> DisplayTaskTeamAsync(CancellationToken cancellationToken)
        {
            try
            {
                var taskTeamList = await _mediator.Send(new GetAllTaskTeamQuery(), cancellationToken);
                return Ok(taskTeamList);
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An Error occurred while retrieving the members for Task.");
            }
        }

        [HttpGet(RouteKeys.DisplayTaskTeamById)]
        public async Task<IActionResult> DisplayTaskTeamByIdAsync([FromQuery]int taskId, CancellationToken cancellationToken)
        {
            try
            {
                var taskTeamList = await _mediator.Send(new GetTaskTeamByIdQuery(taskId), cancellationToken);
                return Ok(taskTeamList);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An Error occurred while retrieving the members for Task.");
            }
        }

    }
}
