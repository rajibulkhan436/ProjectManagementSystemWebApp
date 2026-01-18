namespace ProjectManagementSystem.Entities
{
    public class Feature
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public  string FeatureName{  get; set; }

        public string PathUrl { get; set; }

        public FeaturesCategory Category { get; set; }
        
    }
}

