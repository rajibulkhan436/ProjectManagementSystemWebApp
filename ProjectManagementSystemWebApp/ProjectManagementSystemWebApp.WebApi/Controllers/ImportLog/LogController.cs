using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystemWebApp.WebApi.Constants;
using ProjectManagementSystemWebApp.WebApi.Controllers.FileUpload;
using static ProjectManagementSystem.Services.Features.ImportLog.GetImportLog;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.ImportLog
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class LogController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LogController> _logger;

        public LogController(IMediator mediator, ILogger<LogController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(RouteKeys.DisplayImportLog)]
        public async Task<IActionResult> GetImportLogs(CancellationToken cancellationToken)
        {
            try
            {
                var importList = await _mediator.Send(new GetImportLogQuery(), cancellationToken);
                return Ok(importList);
            }catch(Exception exception)
            {
                _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                return BadRequest("An error in retrieving Import Logs");
            }
        }

    }
}
