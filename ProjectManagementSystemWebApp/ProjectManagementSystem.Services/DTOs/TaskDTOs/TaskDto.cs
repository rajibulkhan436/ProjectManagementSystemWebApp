using ProjectManagementSystem.Core.Enums;
using ProjectManagementSystem.Entities;
using ProjectManagementSystem.Services.DTOs.CommentDTos;

namespace ProjectManagementSystem.Services.DTOs.TaskDTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int ProjectId { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }

        public ICollection<CommentDto>? Comments { get; set; } = [];
        public ICollection<TaskAssignmentDto>? TaskAssignments { get; set; } = [];
    }
}
