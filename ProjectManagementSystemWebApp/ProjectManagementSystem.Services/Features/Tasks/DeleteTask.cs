
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;

namespace ProjectManagementSystem.Services.Features.Tasks
{
    public class DeleteTask
    {
        public class DeleteTaskCommand : IRequest<Boolean>
        {
            public int Id { get; }

            public DeleteTaskCommand(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<DeleteTaskCommand, Boolean>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Boolean> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    await _unitOfWork.TaskRepository.DeleteTaskAsync(command.Id, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"An error occurred while deleting the Task: {exception.Message}");
                    throw new Exception($"An error occurred while deleting the task: {exception.Message}", exception);
                }
            }
        }
    }
}
