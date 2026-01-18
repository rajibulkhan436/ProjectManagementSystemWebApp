
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Services.DTOs.FeaturesDTOs
{
    public class FeatureCategoryDto
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public string Icon { get; set; }

        public virtual ICollection<FeatureDto> Features { get; set; } = [];
    }
}
