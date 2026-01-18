using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using ProjectManagementSystem.Core.Enums;
namespace ProjectManagementSystem.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int ProjectId { get; set; }

        public Status Status { get; set; }

        public DateTime DueDate { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }

        public ICollection<Comment>? Comments { get; set; } = [];

        public ICollection<TaskAssignment>? TaskAssignments { get; set; } = [];
    }
}
