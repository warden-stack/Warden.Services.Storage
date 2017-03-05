using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Mongo;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Users;

namespace Warden.Services.Storage.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IMongoDatabase _database;

        public UserSessionRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<UserSession>> GetAsync(Guid id)
            => await Collection.FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(UserSession session)
            => await Collection.InsertOneAsync(session);

        private IMongoCollection<UserSession> Collection
            => _database.GetCollection<UserSession>();
    }
}