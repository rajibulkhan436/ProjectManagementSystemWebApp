using Microsoft.EntityFrameworkCore;
using Pipelines.Sockets.Unofficial.Arenas;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.Core.Enums;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DatabaseContext _dbContext;
               
        public ProjectRepository(DatabaseContext dbContext) 
        {
            _dbContext = dbContext;        
        }

        public async Task<IEnumerable<Project>> GetProjectStatusAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Project> projectList = await _dbContext.Projects.ToListAsync(cancellationToken);
                return projectList;
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while fetching the project status:{exception.Message}");
            }
        }

        public async Task<IEnumerable<Project>> GetProjectStatusAsync(int projectId, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine(projectId);
                IEnumerable<Project> projectList = await _dbContext.Projects.Where(project => project.Id == projectId).ToListAsync(cancellationToken);
                return projectList;
            }
            catch(Exception exception)
            {
                throw new Exception($"An error occurred while fetching the project status : {exception.Message}");
            }
        }

        public async Task UpdateProjectStatusAsync(ICollection<Project> projectList, CancellationToken cancellationToken)
        {
            if (projectList == null || !projectList.Any())
            {
                throw new Exception("Project list cannot be null or empty.");
            }

            foreach (var project in projectList)
            {
                var requiredProject = await _dbContext.Projects.FindAsync(project.Id, cancellationToken);
                if (requiredProject != null)
                {
                    requiredProject.Status = project.Status;  
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectReport>> GetProjectReportAsync(CancellationToken cancellationToken)
        {
            var projectReport = await (from p in _dbContext.Projects
                                       join t in _dbContext.Tasks on p.Id equals t.ProjectId into taskGroup
                                       from t in taskGroup.DefaultIfEmpty() 
                                       orderby p.Id
                                       select new ProjectReport
                                       {
                                           ProjectName = p.ProjectName,
                                           TaskName = t.TaskName,
                                           TaskStatus = t.Status,
                                           DueDate = t.DueDate, 
                                           ProjectStatus = p.Status
                                       }).ToListAsync(cancellationToken);

            return projectReport;
        }

        public async Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine(await _dbContext.Projects.Where(project => project.Id == projectId).ToListAsync(cancellationToken));
               

               var project = await this.GetProjectStatusAsync(projectId, cancellationToken);
                Console.WriteLine(project);
                if (project == null)
                {
                    throw new KeyNotFoundException($"Project with ID {projectId} not found.");
                }
                
                _dbContext.Projects.Remove(project.FirstOrDefault());
                await _dbContext.SaveChangesAsync(cancellationToken);
            }catch(Exception exception)
            {
                throw new Exception($"An error occurred while deleting the project: {exception.Message}", exception);
            }
          
        }

        public async Task<bool> ExistsAsync(int projectId, CancellationToken cancellationToken)
        {
            return await _dbContext.Projects.AnyAsync(project => project.Id == projectId, cancellationToken);
        }
    }
}
