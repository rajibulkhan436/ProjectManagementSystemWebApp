using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DatabaseContext _dbContext;
        public TeamRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TaskTeam>> GetTaskTeamMembersAsync(CancellationToken cancellationToken)
        {
            var taskTeamList = await(from ta in _dbContext.TaskAssignments
                                     join t in _dbContext.Tasks on ta.TaskId equals t.Id
                                     join tm in _dbContext.TeamMembers on ta.TeamMemberId equals tm.Id
                                     select new TaskTeam
                                     {
                                         TaskId = t.Id,
                                         TaskName = t.TaskName,
                                         TeamMemberId = tm.Id,
                                         TeamMemberName = tm.Name,
                                         AssignedDate = ta.AssignDate
                                     }).ToListAsync(cancellationToken);

            return taskTeamList;
        }

        public async Task<IEnumerable<TaskTeam>> GetTaskTeamMembersAsync(int taskId, CancellationToken cancellationToken)
        {         
            var allTaskTeamMembers = await GetTaskTeamMembersAsync(cancellationToken);
            var filteredTaskTeamMembers = allTaskTeamMembers.Where(taskTeam => taskTeam.TaskId == taskId);

            return filteredTaskTeamMembers;
        }

        public async Task<IEnumerable<TeamMember>> GetTeamMembersAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.TeamMembers.ToListAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while retrieving team members. Please try again later.: {exception.Message}");
            }
        }

        public async Task<TeamMember> GetTeamMemberByIdAsync(int teamMemberId, CancellationToken cancellationToken)
        {
            try
            {
                var teamMember = await _dbContext.TeamMembers.FindAsync(teamMemberId);

                if (teamMember == null)
                {
                    throw new Exception($"Team member with ID {teamMemberId} not found.");
                }

                return teamMember;
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while retrieving the team member. Please try again later.: {exception.Message}");
            }
        }


    }
}
