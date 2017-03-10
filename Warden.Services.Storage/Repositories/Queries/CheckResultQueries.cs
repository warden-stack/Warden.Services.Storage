using System;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Mongo;
using Warden.Services.Storage.ServiceClients.Queries;
using Warden.Services.Storage.Models.WardenChecks;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class CheckResultQueries
    {
        public static IMongoCollection<CheckResult> CheckResults(this IMongoDatabase database)
            => database.GetCollection<CheckResult>();

        public static IMongoQueryable<CheckResult> Query(this IMongoCollection<CheckResult> checkResults,
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