using ProjectManagementSystem.Core.Enums;

namespace ProjectManagementSystem.Services.DTOs.ProjectDTOs
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status { get; set; }
    }
}
