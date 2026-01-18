using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystemWebApp.WebApi.Constants;
using static ProjectManagementSystem.Services.Features.TeamMember.GetAllTaskTeamMember;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.Background
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class BackgroundController : BaseController
    {
        private readonly IBackgroundTaskQueue _queue;
        private readonly ILogger<BackgroundController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BackgroundController(IBackgroundTaskQueue queue, ILogger<BackgroundController> logger, IServiceProvider serviceProvider)
        {
            _queue = queue;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        [HttpGet(RouteKeys.EnqueueTask)]
        public  IActionResult EnqueueTask(CancellationToken cancellationToken)
        {
            var scope = _serviceProvider.CreateScope();
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            _queue.QueueBackgroundWorkItem(async token =>
            {
                _logger.LogInformation("Processing background task...");
                var taskteam = await mediator.Send(new GetAllTaskTeamQuery(), cancellationToken);
                await Task.Delay(15000, token);
                _logger.LogInformation("All Team members with task assigned retrieved..");
            });

            return Ok("Task enqueued.");
        }
    }
}
