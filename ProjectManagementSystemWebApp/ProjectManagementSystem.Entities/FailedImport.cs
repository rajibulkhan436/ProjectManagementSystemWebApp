
using System.Text.Json.Serialization;

namespace ProjectManagementSystem.Entities
{
    public class FailedImport
    {
        public int Id { get; set; }

        public int ImportId { get; set; }

        public string TaskName { get; set; }

        public string FailureReason {  get; set; }

        [JsonIgnore]
        public Import? Import { get; set; }

    }
}
