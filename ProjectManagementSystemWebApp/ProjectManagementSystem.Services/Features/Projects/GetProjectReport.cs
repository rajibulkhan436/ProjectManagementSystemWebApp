using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.ProjectDTOs;

namespace ProjectManagementSystem.Services.Features.Projects
{
    public class GetProjectReport
    {

        public class GetProjectReportQuery : IRequest<ICollection<ProjectReportDto>>
        {
           
        }

        public class Handler : IRequestHandler<GetProjectReportQuery, ICollection<ProjectReportDto>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProjectTaskManager _projectTaskManager;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IProjectTaskManager projectTaskmanger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _projectTaskManager = projectTaskmanger;
            }
            public async Task<ICollection<ProjectReportDto>> Handle(GetProjectReportQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var projectReport = await _unitOfWork.ProjectRepository.GetProjectReportAsync(cancellationToken);
                    var projectReportList = _projectTaskManager.MapProjectReportToDto(projectReport);
                    return projectReportList;
                   //return await _projectTaskManager.CreateProjectReportTable(projectReportList, cancellationToken);
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
