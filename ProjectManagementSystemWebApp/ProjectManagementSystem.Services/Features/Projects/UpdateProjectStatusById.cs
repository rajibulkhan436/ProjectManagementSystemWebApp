using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Core.Enums;
using ProjectManagementSystem.Entities;
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class UpdateProjectStatusById
    {
        public class UpdateProjectStatusCommand : IRequest<string>
        {
            public int ProjectId { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public int Status { get; set; }

        }  

        public class Handler : IRequestHandler<UpdateProjectStatusCommand, string>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;              
            }

            public async Task<string> Handle(UpdateProjectStatusCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var projects = await _unitOfWork.ProjectRepository.GetProjectStatusAsync(command.ProjectId, cancellationToken);

                    if (projects == null)
                    {
                        return $"Project with ID {command.ProjectId} not found.";
                    }
                    
                    foreach (var project in projects)
                    {
                        project.Status = (Status)command.Status;  
                    }
                    
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return $"Project ID {command.ProjectId} status updated to {command.Status}.";
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error updating project status.");
                    return "An error occurred while updating the project status.";
                }
            }
        }
    }
}

