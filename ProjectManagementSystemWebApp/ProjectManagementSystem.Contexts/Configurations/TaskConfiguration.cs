using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.HasKey(projectTask => projectTask.Id);

            builder.Property(projectTask => projectTask.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(projectTask => projectTask.TaskName).HasColumnType("VARCHAR(30)").HasColumnName("TaskName").IsRequired();
            builder.Property(projectTask => projectTask.TaskDescription).HasColumnType("TEXT").HasColumnName("TaskDescription");
            builder.Property(projectTask => projectTask.ProjectId).HasColumnType("int").HasColumnName("ProjectId").IsRequired();
            builder.Property(projectTask => projectTask.DueDate).HasColumnType("Date").HasColumnName("DueDate").IsRequired();
            builder.Property(projectTask => projectTask.Status).HasColumnType("int").HasColumnName("Status").IsRequired();

            builder.HasOne(projectTask => projectTask.Project) 
                   .WithMany(projectTask => projectTask.ProjectTasks)
                   .HasForeignKey(projectTask => projectTask.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

