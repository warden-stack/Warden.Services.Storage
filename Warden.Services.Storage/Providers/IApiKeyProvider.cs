using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Providers
{
    public interface IApiKeyProvider
    {
        Task<Maybe<ApiKeyDto>> GetAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query);
    }
}