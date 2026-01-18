using Microsoft.EntityFrameworkCore.Metadata;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.DTOs.CommentDTos;
using ProjectManagementSystem.Services.DTOs.FeaturesDTOs;
using ProjectManagementSystem.Services.DTOs.ImportDTOs;
using ProjectManagementSystem.Services.DTOs.ProjectDTOs;
using ProjectManagementSystem.Services.DTOs.TaskDTOs;
using ProjectManagementSystem.Services.DTOs.TeamDTOs;

namespace ProjectManagementSystem.Services.Contracts
{
    public interface IProjectTaskManager
    {
        Task<string> CreateProjectStatusTable(ICollection<ProjectDto> projects, CancellationToken cancellationToken);

        ICollection<ProjectDto> MapProjectEntityToDto(IEnumerable<Project> projectList);

        ICollection<Project> MapProjectDtoToEntity(ICollection<ProjectDto> projects);

        ICollection<TaskDto> MapTaskEntityToDto(IEnumerable<ProjectTask> taskList);

        ICollection<ProjectTask> MapTaskDtoToEntity(IEnumerable<TaskDto> taskList);

        ICollection<TeamDto> MapTeamEntityToDto(IEnumerable<TeamMember> teamMemberList);

        ICollection<TeamMember> MapTeamDtoToEntity(IEnumerable<TeamDto> teamMemberList);

        ICollection<ProjectReportDto> MapProjectReportToDto(IEnumerable<ProjectReport> reports);

        public ICollection<FeatureCategoryDto> MapFeatureEntityToDto(IEnumerable<FeaturesCategory> features);

        Task<string> CreateTaskStatusTable(ICollection<TaskDto> tasks, CancellationToken cancellationToken);

        ICollection<TaskTeamDto> MapTaskTeamEntityToDto(IEnumerable<TaskTeam> taskTeamList);

        Task<string> CreateTaskTeamTable(IEnumerable<TaskTeamDto> taskTeamList, CancellationToken cancellationToken);

        Task<string> CreateProjectReportTable(ICollection<ProjectReportDto> projectList, CancellationToken cancellationToken);

        ICollection<CommentDto> MapCommentEntityToDto(ICollection<Comment> comments);

        ICollection<Comment> MapCommentDtoToEntity(ICollection<CommentDto> comments);

        ICollection<TaskDto> TaskListFromFile(string filePath);

        ICollection<ImportDto> MapImportEntityToDto(ICollection<Import> importList);

        ICollection<Import> MapImportDtoToEntity(ICollection<ImportDto> importList);
    }
}
