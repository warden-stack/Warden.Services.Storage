using System.Collections.Generic;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Repositories
{
    public interface IApiKeyRepository
    {
        Task<Maybe<ApiKeyDto>> GetAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query);
        Task AddManyAsync(IEnumerable<ApiKeyDto> apiKeys);
        Task AddAsync(ApiKeyDto apiKey);
        Task DeleteAsync(string key);
    }
}