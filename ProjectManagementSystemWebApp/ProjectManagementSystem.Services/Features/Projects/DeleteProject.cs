using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class DeleteProject
    {
        //public class IntToStringConverter : JsonConverter<int>
        //{
        //    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //    {
        //        return int.Parse(reader.GetString() ?? "0");
        //    }

        //    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        //    {
        //        writer.WriteStringValue(value.ToString());
        //    }
        //}
        public class DeleteProjectCommand : IRequest<Boolean>
        { 
            public int ProjectId { get; }

            public DeleteProjectCommand(int projectId)
            {
                ProjectId = projectId;
            }
        }

        public class Handler : IRequestHandler<DeleteProjectCommand, Boolean>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Boolean> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    await _unitOfWork.ProjectRepository.DeleteProjectAsync(command.ProjectId,cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return true;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"An error occurred while deleting the project: {exception.Message}");
                    throw new Exception($"An error occurred while deleting the project: {exception.Message}", exception);
                }
            }
        }
    }
}
