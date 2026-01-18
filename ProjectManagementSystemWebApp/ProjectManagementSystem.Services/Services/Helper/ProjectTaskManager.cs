using System.Collections.Immutable;
using System.Text;
using System.Xml.Linq;
using ExcelDataReader;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Core.Enums;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.Constants;
using ProjectManagementSystem.Services.Contracts;
using ProjectManagementSystem.Services.DTOs.CommentDTos;
using ProjectManagementSystem.Services.DTOs.FeaturesDTOs;
using ProjectManagementSystem.Services.DTOs.ImportDTOs;
using ProjectManagementSystem.Services.DTOs.ProjectDTOs;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;
using ProjectManagementSystem.Services.DTOs.TeamDTOs;
using static System.Net.Mime.MediaTypeNames;


namespace ProjectManagementSystem.Services.Services.Helper
{
    public class ProjectTaskManager : IProjectTaskManager
    {
        private readonly IMapper _mapper;
        

        public ProjectTaskManager(IMapper mapper, IFileManager fileManager)
        {
            _mapper = mapper;

        }


        public ICollection<TaskDto> TaskListFromFile(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var taskList = new List<TaskDto>();

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            do
            {
                bool isHeaderSkipped = false;

                while (reader.Read())
                {
                    if (!isHeaderSkipped)
                    {
                        isHeaderSkipped = true;
                        continue;
                    }

                    var taskName = reader.GetValue(0)?.ToString();
                    var description = reader.GetValue(1)?.ToString();
                    var projectIdStr = reader.GetValue(2)?.ToString();
                    var statusStr = reader.GetValue(3)?.ToString();
                    var dueDateStr = reader.GetValue(4)?.ToString();

                    if (int.TryParse(projectIdStr, out int projectId) &&
                        int.TryParse(statusStr, out int statusInt) &&
                        DateTime.TryParse(dueDateStr, out DateTime dueDate) &&
                        !string.IsNullOrWhiteSpace(taskName))
                    {
                        var task = new TaskDto
                        {
                            TaskName = taskName,
                            TaskDescription = description,
                            ProjectId = projectId,
                            Status = (Status)statusInt,
                            DueDate = dueDate
                        };

                        taskList.Add(task);
                    }
                }

            } while (reader.NextResult());

            return taskList;
        }

