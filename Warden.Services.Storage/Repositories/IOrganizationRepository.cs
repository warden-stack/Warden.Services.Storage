using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Organizations;

namespace Warden.Services.Storage.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Maybe<PagedResult<Organization>>> BrowseAsync(string userId, string ownerId,
            int page = 1, int results = 10);

        Task<Maybe<Organization>> GetAsync(Guid id);
        Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId);
        Task<Maybe<Organization>> GetAsync(string userId, string name);
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
    }
}