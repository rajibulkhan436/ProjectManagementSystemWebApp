
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;
using ProjectManagementSystem.Services.Services.Helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectManagementSystem.Services.Features.Tasks
{
    public class UploadTaskFile
    {
        public class UploadTaskFileCommand : IRequest<string>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<UploadTaskFileCommand, string>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IProjectTaskManager _projectTaskManager;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IProjectTaskManager projectTaskManager)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _projectTaskManager = projectTaskManager;
            }

            public async Task<string> Handle(UploadTaskFileCommand command, CancellationToken cancellationToken)
            {
                int count = 0;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var filePath = await SaveUploadedFileAsync(command.File);
                if (filePath == null) return "File upload failed.";


                int importId = await AddToImportLog(command, cancellationToken);
                
                var taskEntities = GetTaskEntitiesFromFile(filePath);
                var totalTask = taskEntities.Count();
                var successCount = await SaveTasksAsync(importId, count, taskEntities, cancellationToken);
                int failCount = totalTask - successCount;

                await updateImportLog(importId, totalTask, successCount, failCount, cancellationToken);

                return $"File saved and {successCount} tasks added successfully And {failCount} failed.";
            }

            private async Task updateImportLog(int importId, int totalTask, int successCount, int failCount, CancellationToken cancellationToken)
            {
                Import updatedImport = new Import
                {
                    Id = importId,
                    Total = totalTask,
                    Success = successCount,
                    Fails = failCount
                };
                await _unitOfWork.ImportRepository.UpdateImportAsync(updatedImport, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            private async Task<int> AddToImportLog(UploadTaskFileCommand command, CancellationToken cancellationToken)
            {
                Import import = new Import
                {
                    FileName = command.File.FileName,
                    UploadTime = DateTime.Now,
                    Total = 0,
                    Success = 0,
                    Fails = 0
                };

                await _unitOfWork.ImportRepository.AddImportLogAsync(import, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return import.Id;
            }

            private async Task<string?> SaveUploadedFileAsync(IFormFile file)
            {
                if (file == null || file.Length <= 0) return null;

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                return filePath;
            }

            private ICollection<ProjectTask> GetTaskEntitiesFromFile(string filePath)
            {
                var taskList = _projectTaskManager.TaskListFromFile(filePath);
                return _projectTaskManager.MapTaskDtoToEntity(taskList);
            }

            private async Task<int> SaveTasksAsync(int importId, int count, ICollection<ProjectTask> tasks, CancellationToken cancellationToken)
            {
                foreach (var task in tasks)
                {
                    
                    if (IsProjectExist(task.ProjectId, cancellationToken).Result)
                    {
                        if (TaskAlreadyExists(task.ProjectId, task.TaskName, cancellationToken).Result)
                        {
                            await SaveFailedImport(importId, task.TaskName, "Same Task Already exists For the Project", cancellationToken);

                        }
                        else
                        {
                            await _unitOfWork.TaskRepository.AddTaskAsync(task, cancellationToken);
                            count++;
                        }
                    }
                    else
                    {
                        await SaveFailedImport(importId, task.TaskName, "Project Id mentioned Doesn't Exist", cancellationToken);
                    }

                }
                               
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return count;
            }

            private async Task<bool> IsProjectExist(int projectId ,CancellationToken cancellationToken)
            { 
                   
                    return await _unitOfWork.ProjectRepository.ExistsAsync(projectId, cancellationToken);

            }

            private async Task<bool> TaskAlreadyExists(int projectId, string taskName ,CancellationToken cancellationToken)
            {
                return await _unitOfWork.TaskRepository.AlreadyExist(projectId, taskName, cancellationToken);
            }

            private async Task SaveFailedImport(int importId, string taskName,string failureReason, CancellationToken cancellationToken)
            {
                FailedImport failLog = new FailedImport
                {
                    ImportId = importId,
                    TaskName = taskName,
                    FailureReason = failureReason,
                };
                await _unitOfWork.ImportRepository.AddTaskImportAsync(failLog, cancellationToken);

            }

        }
    }
}
