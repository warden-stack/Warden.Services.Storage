using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Types;
using Warden.Common.Mongo;
using Warden.Services.Users.Shared.Dto;

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

        private IMongoCollection<UserDto> Collection
            => _database.GetCollection<UserDto>();
    }
}