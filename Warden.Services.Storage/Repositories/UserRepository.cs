using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Types;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.Users;
using System.Collections.Generic;
using Warden.Common.ServiceClients.Queries;

namespace Warden.Services.Storage.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<bool> ExistsAsync(string userId)
            => await Collection.AsQueryable().AnyAsync(x => x.UserId == userId);

        public async Task<Maybe<User>> GetByIdAsync(string userId)
            => await Collection.FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<Maybe<User>> GetByNameAsync(string name)
            => await Collection.FirstOrDefaultAsync(x => x.Name == name);

        public async Task UpdateAsync(User user)
            => await Collection.ReplaceOneAsync(x => x.UserId == user.UserId, user);

        public async Task AddAsync(User user)
            => await Collection.InsertOneAsync(user);

        public async Task<Maybe<AvailableResource>> IsNameAvailableAsync(string name)
        {
            var exists = await Collection.AsQueryable()
                            .AnyAsync(x => x.Name == name);

            return new AvailableResource {IsAvailable = exists == false};
        }

        public async Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query)
        {
            var users = Collection.AsQueryable();

            return await users.PaginateAsync(query);
        }

        public async Task AddManyAsync(IEnumerable<User> users)
            => await Collection.InsertManyAsync(users);

        private IMongoCollection<User> Collection
            => _database.GetCollection<User>();
    }
}