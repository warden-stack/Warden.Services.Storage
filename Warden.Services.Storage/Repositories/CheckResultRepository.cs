using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Common.ServiceClients.Queries;
using Warden.Services.Storage.Repositories.Queries;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.WardenChecks;

namespace Warden.Services.Storage.Repositories
{
    public class CheckResultRepository : ICheckResultRepository
    {
        private readonly IMongoDatabase _database;

        public CheckResultRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<PagedResult<CheckResult>>> BrowseAsync(Guid organizationId,
            Guid wardenId, int page = 1, int results = 10)
        {
            var query = new BrowseWardenCheckResults
            {
                OrganizationId = organizationId,
                WardenId = wardenId,
                Page = page,
                Results = results
            };

            return await _database.CheckResults()
                .Query(query)
                .PaginateAsync(query);
        }

        public async Task AddAsync(CheckResult checkResult)
            => await _database.CheckResults().InsertOneAsync(checkResult);

        public async Task DeleteAsync(CheckResult checkResult)
            => await _database.CheckResults().DeleteOneAsync(x => x.Id == checkResult.Id);
    }
}