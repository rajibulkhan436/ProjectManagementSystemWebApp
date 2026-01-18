using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pipelines.Sockets.Unofficial.Arenas;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseContext _dbContext;
        public TaskRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTaskAsync(ProjectTask task, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.Tasks.AddAsync(task, cancellationToken);
            }
            catch (Exception exception) 
            {
                throw new Exception($"New Task couldn't be added: {exception.Message}");
            }

        }

        public  async Task<bool> AlreadyExist(int projectId, string taskName, CancellationToken cancellationToken)
        {
           return await _dbContext.Tasks.AnyAsync(task => task.ProjectId == projectId && task.TaskName == taskName, cancellationToken);
        }

        public async Task AssignTaskAsync(TaskAssignment taskAssignment, CancellationToken cancellationToken)
        {
            try
            {
                if (taskAssignment == null)
                {
                    throw new Exception( "Task assignment cannot be null.");
                }

                await _dbContext.TaskAssignments.AddAsync(taskAssignment, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);           
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while assigning the task. Please try again later: {exception.Message}");
            }
        }

        public async Task DeleteTaskAsync(int taskId, CancellationToken cancellationToken)
        {
            try
            {
                var requiredTask = await _dbContext.Tasks.FindAsync(new Object[] { taskId }, cancellationToken);

                if(requiredTask == null)
                {
                    throw new KeyNotFoundException($"Task with ID {taskId} not found.");
                }
                _dbContext.Tasks.Remove(requiredTask);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while deleting the project: {exception.Message}", exception);
            }

        }

        public async  Task<IEnumerable<ProjectTask>> GetTaskByProjectIdAsync(int projectId, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ProjectTask> taskList = await _dbContext.Tasks.Where(task => task.ProjectId == projectId).ToListAsync(cancellationToken);
                return taskList;
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while fetching the task status:{exception.Message}");
            }
        }

        public async Task<IEnumerable<ProjectTask>> GetTaskStatusAsync(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ProjectTask> taskList = await _dbContext.Tasks
                                            .Include(task  => task.TaskAssignments)
                                            .Include(task => task.Comments)
                                            .ToListAsync(cancellationToken);
                return taskList;

            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while fetching the task status:{exception.Message}");
            }
        }

        public async Task<IEnumerable<ProjectTask>> GetTaskStatusAsync(int taskId, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<ProjectTask> taskList = await _dbContext.Tasks.Where(task => task.Id == taskId).Include(comm => comm.Comments).ToListAsync(cancellationToken);
                return taskList;
            }
            catch (Exception exception)
            {
                throw new Exception($"An error occurred while fetching the task status:{exception.Message}");
            }
        }

        

        public async Task UpdateTaskStatusAsync(ProjectTask task, CancellationToken cancellationToken)
        {
            var requiredTask = await _dbContext.Tasks.FindAsync(task.Id, cancellationToken);
            if (requiredTask != null)
            {
                requiredTask.Status = task.Status;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
