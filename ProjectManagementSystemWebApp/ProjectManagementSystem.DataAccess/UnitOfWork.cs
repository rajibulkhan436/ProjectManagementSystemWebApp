using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.DataAccess.Repositories;

namespace ProjectManagementSystem.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _dbContext;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;

        }
        public IProjectRepository ProjectRepository => new ProjectRepository(_dbContext);

        public ITaskRepository TaskRepository => new TaskRepository(_dbContext);

        public ITeamRepository TeamRepository => new TeamRepository(_dbContext);

        public IUserRepository UserRepository => new UserRepository(_dbContext);

        public IMenuRepository MenuRepository => new MenuRepository(_dbContext);
        public ICommentRepository CommentRepository => new CommentRepository(_dbContext);
        public IImportRepository ImportRepository => new ImportRepository(_dbContext);


        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);  
        }
    }
}
