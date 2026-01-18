using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class GetAllTaskStatus
    {

        public class GetAllTaskStatusQuery : IRequest<ICollection<TaskDto>>
        {

        }

        public class Handler : IRequestHandler<GetAllTaskStatusQuery, ICollection<TaskDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProjectTaskManager _projectTaskManager;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IProjectTaskManager projectTaskManager, ILogger<Handler> logger)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
                _projectTaskManager = projectTaskManager;
            }

            public async Task<ICollection<TaskDto>> Handle(GetAllTaskStatusQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var list = await _unitOfWork.TaskRepository.GetTaskStatusAsync(cancellationToken);
                    var dtoList = _projectTaskManager.MapTaskEntityToDto(list);
                    return dtoList;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                    throw new Exception("There is a error in retrieving task status.");
                }
            }
        }
    }
}
