
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Comment>> GetCommentsByIdAsync(int taskId, CancellationToken cancellationToken);

        Task AddComment(Comment comment, CancellationToken cancellationToken);
    }
}
