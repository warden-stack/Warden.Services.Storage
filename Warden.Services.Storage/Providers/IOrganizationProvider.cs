using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Storage.Models.Organizations;

namespace Warden.Services.Storage.Providers
{
    public interface IOrganizationProvider
    {
        Task<Maybe<Organization>> GetAsync(string userId, Guid organizationId);         
    }
}