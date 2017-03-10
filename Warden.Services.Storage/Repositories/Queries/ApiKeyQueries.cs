using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Common.Types;
using Warden.Services.Storage.ServiceClients.Queries;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class ApiKeyQueries
    {
        public static IMongoCollection<ApiKey> ApiKeys(this IMongoDatabase database)
            => database.GetCollection<ApiKey>();

        public static async Task<Maybe<ApiKey>> GetAsync(this IMongoCollection<ApiKey> apiKeys,
            string userId, string name)
        {
            if (userId.Empty() || name.Empty())
                return new Maybe<ApiKey>();

            return await apiKeys.FirstOrDefaultAsync(x => x.UserId == userId && x.Name == name);
        }

        public static IMongoQueryable<ApiKey> Query(this IMongoCollection<ApiKey> apiKeys,
            BrowseApiKeys query)
        {
            var values = apiKeys.AsQueryable();
            if (!query.UserId.Empty())
                values = values.Where(x => x.UserId == query.UserId);

            return values;
        }
    }
}