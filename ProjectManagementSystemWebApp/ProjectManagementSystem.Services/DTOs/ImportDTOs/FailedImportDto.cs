
namespace ProjectManagementSystem.Services.DTOs.ImportDTOs
{
    public class FailedImportDto
    {
        public int Id { get; set; }

        public int ImportId { get; set; }

        public string TaskName { get; set; }

        public string FailureReason { get; set; }
    }
}
