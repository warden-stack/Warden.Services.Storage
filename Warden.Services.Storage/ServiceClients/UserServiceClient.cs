using System;
using System.Threading.Tasks;
using NLog;
using Warden.Common.Types;
using Warden.Services.Storage.Queries;
using Warden.Services.Storage.Settings;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.ServiceClients
{
    public class UserServiceClient : IUserServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly ProviderSettings _settings;

        public UserServiceClient(IServiceClient serviceClient, ProviderSettings settings)
        {
            _serviceClient = serviceClient;
            _settings = settings;
        }

        public async Task<Maybe<UserDto>> GetAsync(string userId)
        {
            Logger.Debug($"Requesting GetAsync, userId:{userId}");
            return await _serviceClient.GetAsync<UserDto>(_settings.UsersApiUrl, $"users/{userId}");
        }

        public async Task<Maybe<UserDto>> GetByNameAsync(string name)
        {
            Logger.Debug($"Requesting GetByNameAsync, name:{name}");
            return await _serviceClient.GetAsync<UserDto>(_settings.UsersApiUrl, $"users/{name}/account");
        }

        public async Task<Maybe<UserSessionDto>> GetSessionAsync(Guid id)
        {
            Logger.Debug($"Requesting GetSessionAsync, id:{id}");
            return await _serviceClient.GetAsync<UserSessionDto>(_settings.UsersApiUrl, $"user-sessions/{id}");
        }

        public async Task<Maybe<ApiKeyDto>> GetApiKeyAsync(string userId, string name)
        {
            Logger.Debug($"Requesting GetApiKeyAsync, userId:{userId}, name:{name}");
            return await _serviceClient.GetAsync<ApiKeyDto>(_settings.UsersApiUrl, 
                $"users/{userId}/api-keys/{name}");
        }

        public async Task<Maybe<PagedResult<ApiKeyDto>>> BrowseApiKeysAsync(BrowseApiKeys query)
        {
            Logger.Debug($"Requesting BrowseApiKeysAsync");
            return await _serviceClient.GetCollectionAsync<ApiKeyDto>(_settings.UsersApiUrl, 
                $"users/{query.UserId}/api-keys");
        }

        public async Task<Maybe<AvailableResourceDto>> IsAvailableAsync(string name)
        {
            Logger.Debug($"Requesting IsAvailableAsync, name:{name}");
            return await _serviceClient.GetAsync<AvailableResourceDto>(_settings.UsersApiUrl, $"usernames/{name}/available");
        }
    }
}