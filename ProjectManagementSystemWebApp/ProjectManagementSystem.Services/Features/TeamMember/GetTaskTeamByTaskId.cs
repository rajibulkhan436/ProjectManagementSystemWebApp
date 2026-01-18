using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.TeamDTOs;

namespace ProjectManagementSystem.Services.Features.TeamMember
{
    public class GetTaskTeamByTaskId
    {
        public class GetTaskTeamByIdQuery : IRequest<ICollection<TaskTeamDto>>
        {
            public int TaskId { get; set; }

            public GetTaskTeamByIdQuery(int taskId)
            {
                TaskId = taskId;
            }

            public class Handler : IRequestHandler<GetTaskTeamByIdQuery, ICollection<TaskTeamDto>>
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

                public async Task<ICollection<TaskTeamDto>> Handle(GetTaskTeamByIdQuery query, CancellationToken cancellationToken)
                {
                    try
                    {
                        var list = await _unitOfWork.TeamRepository.GetTaskTeamMembersAsync(query.TaskId, cancellationToken);
                        var taskTeamList = _projectTaskManager.MapTaskTeamEntityToDto(list);
                       // var taskTeamList = await _projectTaskManager.CreateTaskTeamTable(DtoList, cancellationToken);
                        return taskTeamList;
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
}
