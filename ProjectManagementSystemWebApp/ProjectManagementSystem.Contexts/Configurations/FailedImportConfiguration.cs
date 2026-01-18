
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class FailedImportConfiguration : IEntityTypeConfiguration<FailedImport>
    {
        public void Configure(EntityTypeBuilder<FailedImport> builder)
        {
            builder.HasKey(failedImport =>  failedImport.Id);

            builder.Property(failedImport => failedImport.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(failedImport => failedImport.ImportId).HasColumnType("int").HasColumnName("ImportId").IsRequired();
            builder.Property(failedImport => failedImport.TaskName).HasColumnType("TEXT").HasColumnName("TaskName").IsRequired();
            builder.Property(failedImport => failedImport.FailureReason).HasColumnType("TEXT").HasColumnName("FailureReason").IsRequired();

            builder.HasOne(failedImport => failedImport.Import)
                   .WithMany(failedImport => failedImport.FailedImports)
                   .HasForeignKey(failedImport => failedImport.ImportId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
