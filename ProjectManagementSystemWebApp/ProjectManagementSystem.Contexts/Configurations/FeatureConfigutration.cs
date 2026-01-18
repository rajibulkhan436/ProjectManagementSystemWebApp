using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class FeatureConfigutration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(feature => feature.Id);

            builder.Property(feature => feature.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(feature => feature.CategoryId).HasColumnType("int").HasColumnName("CategoryId").IsRequired();
            builder.Property(feature => feature.FeatureName).HasColumnType("VARCHAR(30)").HasColumnName("FeatureName").IsRequired();
            builder.Property(feature => feature.PathUrl).HasColumnType("VARCHAR(30)").HasColumnName("PathUrl").IsRequired();

            builder.HasOne(feature => feature.Category)
                   .WithMany(feature => feature.Features)
                   .HasForeignKey(feature => feature.CategoryId)
                   .HasConstraintName("CategoryId")
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
