using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class FeatureCategoryConfiguration : IEntityTypeConfiguration<FeaturesCategory>
    {
        public void Configure(EntityTypeBuilder<FeaturesCategory> builder)
        {
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(category => category.Category).HasColumnType("VARCHAR(30)").HasColumnName("Category").IsRequired();
            builder.Property(category => category.Icon).HasColumnType("VARCHAR(30)").HasColumnName("Icon").IsRequired();

        }
    }
}
