using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.ProjectDTOs;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class GetAllProjectStatus 
    {
        public class GetAllProjectStatusQuery : IRequest<ICollection<ProjectDto>>
        {
        }

        public class Handler : IRequestHandler<GetAllProjectStatusQuery, ICollection<ProjectDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProjectTaskManager _projectTaskManager;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IProjectTaskManager projectManager, ILogger<Handler> logger)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
                _projectTaskManager = projectManager;
            }

            public async Task<ICollection<ProjectDto>> Handle(GetAllProjectStatusQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var list = await _unitOfWork.ProjectRepository.GetProjectStatusAsync(cancellationToken);
                    var dtoList = _projectTaskManager.MapProjectEntityToDto(list);
                    return dtoList;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                    throw new Exception(exception.Message);
                }
            }
        }
    }
}
