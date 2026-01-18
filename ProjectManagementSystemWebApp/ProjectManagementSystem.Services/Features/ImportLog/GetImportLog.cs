
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.ImportDTOs;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;
using static ProjectManagementSystem.Services.Features.ImportLog.GetImportLog;
using static ProjectManagementSystem.Services.Features.Projects.GetAllTaskStatus;

namespace ProjectManagementSystem.Services.Features.ImportLog
{
    public class GetImportLog
    {
        public class GetImportLogQuery : IRequest<ICollection<ImportDto>>
        {
        }

        public class Handler : IRequestHandler<GetImportLogQuery, ICollection<ImportDto>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProjectTaskManager _projectTaskManager;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IProjectTaskManager projectTaskManager)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _projectTaskManager = projectTaskManager;
            }

            public async Task<ICollection<ImportDto>> Handle(GetImportLogQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var importList = await _unitOfWork.ImportRepository.GetImportsAsync(cancellationToken);
                    var importDtoList = _projectTaskManager.MapImportEntityToDto(importList);
                    return importDtoList;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"There's a Exception: {exception.Message}");
                    throw new Exception("There is a error in retrieving import Logs");
                }
            }
        }
    }
}
