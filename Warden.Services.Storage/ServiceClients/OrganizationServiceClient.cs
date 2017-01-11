using System;
using System.Threading.Tasks;
using Warden.Common.Security;
using Warden.Common.Types;
using Warden.Services.Organizations.Shared.Dto;

namespace Warden.Services.Storage.ServiceClients
{
    public class OrganizationServiceClient : IOrganizationServiceClient
    {
        private readonly IServiceClient _serviceClient;
        private readonly ServiceSettings _settings;

        public OrganizationServiceClient(IServiceClient serviceClient, ServiceSettings settings)
        {
            _serviceClient = serviceClient;
            _settings = settings;
            _serviceClient.SetSettings(settings);
        }

        public async Task<Maybe<OrganizationDto>> GetAsync(string userId, Guid organizationId)
        {
            return await _serviceClient.GetAsync<OrganizationDto>(_settings.Url, 
                $"/organizations/{organizationId}?userId={userId}");
        }
    }
}