
namespace ProjectManagementSystem.Services.DTOs.ImportDTOs
{
    public class ImportDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public DateTime UploadTime { get; set; }

        public int Total { get; set; }

        public int Success { get; set; }

        public int Fails { get; set; }

        public ICollection<FailedImportDto>? FailedImports { get; set; } = [];
    }
}
