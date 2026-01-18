namespace ProjectManagementSystem.Services.DTOs.CommentDTos
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int TaskId { get; set; }

        public int UserId { get; set; }

        public DateTime CommentedAt { get; set; } = DateTime.Now;

        public string? CommentMessage { get; set; }
    }
}
