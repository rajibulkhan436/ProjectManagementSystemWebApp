using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(project => project.Id);

            builder.Property(project => project.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(project => project.ProjectName).HasColumnType("VARCHAR(30)").HasColumnName("ProjectName").IsRequired();
            builder.Property(project => project.StartDate).HasColumnType("Date").HasColumnName("StartDate").IsRequired();
            builder.Property(project => project.EndDate).HasColumnType("Date").HasColumnName("EndDate");
            builder.Property(project => project.Status).HasColumnType("Enum").HasColumnName("Status").IsRequired();
        }
    }
}
