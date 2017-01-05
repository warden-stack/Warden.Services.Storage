using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Mongo;
using Warden.Services.Storage.Queries;
using Warden.Services.WardenChecks.Shared.Dto;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class CheckResultQueries
    {
        public static IMongoCollection<CheckResultDto> CheckResults(this IMongoDatabase database)
            => database.GetCollection<CheckResultDto>();

        public static IMongoQueryable<CheckResultDto> Query(this IMongoCollection<CheckResultDto> checkResults,
            BrowseWardenCheckResults query)
        {
            var values = checkResults.AsQueryable();
            if (query.OrganizationId != Guid.Empty)
                values = values.Where(x => x.OrganizationId == query.OrganizationId);
            if (query.WardenId != Guid.Empty)
                values = values.Where(x => x.WardenId == query.WardenId);

            return values.OrderByDescending(x => x.CreatedAt);
        }
    }
}