
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface ITeamRepository
    {

        Task<IEnumerable<TeamMember>> GetTeamMembersAsync(CancellationToken cancellationToken);

        Task<TeamMember> GetTeamMemberByIdAsync(int teamMemberId, CancellationToken cancellationToken);

        Task<IEnumerable<TaskTeam>> GetTaskTeamMembersAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TaskTeam>> GetTaskTeamMembersAsync(int taskId, CancellationToken cancellationToken);

    }
}
