using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;
using Warden.Services.Storage.Repositories;
using Warden.Services.Storage.ServiceClients;
using Warden.Services.Storage.Settings;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Providers
{
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly IProviderClient _providerClient;
        private readonly IUserServiceClient _userServiceClient;
        private readonly ProviderSettings _providerSettings;

        public ApiKeyProvider(IApiKeyRepository apiKeyRepository,
            IProviderClient providerClient,
            IUserServiceClient userServiceClient,
            ProviderSettings providerSettings)
        {
            _apiKeyRepository = apiKeyRepository;
            _providerClient = providerClient;
            _userServiceClient = userServiceClient;
            _providerSettings = providerSettings;
        }

        public async Task<Maybe<ApiKeyDto>> GetAsync(string userId, string name)
            => await _providerClient.GetAsync(
                async () => await _apiKeyRepository.GetAsync(userId, name),
                async () => await _userServiceClient.GetApiKeyAsync(userId, name));

        public async Task<Maybe<PagedResult<ApiKeyDto>>> BrowseAsync(BrowseApiKeys query)
            => await _providerClient.GetAsync(
                async () => await _apiKeyRepository.BrowseAsync(query),
                async () => await _userServiceClient.BrowseApiKeysAsync(query));
    }
}