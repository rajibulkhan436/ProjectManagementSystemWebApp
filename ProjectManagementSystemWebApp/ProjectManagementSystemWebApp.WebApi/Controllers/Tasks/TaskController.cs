using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystemWebApp.WebApi.Authorization.Attributes;
using ProjectManagementSystemWebApp.WebApi.Constants;
using static ProjectManagementSystem.Services.Features.Projects.DeleteProject;
using static ProjectManagementSystem.Services.Features.Projects.GetAllTaskStatus;
using static ProjectManagementSystem.Services.Features.Projects.GetTaskStatusById;
using static ProjectManagementSystem.Services.Features.Projects.UpdateTaskStatusById;
using static ProjectManagementSystem.Services.Features.Tasks.AddTask;
using static ProjectManagementSystem.Services.Features.Tasks.AssignTaskToMember;
using static ProjectManagementSystem.Services.Features.Tasks.DeleteTask;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.Tasks
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class TaskController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskController> _logger;

        public TaskController(IMediator mediator, ILogger<TaskController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(RouteKeys.Default)]
        public async Task<IActionResult> DisplayHomePageAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await Task.FromResult(Ok("Welcome to Screen"));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred while loading the page");
            }
        }

        [HttpGet(RouteKeys.DisplayTasks)]
        public async Task<IActionResult> DisplayTaskStatusAsync(CancellationToken cancellationToken)
        {
            try
            {
                var taskList = await _mediator.Send(new GetAllTaskStatusQuery(), cancellationToken);
                return Ok(taskList);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred in retrieving Task Status.");
            }
            
        }

        [HttpGet(RouteKeys.DisplayTasksById)]
        public async Task<IActionResult> DisplayTaskStatusByIdAsync([FromQuery]int taskId, CancellationToken cancellationToken)
        {
            try
            {
                var taskList = await _mediator.Send(new GetTaskStatusByIdQuery(taskId), cancellationToken);
                return Ok(taskList);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred in retrieving Task Status.");
            }

        }


        [HttpPost(RouteKeys.AddTask)]
        public async Task<IActionResult> AddTaskASync([FromBody]AddTaskCommand command, CancellationToken cancellationToken)
        {
            try{
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new { Message = result });
            }catch(Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error occurred in Adding New Task");
            }


        }

        [HttpPut(RouteKeys.UpdateTaskStatus)]
        public async Task<IActionResult> UpdateTaskStatusAsync([FromBody] UpdateTaskStatusCommand command,
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
                return BadRequest("An error occurred in Updating Task Status.");
            }

        }

        [HttpPost(RouteKeys.AssignTask)]
        public async Task<IActionResult> AssignTaskAsync([FromBody] AssignTaskToMemberCommand command,
                                                             CancellationToken cancellationToken)
        {
            try
            {
                if (command == null)
                {
                    return BadRequest("Invalid task assignment data.");
                }

                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new { Message = result });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occurred while assigning the task.");
                return StatusCode(500, "An error occurred while assigning the task. Please try again later.");
            }
        }


        [HttpDelete(RouteKeys.DeleteTask)]
        [CustomAuthorization("Manager")]
        public async Task<IActionResult> DeleteTaskAsync([FromQuery] int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new DeleteTaskCommand(id), cancellationToken);
                if (result)
                {
                    return Ok(new { Message = "Task Deleted Successfully" });
                }
                else
                {
                    return BadRequest("An error while Deleting Task by try");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error while Deleting Task");
            }

        }
    }
}
