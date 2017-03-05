using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Repositories;
using Warden.Common.ServiceClients;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Providers
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly IProviderClient _providerClient;
        private readonly IUserServiceClient _userServiceClient;

        public ApiKeyProvider(IApiKeyRepository apiKeyRepository,
            IProviderClient providerClient,
            IUserServiceClient userServiceClient)
        {
            _apiKeyRepository = apiKeyRepository;
            _providerClient = providerClient;
            _userServiceClient = userServiceClient;
        }

        public async Task<Maybe<ApiKey>> GetAsync(string userId, string name)
            => await _providerClient.GetAsync(
                async () => await _apiKeyRepository.GetAsync(userId, name),
                async () => await _userServiceClient.GetApiKeyAsync(userId, name));

        public async Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query)
            => await _providerClient.GetAsync(
                async () => await _apiKeyRepository.BrowseAsync(query),
                async () => await _userServiceClient.BrowseApiKeysAsync(query));
    }
}