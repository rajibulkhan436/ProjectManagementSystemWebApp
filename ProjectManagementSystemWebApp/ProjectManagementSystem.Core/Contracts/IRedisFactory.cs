using Microsoft.Extensions.Caching.Distributed;

namespace ProjectManagementSystem.Core.Contracts
{
    public interface IRedisFactory
    {
        IDistributedCache GetCache();
    }
}
