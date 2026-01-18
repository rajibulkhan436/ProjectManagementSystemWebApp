using System.Text.Json.Serialization;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Core.Enums;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.DTOs.CommentDTos;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;

namespace ProjectManagementSystem.Services.Features.Tasks
{
    public class AddTask
    {
        public class AddTaskCommand : IRequest<string> 
        {
            public string TaskName { get; set; }

            public string TaskDescription { get; set; }

            public int ProjectId { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Status Status { get; set; }

            public DateTime DueDate { get; set; }
            public CommentDto? Comment { get; set; }

            public TaskAssignmentDto? TaskAssignment {  get; set; }

        }

        public class Handler : IRequestHandler<AddTaskCommand, string> 
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<string> Handle(AddTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    if (!await IsProjectValid(command.ProjectId, cancellationToken))
                    {
                        return $"Project with ID: {command.ProjectId} not Found.";
                    }

                    var taskEntity = await CreateTask(command, cancellationToken);
                    await AssignTask(command, taskEntity.Id, cancellationToken);
                    await AddInitialComment(command, taskEntity.Id, cancellationToken);

                    return $"New Task: {command.TaskName} has been added successfully";
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error While Adding New Task");
                    return "An error occurred while adding new Task.";
                }
            }
            private async Task<bool> IsProjectValid(int projectId, CancellationToken cancellationToken)
            {
                return await _unitOfWork.ProjectRepository.ExistsAsync(projectId, cancellationToken);
            }

            private async Task<ProjectTask> CreateTask(AddTaskCommand command, CancellationToken cancellationToken)
            {
                var taskEntity = command.Adapt<ProjectTask>();
                if (taskEntity != null)
                {
                    await _unitOfWork.TaskRepository.AddTaskAsync(taskEntity, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return taskEntity;
            }

            private async Task AssignTask(AddTaskCommand command, int taskId, CancellationToken cancellationToken)
            {
                var taskAssignmentEntity = command.TaskAssignment.Adapt<TaskAssignment>();
                if (taskAssignmentEntity != null)
                {
                    taskAssignmentEntity.TaskId = taskId;
                    await _unitOfWork.TaskRepository.AssignTaskAsync(taskAssignmentEntity, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            private async Task AddInitialComment(AddTaskCommand command, int taskId, CancellationToken cancellationToken)
            {
                var commentEntity = command.Comment.Adapt<Comment>();
                if (commentEntity != null)
                {
                    commentEntity.TaskId = taskId;
                    await _unitOfWork.CommentRepository.AddComment(commentEntity, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
        }

    }
}
