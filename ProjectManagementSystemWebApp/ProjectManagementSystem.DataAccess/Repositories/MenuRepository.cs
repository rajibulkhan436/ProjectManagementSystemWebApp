using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Contexts;
using ProjectManagementSystem.DataAccess.Contracts;
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DatabaseContext _dbContext;

        public MenuRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Feature>> GetMenuFeaturesAsync(CancellationToken cancellationToken)
        {
            var featuresList = await _dbContext.Features.ToListAsync(cancellationToken);
            return featuresList;
        }

        public async Task<IEnumerable<FeaturesCategory>> GetfeaturesAsync(CancellationToken cancellationToken)
        {
            var featuresList = await _dbContext.FeaturesCategory
                .Include(menu => menu.Features)
                .ToListAsync(cancellationToken);
            return featuresList;
        }
    }
}
