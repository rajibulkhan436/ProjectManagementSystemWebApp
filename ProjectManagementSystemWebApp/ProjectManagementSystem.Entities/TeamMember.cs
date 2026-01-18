namespace ProjectManagementSystem.Entities
{
    public class TeamMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public ICollection<TaskAssignment>? TaskAssignments { get; set; } = new List<TaskAssignment>();

    }
}
