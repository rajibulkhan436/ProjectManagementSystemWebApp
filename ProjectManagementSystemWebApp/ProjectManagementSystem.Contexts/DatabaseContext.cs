using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Contexts.Configurations;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.Contexts
{
    public class DatabaseContext : DbContext
    {
            
        public DatabaseContext(DbContextOptions<DatabaseContext> context): base(context) 
        {

        }
      
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> Tasks { get; set; }

        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<FeaturesCategory> FeaturesCategory { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Import> Imports { get; set; }
        
        
       public DbSet<FailedImport> FailedImports { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder
        //        .ApplyConfiguration(new TaskConfiguration())
        //        .ApplyConfiguration(new CommentConfiguration())
        //        .ApplyConfiguration(new ProjectConfiguration())
        //        .ApplyConfiguration(new TaskAssignmentConfiguration());
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
