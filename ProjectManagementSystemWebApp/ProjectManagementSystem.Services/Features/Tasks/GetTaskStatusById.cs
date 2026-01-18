using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class GetTaskStatusById
    {
        public class GetTaskStatusByIdQuery : IRequest<ICollection<TaskDto>>
        {
            public int TaskId {  get; set; }

            public GetTaskStatusByIdQuery(int taskId) 
            {
                TaskId = taskId;
            }
        }

        public class Handler : IRequestHandler<GetTaskStatusByIdQuery, ICollection<TaskDto>>
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

            public async Task<ICollection<TaskDto>> Handle(GetTaskStatusByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var list = await _unitOfWork.TaskRepository.GetTaskStatusAsync(query.TaskId, cancellationToken);
                    var dtoList = _projectTaskManager.MapTaskEntityToDto(list);
                   // var taskList = await _projectTaskManager.CreateTaskStatusTable(dtoList, cancellationToken);
                    return dtoList;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                    throw new Exception("There is a error in Retrieving the Task status.");
                }
            }

        }
    }
}
