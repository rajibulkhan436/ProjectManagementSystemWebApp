
namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        IProjectRepository ProjectRepository { get; }

        ITaskRepository TaskRepository { get; }

        ITeamRepository TeamRepository { get; }

        IUserRepository UserRepository { get; }

        IMenuRepository MenuRepository { get; }

        ICommentRepository CommentRepository { get; }

        IImportRepository ImportRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
