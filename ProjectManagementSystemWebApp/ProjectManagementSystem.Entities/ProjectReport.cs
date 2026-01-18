using ProjectManagementSystem.Core.Enums;
namespace ProjectManagementSystem.Entities
{
    public class ProjectReport
    {
        public string ProjectName { get; set; }

        public string? TaskName { get; set; }

        public Status? TaskStatus { get; set; }

        public DateTime? DueDate { get; set; }

        public Status ProjectStatus { get; set; }
    }
}
