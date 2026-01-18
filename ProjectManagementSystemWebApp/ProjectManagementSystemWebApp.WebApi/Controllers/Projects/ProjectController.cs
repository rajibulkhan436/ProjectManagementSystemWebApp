using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Core.NotificationHub;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.Features.Projects;
using ProjectManagementSystemWebApp.WebApi.Authorization.Attributes;
using ProjectManagementSystemWebApp.WebApi.Constants;
using static ProjectManagementSystem.Services.Features.Projects.DeleteProject;
using static ProjectManagementSystem.Services.Features.Projects.GetAllProjectStatus;
using static ProjectManagementSystem.Services.Features.Projects.GetProjectReport;
using static ProjectManagementSystem.Services.Features.Projects.GetProjectStatusById;
using static ProjectManagementSystem.Services.Features.Projects.UpdateProjectStatusById;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.Projects
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class ProjectController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProjectController> _logger;
        private readonly INotificationService _notificationService;

        public ProjectController(IMediator mediator, ILogger<ProjectController> logger,
            INotificationService notificationService)
        {
            _mediator = mediator;
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet(RouteKeys.Default)]
        public async Task<IActionResult> DisplayHomePage(CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(Ok("Welcome to Home screen"));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred while loading the page");
            }
        }

        [HttpGet(RouteKeys.DisplayProjects)]
        [CustomAuthorization("Manager")]
        public async Task<IActionResult> DisplayProjectStatusAsync(CancellationToken cancellationToken)
        {
            try
            {
                
                var projectList = await _mediator.Send(new GetAllProjectStatusQuery(), cancellationToken);
                await _notificationService.BroadcastNotification("The project list is successfully retrieved.");

                return Ok(projectList);
            }
            catch (Exception exception) 
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error in retrieving Project Status");
            }
                
        }

        [HttpGet(RouteKeys.DisplayProjectsById)]
        public async Task<IActionResult> DisplayProjectStatusByIdAsync([FromQuery]int projectId, CancellationToken cancellationToken)
        {
            try
            {
                var projectList = await _mediator.Send(new GetProjectStatusByIdQuery(projectId), cancellationToken);
                return Ok(projectList);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error in retrieving Project Status");
            }

        }


        [HttpPut(RouteKeys.UpdateProjectStatus)]
        [CustomAuthorization("Manager")]
        public async Task<IActionResult> UpdateProjectStatusAsync([FromBody] UpdateProjectStatusCommand command,
                                                             CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new { Message = result });
            }
            catch (Exception exception) 
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error while Updating Status");
            }
            
        }

        [HttpGet(RouteKeys.DisplayProjectReports)]
        public async Task<IActionResult> DisplayProjectReportAsync(CancellationToken cancellationToken)
        {
            try
            {
                var projectReportList = await _mediator.Send(new GetProjectReportQuery(), cancellationToken);
                return Ok(projectReportList);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error while retrieving project Reports.");
            }

        }

        [HttpDelete(RouteKeys.DeleteProject)]
        [CustomAuthorization("Manager")]
        public async Task<IActionResult> DeleteProjectAsync([FromQuery] int projectId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProjectCommand(projectId), cancellationToken);
                if(result) {
                    return Ok(new { Message = "Project Deleted Successfully" });
                }
                else
                {
                    return BadRequest("An error while Deleting Project by try");
                }
               
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error while Deleting Project");
            }

        }

    }
}
