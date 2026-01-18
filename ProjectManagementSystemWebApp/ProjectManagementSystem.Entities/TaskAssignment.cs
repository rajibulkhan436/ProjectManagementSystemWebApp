using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Entities
{
    public class TaskAssignment
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int TeamMemberId { get; set; }

        public DateTime AssignDate { get; set; }


        [JsonIgnore]
        public ProjectTask? Task { get; set; }
        [JsonIgnore]
        public TeamMember? TeamMember { get; set; }
    }
    
}
