using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Types;
using Warden.Common.Mongo;
using Warden.Services.Users.Shared.Dto;
using System.Collections.Generic;
using Warden.Services.Storage.Queries;

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

        public async Task<Maybe<UserDto>> GetByIdAsync(string userId)
            => await Collection.FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<Maybe<UserDto>> GetByNameAsync(string name)
            => await Collection.FirstOrDefaultAsync(x => x.Name == name);

        public async Task UpdateAsync(UserDto user)
            => await Collection.ReplaceOneAsync(x => x.UserId == user.UserId, user);

        public async Task AddAsync(UserDto user)
            => await Collection.InsertOneAsync(user);

        public async Task<Maybe<AvailableResourceDto>> IsNameAvailableAsync(string name)
        {
            var exists = await Collection.AsQueryable()
                            .AnyAsync(x => x.Name == name);

            return new AvailableResourceDto {IsAvailable = exists == false};
        }

        public async Task<Maybe<PagedResult<UserDto>>> BrowseAsync(BrowseUsers query)
        {
            var users = Collection.AsQueryable();

            return await users.PaginateAsync(query);
        }

        public async Task AddManyAsync(IEnumerable<UserDto> users)
            => await Collection.InsertManyAsync(users);

        private IMongoCollection<UserDto> Collection
            => _database.GetCollection<UserDto>();
    }
}