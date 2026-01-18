using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Services.Features.Tasks
{
    public class AssignTaskToMember
    {
        public class AssignTaskToMemberCommand : IRequest<string>
        {
            public int TaskId { get; set; }
            public int TeamMemberId { get; set; }
            public DateTime AssignDate { get; set; }

        }

        public class Handler : IRequestHandler<AssignTaskToMemberCommand, string>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;

            }

            public async Task<string> Handle(AssignTaskToMemberCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var task = await _unitOfWork.TaskRepository.GetTaskStatusAsync(command.TaskId, cancellationToken);
                    if (task == null)
                    {
                        return $"Task with ID {command.TaskId} not found.";
                    }

                    var teamMember = await _unitOfWork.TeamRepository.GetTeamMemberByIdAsync(command.TeamMemberId, cancellationToken);
                    if (teamMember == null)
                    {
                        return $"Team Member with ID {command.TeamMemberId} not found.";
                    }

                    var taskAssignment = new TaskAssignment
                    {
                        TaskId = command.TaskId,
                        TeamMemberId = command.TeamMemberId,
                        AssignDate = DateTime.Now
                    };

                    await _unitOfWork.TaskRepository.AssignTaskAsync(taskAssignment, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return $"Task {command.TaskId} successfully assigned to Team Member {command.TeamMemberId}.";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while assigning task.");
                    return "An error occurred while assigning the task.";
                }

            }

        }
    }
}
