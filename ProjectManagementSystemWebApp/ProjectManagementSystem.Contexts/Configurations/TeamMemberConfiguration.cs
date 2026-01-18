using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts.Configurations
{
    internal class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(teamMember => teamMember.Id);

            builder.Property(teamMember => teamMember.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(teamMember => teamMember.Name).HasColumnType("VARCHAR(30)").HasColumnName("Name").IsRequired();
            builder.Property(teamMember => teamMember.Role).HasColumnType("VARCHAR(30)").HasColumnName("Role").IsRequired();
        }
    }
}
