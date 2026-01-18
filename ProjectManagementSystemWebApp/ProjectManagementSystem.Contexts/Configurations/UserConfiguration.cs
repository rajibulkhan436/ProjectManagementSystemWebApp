using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.UserName).HasColumnType("VARCHAR(20)").HasColumnName("UserName").IsRequired();
            builder.Property(user => user.Email).HasColumnType("string").HasColumnName("Email").IsRequired();
            builder.Property(user => user.Password).HasColumnType("string").HasColumnName("Password").IsRequired();
            builder.Property(user => user.Role).HasColumnType("string").HasColumnName("Role");
        }
    }
}
