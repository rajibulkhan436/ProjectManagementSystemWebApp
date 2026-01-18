using System.Text.Json.Serialization;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.Core.Enums;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.CommentDTos;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class UpdateTaskStatusById
    {
        public class UpdateTaskStatusCommand : IRequest<string>
        {
            public int Id { get; set; }

            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Status Status { get; set; }

            public CommentDto? Comment { get; set; }

        }

        public class Handler : IRequestHandler<UpdateTaskStatusCommand, string>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;

            }

            public async Task<string> Handle(UpdateTaskStatusCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var taskList = await _unitOfWork.TaskRepository.GetTaskStatusAsync(command.Id, cancellationToken);
                    if (taskList == null)
                    {
                        return $"Task with ID {command.Id} not found.";
                    }
                    var newComment = command.Comment.Adapt<Comment>();
                    if (newComment != null) 
                    {
                        newComment.UserId = command.Comment.UserId;

                        var userExists = await _unitOfWork.UserRepository.ExistsAsync(newComment.UserId, cancellationToken);
                        if (!userExists)
                        {
                            return $"User with ID {newComment.UserId} does not exist.";
                        }
                    }
                    
                    var task = taskList.FirstOrDefault();
                        task.Status = (Status)command.Status;
                    
                    await _unitOfWork.TaskRepository.UpdateTaskStatusAsync(task,cancellationToken);
                    await _unitOfWork.CommentRepository.AddComment(newComment,cancellationToken);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return $"Task ID {command.Id} status updated to {command.Status}.";
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error updating Task status.");
                    return "An error occurred while updating the Task status.";
                }
            }


        }
    }
}
