using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectStatusAsync(CancellationToken cancellationToken); 

        Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken);

        Task<IEnumerable<Project>> GetProjectStatusAsync(int projectId, CancellationToken cancellationToken);

        Task UpdateProjectStatusAsync(ICollection<Project> projectList, CancellationToken cancellationToken);

        Task<IEnumerable<ProjectReport>> GetProjectReportAsync(CancellationToken cancellationToken);

        Task<Boolean> ExistsAsync(int projectId, CancellationToken cancellationToken);

    }
}
