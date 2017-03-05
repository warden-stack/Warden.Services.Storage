using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Repositories.Queries;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IMongoDatabase _database;

        public ApiKeyRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<ApiKey>> GetAsync(string userId, string name)
            => await _database.ApiKeys().GetAsync(userId, name);

        public async Task<Maybe<PagedResult<ApiKey>>> BrowseAsync(BrowseApiKeys query)
            => await _database.ApiKeys()
                .Query(query)
                .PaginateAsync(query);

        public async Task AddManyAsync(IEnumerable<ApiKey> apiKeys)
            => await _database.ApiKeys().InsertManyAsync(apiKeys);

        public async Task AddAsync(ApiKey apiKey)
            => await _database.ApiKeys().InsertOneAsync(apiKey);

        public async Task DeleteAsync(string key)
            => await _database.ApiKeys().DeleteOneAsync(x => x.Key == key);
    }
}