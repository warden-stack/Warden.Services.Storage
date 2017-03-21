using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Warden.Common.Types;
using Warden.Common.Mongo;
using Warden.Services.Storage.Models.Organizations;
using Warden.Services.Storage.ServiceClients.Queries;
using Warden.Services.Storage.Repositories.Queries;

namespace Warden.Services.Storage.Repositories
{
  public class OrganizationRepository : IOrganizationRepository
  {
    private readonly IMongoDatabase _database;

    public OrganizationRepository(IMongoDatabase database)
    {
      _database = database;
    }

    public async Task<Maybe<Organization>> GetAsync(Guid id)
        => await _database.Organizations().GetAsync(id);

    public async Task<Maybe<PagedResult<Organization>>> BrowseAsync(BrowseOrganizations query)
        => await _database.Organizations()
          .Query(query)
          .PaginateAsync(query);

    public async Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId)
        => await _database.Organizations().GetAsync(userId, organizationId);

    public async Task<Maybe<Organization>> GetAsync(string userId, string name)
        => await _database.Organizations().GetAsync(userId, name);

    public async Task UpdateAsync(Organization organization)
        => await _database.Organizations().ReplaceOneAsync(x => x.Id == organization.Id, organization);

    public async Task AddAsync(Organization organization)
        => await _database.Organizations().InsertOneAsync(organization);

    public async Task DeleteAsync(Organization organization)
        => await _database.Organizations().DeleteOneAsync(x => x.Id == organization.Id && x.Name == organization.Name);
  }
}