using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task AddUserAsync(User user,CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistsAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var userList = await _dbContext.Users.ToListAsync(cancellationToken);
            return userList;
        }

        public Task<User> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
