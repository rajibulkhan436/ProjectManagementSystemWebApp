using System.Text.Json.Serialization;


namespace ProjectManagementSystem.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }   
        public DateTime CommentedAt { get; set; } = DateTime.Now;

        public string? CommentMessage { get; set; }

        [JsonIgnore]
        public ProjectTask Task { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
