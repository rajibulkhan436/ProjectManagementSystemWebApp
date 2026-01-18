

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class ImportConfiguration : IEntityTypeConfiguration<Import>
    {
        public void Configure(EntityTypeBuilder<Import> builder)
        {
            builder.HasKey(import => import.Id);

            builder.Property(import => import.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(import => import.FileName).HasColumnType("TEXT").HasColumnName("FileName").IsRequired();
            builder.Property(import => import.UploadTime).HasColumnType("Date").HasColumnName("UploadTime").IsRequired();
            builder.Property(import => import.Total).HasColumnType("int").HasColumnName("Total").IsRequired();
            builder.Property(import => import.Success).HasColumnType("int").HasColumnName("Success").IsRequired();
            builder.Property(import => import.Fails).HasColumnType("int").HasColumnName("Fails").IsRequired();

            
        }
    }
}
