using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnType("int")
                   .HasColumnName("Id")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.UserName)
                   .HasColumnType("VARCHAR(30)")
                   .HasColumnName("UserName")
                   .IsRequired();

            builder.Property(c => c.TaskId)
                   .HasColumnType("int")
                   .HasColumnName("TaskId")
                   .IsRequired();

            builder.Property(c => c.CommentedAt)
                   .HasColumnType("DATETIME")
                   .HasColumnName("CommentedAt")
                   .IsRequired();

            builder.Property(c => c.CommentMessage)
                   .HasColumnType("TEXT")
                   .HasColumnName("CommentMessage");

            // **Ensuring TaskId is the correct FK column instead of ProjectTaskId**
            builder.HasOne(c => c.Task)
                   .WithMany(c => c.Comments)
                   .HasForeignKey(c => c.TaskId)
                   .HasConstraintName("TaskId")
                   .OnDelete(DeleteBehavior.Cascade);

            // **Ensuring UserName is the correct FK column instead of UserId**
            builder.HasOne(c => c.User)
                   .WithMany(c => c.Comments)
                   .HasForeignKey(c => c.UserName)
                   .HasPrincipalKey(u => u.UserName) 
                   .HasConstraintName("UserName")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
