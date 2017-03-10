using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.Organizations;
using Warden.Services.Storage.ServiceClients.Queries;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class OrganizationQueries
    {
        public static IMongoCollection<Organization> Organizations(this IMongoDatabase database)
            => database.GetCollection<Organization>();

        public static async Task<Organization> GetAsync(this IMongoCollection<Organization> organizations,
            Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await organizations.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<Organization> GetAsync(this IMongoCollection<Organization> organizations,
            string userId, Guid organizationId)
        {
            if (userId.Empty() || organizationId == Guid.Empty)
                return null;

            return await organizations.AsQueryable().FirstOrDefaultAsync(x => x.Id == organizationId && 
                (x.Owner.UserId == userId || x.Users.Any(u => u.UserId == userId)));
        }

        public static async Task<Organization> GetAsync(this IMongoCollection<Organization> organizations,
            string userId, string name)
        {
            if (userId.Empty() || name.Empty())
                return null;

            return await organizations.AsQueryable().FirstOrDefaultAsync(x => x.Owner.UserId == userId && x.Name == name);
        }

        public static async Task<Organization> GetByNameForOwnerAsync(
            this IMongoCollection<Organization> organizations,
            string name, string ownerId)
        {
            if (name.Empty() || ownerId.Empty())
                return null;

            var fixedName = name.TrimToLower();

            return await organizations
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == fixedName
                                          && x.Owner.UserId == ownerId);
        }

        public static IMongoQueryable<Organization> Query(this IMongoCollection<Organization> organizations,
            BrowseOrganizations query)
        {
            var values = organizations.AsQueryable();
            if (query.UserId.Empty() == false)
                values = values.Where(x => x.Users.Any(u => u.UserId == query.UserId));
            if (query.OwnerId.Empty() == false)
                values = values.Where(x => x.Owner.UserId == query.OwnerId);

            return values.OrderBy(x => x.Name);
        }

        public static async Task<bool> ExistsAsync(this IMongoCollection<Organization> organizations,
            Guid id)
        {
            if (id == Guid.Empty)
                return false;

            return await organizations.AsQueryable().AnyAsync(x => x.Id == id);
        }
    }
}