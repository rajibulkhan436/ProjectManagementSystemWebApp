using ProjectManagementSystem.Core.Enums;

namespace ProjectManagementSystem.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; } = [];
    }
}
