
using ProjectManagementSystem.Entities;

namespace ProjectManagementSystem.DataAccess.Contracts
{
    public interface IMenuRepository
    {
       Task<IEnumerable<Feature>> GetMenuFeaturesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<FeaturesCategory>> GetfeaturesAsync(CancellationToken cancellationToken);

    }
}
