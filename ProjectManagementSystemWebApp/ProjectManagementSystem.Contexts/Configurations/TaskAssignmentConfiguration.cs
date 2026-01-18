using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class TaskAssignmentConfiguration : IEntityTypeConfiguration<TaskAssignment>
    {
        public void Configure(EntityTypeBuilder<TaskAssignment> builder)
        {
            builder.HasKey(taskAssignment => taskAssignment.Id);

            builder.Property(taskAssignment => taskAssignment.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(taskAssignmentt => taskAssignmentt.TaskId).HasColumnType("int").HasColumnName("TaskId").IsRequired();
            builder.Property(taskAssignment => taskAssignment.TeamMemberId).HasColumnType("int").HasColumnName("TeamMemberId").IsRequired();
            builder.Property(taskAssignment => taskAssignment.AssignDate).HasColumnType("Date").HasColumnName("AssignDate").IsRequired();

            builder.HasOne(taskAssignment => taskAssignment.Task)
                   .WithMany(task => task.TaskAssignments)  
                   .HasForeignKey(taskAssignment => taskAssignment.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(taskAssignment => taskAssignment.TeamMember)
                   .WithMany(member => member.TaskAssignments)
                   .HasForeignKey(taskAssignment => taskAssignment.TeamMemberId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