        public async Task<string> CreateProjectStatusTable(ICollection<ProjectDto> projects,CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                StringBuilder table = new StringBuilder($"{FieldConstants.Id,-5}{FieldConstants.ProjectName,-30}{FieldConstants.Status,-15}\n" +
                                    $"{new string('-', 50)}\n");

                foreach (var project in projects)
                {
                    table.Append($"{project.ProjectId,-5}{project.ProjectName,-30}{project.Status,-15}\n");
                }

                return table.ToString();
            }, cancellationToken);
        }

        public ICollection<ProjectDto> MapProjectEntityToDto(IEnumerable<Project> projectList)
        {
            ICollection<ProjectDto> allProjectList = new List<ProjectDto>();

            foreach(var projectEntity in projectList)
            {
                ProjectDto projectDto = _mapper.Map<ProjectDto>(projectEntity);
                allProjectList.Add(projectDto);
            }
            return allProjectList;
        }

        public ICollection<Project> MapProjectDtoToEntity(ICollection<ProjectDto> projects)
        {
            ICollection<Project> projectList = new List<Project>();

            foreach(var projectDto in projects)
            {
                Project projectentity = _mapper.Map<Project>(projectDto);
                projectList.Add(projectentity);
            }
            return projectList;
        }

        public ICollection<TaskDto> MapTaskEntityToDto(IEnumerable<ProjectTask> taskList)
        {
            ICollection<TaskDto> allTaskList = taskList.AsQueryable()
                                                       .ProjectToType<TaskDto>()
                                                       .ToList();
            
            return allTaskList;
        }

        public ICollection<ProjectTask> MapTaskDtoToEntity(IEnumerable<TaskDto> taskList)
        {
            ICollection<ProjectTask> ProjectTask = taskList.AsQueryable()
                                                           .ProjectToType<ProjectTask>()
                                                           .ToList();
            return ProjectTask;
        }

        public async Task<string> CreateTaskStatusTable(ICollection<TaskDto> tasks, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                StringBuilder table = new StringBuilder($"{FieldConstants.Id,-5}" +
                                    $"{FieldConstants.TaskName,-30}{FieldConstants.Status,-15}\n" +
                    $"{new string('-', 50)}\n");
                foreach (var task in tasks)
                {
                    table.Append($"{task.Id,-5}{task.TaskName,-30}{task.Status,-15}\n");
                }

                return table.ToString();
            }, cancellationToken);

        }

        public ICollection<TaskTeamDto> MapTaskTeamEntityToDto(IEnumerable<TaskTeam> taskTeamList)
        {
            ICollection<TaskTeamDto> allTaskTeamList = taskTeamList.AsQueryable()
                                                                   .ProjectToType<TaskTeamDto>()
                                                                   .ToList();
            return allTaskTeamList;
        }

        public ICollection<ProjectReportDto> MapProjectReportToDto(IEnumerable<ProjectReport> reports)
        {
            ICollection<ProjectReportDto> allProjectReports = reports.AsQueryable()
                                                                     .ProjectToType<ProjectReportDto>()
                                                                     .ToList();
            return allProjectReports;
        }
        public ICollection<FeatureCategoryDto> MapFeatureEntityToDto(IEnumerable<FeaturesCategory> features)
        {
            ICollection<FeatureCategoryDto> featuresList = features.AsQueryable()
                                                           .ProjectToType<FeatureCategoryDto>()
                                                           .ToList();
            return featuresList;
        }


        public async Task<string> CreateTaskTeamTable(IEnumerable<TaskTeamDto> taskTeamList, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                StringBuilder table = new StringBuilder($"{FieldConstants.TaskId,-10}{FieldConstants.TaskName,-30}" +
                                                        $"{FieldConstants.TeamMemberId,-15}{FieldConstants.TeamMemberName,-20}" +
                                                        $"{FieldConstants.AssignedDate,-25}\n" +
                    $"{new string('-', 100)}\n");
                foreach (var taskTeam in taskTeamList)
                {
                    table.Append($"{taskTeam.TaskId,-10}{taskTeam.TaskName,-30}{taskTeam.TeamMemberId,-15}" +
                                    $"{taskTeam.TeamMemberName,-20}{taskTeam.AssignedDate,-25}\n");
                }

                return table.ToString();
            }, cancellationToken);           
        }

        public async Task<string> CreateProjectReportTable(ICollection<ProjectReportDto> projectReportList, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
            StringBuilder table = new StringBuilder($"{FieldConstants.ProjectName,-25}{FieldConstants.TaskName,-30}" +
                                    $"{FieldConstants.TaskStatus,-15}{FieldConstants.DueDate,-25}" +
                                    $"{FieldConstants.ProjectStatus,-15}\n"+
                                    $"{new string('-', 110)}\n");
                foreach (var report in projectReportList)
                {
                    table.Append($"{report.ProjectName,-25}{report.TaskName,-30}" +
                                $"{report.TaskStatus,-15}{report.DueDate,-25}{report.ProjectStatus,-15}\n");
                }

                return table.ToString();
            }, cancellationToken);            
        }

        public ICollection<TeamDto> MapTeamEntityToDto(IEnumerable<TeamMember> teamMemberList)
        {

            ICollection<TeamDto> allTeamMemberList = teamMemberList.AsQueryable()
                                                                   .ProjectToType<TeamDto>()
                                                                   .ToList();
            return allTeamMemberList;
        }

        public ICollection<TeamMember> MapTeamDtoToEntity(IEnumerable<TeamDto> teamMemberList)
        {
            ICollection<TeamMember> allTeamMemberList = teamMemberList.AsQueryable()
                                                                   .ProjectToType<TeamMember>()
                                                                   .ToList();
            return allTeamMemberList;
        }

        public ICollection<CommentDto> MapCommentEntityToDto(ICollection<Comment> comments)
        {
            ICollection<CommentDto> commentDtoList = comments.AsQueryable()
                                                            .ProjectToType<CommentDto>()
                                                            .ToList();
            return commentDtoList;
        }

        public ICollection<Comment> MapCommentDtoToEntity(ICollection<CommentDto> comments)
        {
            ICollection<Comment> commentList = comments.AsQueryable()
                                                        .ProjectToType<Comment>()
                                                        .ToList();
            return commentList;
        }

        public ICollection<ImportDto> MapImportEntityToDto(ICollection<Import> importList)
        {
            ICollection<ImportDto> importDtoList = importList.AsQueryable()
                                                            .ProjectToType<ImportDto>()
                                                            .ToList();
            return importDtoList;
        }

        public ICollection<Import> MapImportDtoToEntity(ICollection<ImportDto> importList)
        {
            ICollection<Import> importEntityList = importList.AsQueryable()
                                                            .ProjectToType<Import>()
                                                            .ToList();
            return importEntityList;
        }
    }
}
