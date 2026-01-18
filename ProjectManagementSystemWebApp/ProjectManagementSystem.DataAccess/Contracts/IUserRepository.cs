using ProjectManagementSystem.Entities;
namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user, CancellationToken cancellationToken);

        Task<User> GetUserByIdAsync(int userId, CancellationToken cancellationToken);

        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<Boolean> ExistsAsync(int userId, CancellationToken cancellationToken);


    }
}
