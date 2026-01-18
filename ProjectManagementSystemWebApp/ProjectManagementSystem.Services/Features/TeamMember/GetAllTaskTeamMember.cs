using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.TeamDTOs;

namespace ProjectManagementSystem.Services.Features.TeamMember
{
    public class GetAllTaskTeamMember
    {
        public class GetAllTaskTeamQuery : IRequest<ICollection<TaskTeamDto>>
        {

        }

        public class Handler : IRequestHandler<GetAllTaskTeamQuery, ICollection<TaskTeamDto>>
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

            public async Task<ICollection<TaskTeamDto>> Handle(GetAllTaskTeamQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var list = await _unitOfWork.TeamRepository.GetTaskTeamMembersAsync(cancellationToken);
                    var dtoList = _projectTaskManager.MapTaskTeamEntityToDto(list);
                   // var taskTeamList = await _projectTaskManager.CreateTaskTeamTable(dtoList, cancellationToken);
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
