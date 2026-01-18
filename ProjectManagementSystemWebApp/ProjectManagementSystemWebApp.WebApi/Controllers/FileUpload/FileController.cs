using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Core.NotificationHub;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystemWebApp.WebApi.Constants;
using ProjectManagementSystemWebApp.WebApi.Controllers.Projects;
using static ProjectManagementSystem.Services.Features.Tasks.UploadTaskFile;

namespace ProjectManagementSystemWebApp.WebApi.Controllers.FileUpload
{
    [ApiController]
    [Route(RouteKeys.MainRoute)]
    public class FileController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FileController> _logger;

        public FileController(IMediator mediator, ILogger<FileController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost(RouteKeys.UploadFile)]
        public async Task<IActionResult> UploadFileAsync([FromForm] UploadTaskFileCommand command,
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
                return BadRequest("An error while uploading file");
            }

            
        }
    }
}
