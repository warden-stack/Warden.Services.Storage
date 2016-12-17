using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Mongo;
using Warden.Common.Types;
using Warden.Services.Users.Shared.Dto;

namespace Warden.Services.Storage.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IMongoDatabase _database;

        public UserSessionRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<UserSessionDto>> GetAsync(Guid id)
            => await Collection.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(UserSessionDto session)
            => await Collection.InsertOneAsync(session);

        private IMongoCollection<UserSessionDto> Collection
            => _database.GetCollection<UserSessionDto>();
    }
}