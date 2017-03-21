using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.ServiceClients.Queries;

namespace Warden.Services.Storage.ServiceClients
{
    public interface IOrganizationServiceClient
    {
        Task<Maybe<dynamic>> GetAsync(string userId, Guid organizationId);  
        Task<Maybe<T>> GetAsync<T>(string userId, Guid organizationId) where T : class;          
        Task<Maybe<PagedResult<dynamic>>> BrowseAsync(BrowseOrganizations query);
        Task<Maybe<PagedResult<T>>> BrowseAsync<T>(BrowseOrganizations query) where  T : class;   
    }
}