namespace ProjectManagementSystem.Services.DTOs.TeamDTOs
{
    public class TaskTeamDto
    {    
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int TeamMemberId { get; set; }
        public string TeamMemberName { get; set; }
        public DateTime AssignedDate { get; set; }       
    }
}
