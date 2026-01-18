namespace ProjectManagementSystem.Services.DTOs.TaskDTOs
{
    public class TaskAssignmentDto
    {

        public int Id { get; set; }

        public int TaskId { get; set; }

        public int TeamMemberId { get; set; }

        public DateTime AssignDate { get; set; } = DateTime.Now;
    }
}
