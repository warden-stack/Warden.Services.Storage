using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Organizations;
using Warden.Services.Storage.ServiceClients.Queries;

namespace Warden.Services.Storage.Providers
{
    public interface IOrganizationProvider
    {
        Task<Maybe<PagedResult<Organization>>> BrowseAsync(BrowseOrganizations query); 
        Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId);         
    }
}