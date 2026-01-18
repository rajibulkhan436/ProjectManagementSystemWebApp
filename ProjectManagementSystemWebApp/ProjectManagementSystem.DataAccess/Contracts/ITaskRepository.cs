using System.Threading.Tasks;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetTaskStatusAsync(CancellationToken cancellationToken);

        Task<IEnumerable<ProjectTask>> GetTaskStatusAsync(int taskId, CancellationToken cancellationToken);

        Task UpdateTaskStatusAsync(ProjectTask task, CancellationToken cancellationToken);

        Task<IEnumerable<ProjectTask>> GetTaskByProjectIdAsync(int projectId, CancellationToken cancellationToken);

        Task AssignTaskAsync(TaskAssignment taskAssignment, CancellationToken cancellationToken);

        Task DeleteTaskAsync(int taskId, CancellationToken cancellationToken);

        Task AddTaskAsync(ProjectTask task, CancellationToken cancellationToken);

        Task<bool> AlreadyExist(int projectId, string taskName, CancellationToken cancellationToken);
    }
}
