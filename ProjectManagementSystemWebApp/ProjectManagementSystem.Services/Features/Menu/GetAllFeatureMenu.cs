using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.FeaturesDTOs;

namespace ProjectManagementSystem.Services.Features.Menu
{
    public class GetAllFeatureMenu
    {
        public class GetAllFeatureMenuQuery : IRequest<ICollection<FeatureCategoryDto>>
        {
        }

        public class Handler : IRequestHandler<GetAllFeatureMenuQuery, ICollection<FeatureCategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProjectTaskManager _projectTaskManager;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IProjectTaskManager projectManager, ILogger<Handler> logger)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
                _projectTaskManager = projectManager;
            }

            public async Task<ICollection<FeatureCategoryDto>> Handle(GetAllFeatureMenuQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var list = await _unitOfWork.MenuRepository.GetfeaturesAsync(cancellationToken);
                    var dtoList = _projectTaskManager.MapFeatureEntityToDto(list);
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
