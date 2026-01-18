
namespace ProjectManagementSystem.Entities
{
    public  class FeaturesCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public string Icon { get; set; }

        public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
    }
}
