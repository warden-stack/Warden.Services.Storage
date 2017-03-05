using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Repositories
{
    public interface IApiKeyRepository
    {
        Task<Maybe<ApiKey>> GetAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query);
        Task AddManyAsync(IEnumerable<ApiKey> apiKeys);
        Task AddAsync(ApiKey apiKey);
        Task DeleteAsync(string key);
    }
}