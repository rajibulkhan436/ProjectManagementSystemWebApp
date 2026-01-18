using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext _dbContext;

        public CommentRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddComment(Comment comment, CancellationToken cancellationToken)
        {
            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Comments
            .Include(c => c.Task)
            .Include(c => c.User)
            .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByIdAsync(int taskId, CancellationToken cancellationToken)
        {
            return await _dbContext.Comments
            .Where(c => c.TaskId == taskId)
            .Include(c => c.Task)
            .Include(c => c.User)
            .ToListAsync(cancellationToken);
        }
    }
}
