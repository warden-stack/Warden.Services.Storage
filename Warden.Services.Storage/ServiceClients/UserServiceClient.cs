using System;
using System.Threading.Tasks;
using NLog;
using Warden.Common.ServiceClients;
using Warden.Common.Types;
using Warden.Services.Storage.ServiceClients.Queries;

namespace Warden.Services.Storage.ServiceClients
{
    public class UserServiceClient : IUserServiceClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceClient _serviceClient;
        private readonly string _name;

        public UserServiceClient(IServiceClient serviceClient, string name)
        {
            _serviceClient = serviceClient;
            _name = name;
        }

        public async Task<Maybe<dynamic>> GetAsync(string userId)
            => await GetAsync<dynamic>(userId);

        public async Task<Maybe<T>> GetAsync<T>(string userId) where  T : class
            => await _serviceClient.GetAsync<T>(_name, $"users/{userId}");

        public async Task<Maybe<dynamic>> GetByNameAsync(string name)
            => await GetByNameAsync<dynamic>(name);

        public async Task<Maybe<T>> GetByNameAsync<T>(string name) where  T : class
            => await _serviceClient.GetAsync<T>(_name, $"users/{name}/account");

        public async Task<Maybe<dynamic>> GetSessionAsync(Guid id)
            => await GetSessionAsync<dynamic>(id);

        public async Task<Maybe<T>> GetSessionAsync<T>(Guid id) where  T : class
            => await _serviceClient.GetAsync<T>(_name, $"user-sessions/{id}");

        public async Task<Maybe<dynamic>> GetApiKeyAsync(string userId, string name) 
            => await GetApiKeyAsync<dynamic>(userId, name);

        public async Task<Maybe<T>> GetApiKeyAsync<T>(string userId, string name) where  T : class
            => await _serviceClient.GetAsync<T>(_name, $"users/{userId}/api-keys/{name}");

        public async Task<Maybe<PagedResult<dynamic>>> BrowseApiKeysAsync(BrowseApiKeys query)
            => await BrowseApiKeysAsync<dynamic>(query);

        public async Task<Maybe<PagedResult<T>>> BrowseApiKeysAsync<T>(BrowseApiKeys query) where  T : class
            => await _serviceClient.GetCollectionAsync<T>(_name, $"users/{query.UserId}/api-keys");

        public async Task<Maybe<dynamic>> IsAvailableAsync(string name)
            => await IsAvailableAsync<dynamic>(name);

        public async Task<Maybe<T>> IsAvailableAsync<T>(string name) where  T : class
             => await _serviceClient.GetAsync<T>(_name, $"usernames/{name}/available");
    }
}