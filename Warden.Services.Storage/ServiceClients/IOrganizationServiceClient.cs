using System;
using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Organizations.Shared.Dto;

namespace Warden.Services.Storage.ServiceClients
{
    public interface IOrganizationServiceClient
    {
        Task<Maybe<OrganizationDto>> GetAsync(string userId, Guid organizationId);             
    }
}