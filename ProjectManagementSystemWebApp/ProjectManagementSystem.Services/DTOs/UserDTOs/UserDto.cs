using ProjectManagementSystem.Services.DTOs.CommentDTos;

namespace ProjectManagementSystem.Services.DTOs.UserDTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public ICollection<CommentDto> comments { get; set; } = [];

    }
}
