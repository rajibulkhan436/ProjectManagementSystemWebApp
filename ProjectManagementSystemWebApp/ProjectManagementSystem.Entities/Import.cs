
namespace ProjectManagementSystem.Entities
{
    public class Import
    {
        public int Id {  get; set; }

        public string FileName { get; set; }

        public DateTime UploadTime { get; set; }

        public int Total {  get; set; }

        public int Success {  get; set; }

        public int Fails {  get; set; }

        public ICollection<FailedImport>? FailedImports { get; set; } = [];
    }
}
