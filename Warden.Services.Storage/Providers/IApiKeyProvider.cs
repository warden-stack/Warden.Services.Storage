using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Providers
{
    public interface IApiKeyProvider
    {
        Task<Maybe<ApiKey>> GetAsync(string userId, string name);
        Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query);
    }
}